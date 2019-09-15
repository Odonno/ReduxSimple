using Microsoft.Toolkit.Uwp.UI.Animations;
using ReduxSimple.Samples.Extensions;
using SuccincT.Options;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Imaging;
using static Microsoft.Toolkit.Uwp.Helpers.DispatcherHelper;
using static ReduxSimple.Samples.Pokedex.Reducers;
using static ReduxSimple.Samples.Pokedex.Selectors;
using static ReduxSimple.Samples.Pokedex.Effects;
using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples.Pokedex
{
    public sealed partial class PokedexPage : Page
    {
        public static readonly ReduxStore<PokedexState> Store = 
            new ReduxStore<PokedexState>(CreateReducers(), true);

        public PokedexPage()
        {
            InitializeComponent();

            // Reset Store (due to HistoryComponent lifecycle)
            Store.Reset();

            // Observe changes on state
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

            Store.Select(SelectSuggestions, 5)
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

            // Register Effects
            Store.RegisterEffects(
                LoadPokemonList,
                LoadPokemonById,
                SearchPokemon
            );
            
            // Initialize Components
            HistoryComponent.Initialize(Store);

            // Initialize Documentation
            DocumentationComponent.LoadMarkdownFilesAsync("Pokedex");

            ContentGrid.Events().Tapped
                .Subscribe(_ => DocumentationComponent.Collapse());
            DocumentationComponent.ObserveOnExpanded()
                .Subscribe(_ => ContentGrid.Blur(5).Start());
            DocumentationComponent.ObserveOnCollapsed()
                .Subscribe(_ => ContentGrid.Blur(0).Start());

            // Track redux actions
            Store.ObserveAction()
                .Subscribe(action =>
                {
                    bool trackProperties = action.GetType().Name != nameof(GetPokemonListFullfilledAction);
                    TrackReduxAction(action, trackProperties);
                });
        }
    }
}
