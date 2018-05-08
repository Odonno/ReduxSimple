using ReduxSimple.Samples.Extensions;
using SuccincT.Options;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using static Microsoft.Toolkit.Uwp.Helpers.DispatcherHelper;

namespace ReduxSimple.Samples.Pokedex
{
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

            Store.ObserveState(state => state.Search)
                .ObserveOn(Scheduler.Default)
                .Subscribe(search =>
                {
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
                            var suggestions = Store.State.Pokedex
                                .Where(p => p.Id.ToString().Contains(search) || p.Name.ToLower().Contains(search.ToLower()))
                                .OrderBy(p => p.Id)
                                .Take(10)
                                .ToImmutableList();

                            if (suggestions.Any())
                                Store.Dispatch(new GetPokemonByIdAction { Id = suggestions.First().Id });
                            else
                                Store.Dispatch(new ResetPokemonAction());
                        }
                    }
                    else
                    {
                        Store.Dispatch(new ResetPokemonAction());
                    }
                });

            Store.ObserveState(state => state.Suggestions)
                .ObserveOn(Scheduler.Default)
                .Subscribe(suggestions =>
                {
                    ExecuteOnUIThreadAsync(() =>
                    {
                        AutoSuggestBox.ItemsSource = suggestions;
                    });
                });

            Store.ObserveState(state => state.Pokemon)
                .ObserveOn(Scheduler.Default)
                .Subscribe(pokemon =>
                {
                    ExecuteOnUIThreadAsync(() =>
                    {
                        PokemonIdTextBlock.Text = pokemon.HasValue ? $"#{pokemon.Value.Id}" : string.Empty;
                        PokemonNameTextBlock.Text = pokemon.HasValue ? pokemon.Value.Name : string.Empty;
                        PokemonImage.Source = pokemon.HasValue ? new BitmapImage(new Uri(pokemon.Value.Image)) : null;
                    });
                });

            // Observe UI events
            AutoSuggestBox.ObserveOnTextChanged()
               .ObserveOn(Scheduler.Default)
               .Subscribe(e =>
               {
                   ExecuteOnUIThreadAsync(() =>
                   {
                       Store.Dispatch(new UpdateSearchStringAction { Search = AutoSuggestBox.Text });
                   });
               });

            AutoSuggestBox.ObserveOnSuggestionChosen()
                .ObserveOn(Scheduler.Default)
                .Subscribe(e =>
                {
                    ExecuteOnUIThreadAsync(() =>
                    {
                        var selectedPokemonOption = (e.EventArgs.SelectedItem as PokemonGeneralInfo).ToOption();
                        if (selectedPokemonOption.HasValue)
                        {
                            AutoSuggestBox.Text = selectedPokemonOption.Value.Name; // Avoid the automatic change of the Text property of SuggestBox
                            Store.Dispatch(new UpdateSearchStringAction { Search = selectedPokemonOption.Value.Name });
                        }
                    });
                });

            // Initialize UI
            AutoSuggestBox.Text = Store.State.Search;
            PokemonIdTextBlock.Text = Store.State.Pokemon.HasValue ? $"#{Store.State.Pokemon.Value.Id}" : string.Empty;
            PokemonNameTextBlock.Text = Store.State.Pokemon.HasValue ? Store.State.Pokemon.Value.Name : string.Empty;
            PokemonImage.Source = Store.State.Pokemon.HasValue ? new BitmapImage(new Uri(Store.State.Pokemon.Value.Image)) : null;

            // Initialize Components
            HistoryComponent.Store = Store;

            // Start logic
            if (!Store.State.Loading && Store.State.Pokedex.Count <= 0)
            {
                Store.Dispatch(new GetPokemonListAction());
            }
        }
    }
}
