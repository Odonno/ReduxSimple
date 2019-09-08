```csharp
public sealed partial class PokedexPage : Page
{
    private readonly PokedexApiClient _pokedexApiClient = new PokedexApiClient();

    public PokedexStore Store = new PokedexStore();

    public PokedexPage()
    {
        InitializeComponent();

        // Observe changes on state

        // Load pokemon list from API
        Store.ObserveAction<GetPokemonListAction>()
            .SelectMany(_ => _pokedexApiClient.GetPokedex())
            .Subscribe(response =>
            {
                Store.Dispatch(new GetPokemonListFullfilledAction
                {
                    Pokedex = response.Root[0].Pokemons
                        .Select(p => new PokemonGeneralInfo { Id = p.Id, Name = p.Name.Capitalize() })
                        .ToList()
                });
            }, e =>
            {
                Store.Dispatch(new GetPokemonListFailedAction
                {
                    Exception = e
                });
            });

        // Load pokemon by id from API
        Store.ObserveAction<GetPokemonByIdAction>()
            .SelectMany(action =>
            {
                return _pokedexApiClient.GetPokemonById(action.Id)
                    .TakeUntil(Store.ObserveAction<GetPokemonByIdAction>());
            })
            .Subscribe(response =>
            {
                Store.Dispatch(new GetPokemonByIdFullfilledAction
                {
                    Pokemon = new Pokemon
                    {
                        Id = response.Id,
                        Name = response.Name.Capitalize(),
                        Image = response.Sprites.Image,
                        Types = response.Types
                            .OrderBy(t => t.Slot)
                            .Select(t => new PokemonType { Name = t.Type.Name })
                            .ToList()
                    }
                });
            }, e =>
            {
                Store.Dispatch(new GetPokemonByIdFailedAction
                {
                    Exception = e
                });
            });

        Observable.CombineLatest(
            Store.Select(SelectLoading),
            Store.Select(SelectIsPokedexEmpty),
            Tuple.Create
        )
            .ObserveOn(Scheduler.Default)
            .Subscribe(x =>
            {
                var (loading, isPokedexEmpty) = x;

                ExecuteOnUIThreadAsync(() =>
                {
                    OpenPokedexButton.ShowIf(!loading && isPokedexEmpty);

                    GlobalLoadingProgressRing.IsActive = loading && isPokedexEmpty;
                    GlobalLoadingProgressRing.ShowIf(loading && isPokedexEmpty);
                    RootStackPanel.ShowIf(!isPokedexEmpty);
                });
            });

        Store.Select(SelectSearch)
            .ObserveOn(Scheduler.Default)
            .Subscribe(search =>
            {
                if (Store.State.Pokedex.IsEmpty)
                    return;

                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (search.StartsWith("#"))
                    {
                        // Search Pokemon by Id
                        if (int.TryParse(search.Substring(1), out int searchedId))
                        {
                            if (Store.State.Pokedex.Any(p => p.Id == searchedId))
                                Store.Dispatch(new GetPokemonByIdAction { Id = searchedId });
                            else
                                Store.Dispatch(new ResetPokemonAction());
                        }
                    }
                    else
                    {
                        // Search Pokemon by both Id and Name
                        if (Store.State.Suggestions.Any())
                            Store.Dispatch(new GetPokemonByIdAction { Id = Store.State.Suggestions.First().Id });
                        else
                            Store.Dispatch(new ResetPokemonAction());
                    }
                }
                else
                {
                    Store.Dispatch(new ResetPokemonAction());
                }
            });

        Store.Select(SelectSuggestions)
            .ObserveOn(Scheduler.Default)
            .Subscribe(suggestions =>
            {
                ExecuteOnUIThreadAsync(() =>
                {
                    AutoSuggestBox.ItemsSource = suggestions;
                });
            });

        Store.Select(SelectPokemon)
            .ObserveOn(Scheduler.Default)
            .Subscribe(pokemon =>
            {
                ExecuteOnUIThreadAsync(() =>
                {
                    PokemonPanel.ShowIf(pokemon.HasValue);
                    PokemonIdTextBlock.Text = pokemon.HasValue ? $"#{pokemon.Value.Id}" : string.Empty;
                    PokemonNameTextBlock.Text = pokemon.HasValue ? pokemon.Value.Name : string.Empty;
                    PokemonImage.Source = pokemon.HasValue ? new BitmapImage(new Uri(pokemon.Value.Image)) : null;
                });
            });

        Store.Select(SelectErrors)
            .ObserveOn(Scheduler.Default)
            .Subscribe(errors =>
            {
                ExecuteOnUIThreadAsync(() =>
                {
                    ErrorsListView.ItemsSource = errors;
                });
            });

        // Observe UI events
        OpenPokedexButton.Events().Click
            .ObserveOn(Scheduler.Default)
            .Subscribe(_ => Store.Dispatch(new GetPokemonListAction()));

        AutoSuggestBox.Events().TextChanged
            .ObserveOn(Scheduler.Default)
            .Subscribe(_ =>
            {
                ExecuteOnUIThreadAsync(() =>
                {
                    Store.Dispatch(new UpdateSearchStringAction { Search = AutoSuggestBox.Text });
                });
            });

        AutoSuggestBox.Events().SuggestionChosen
            .ObserveOn(Scheduler.Default)
            .Subscribe(e =>
            {
                ExecuteOnUIThreadAsync(() =>
                {
                    var selectedPokemonOption = (e.SelectedItem as PokemonGeneralInfo).ToOption();
                    if (selectedPokemonOption.HasValue)
                    {
                        AutoSuggestBox.Text = selectedPokemonOption.Value.Name; // Avoid the automatic change of the Text property of SuggestBox
                        Store.Dispatch(new UpdateSearchStringAction { Search = selectedPokemonOption.Value.Name });
                    }
                });
            });
    }
}
```