using Microsoft.Toolkit.Uwp.UI.Animations;
using ReduxSimple.Uwp.Samples.Extensions;
using SuccincT.Options;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Imaging;
using static ReduxSimple.Selectors;
using static ReduxSimple.Uwp.Samples.App;
using static ReduxSimple.Uwp.Samples.Pokedex.Selectors;
using static ReduxSimple.Uwp.Samples.Pokedex.Effects;

namespace ReduxSimple.Uwp.Samples.Pokedex
{
    public sealed partial class PokedexPage : Page
    {
        public PokedexPage()
        {
            InitializeComponent();

            // Observe changes on state
            Store.Select(
                CombineSelectors(SelectLoading, SelectIsPokedexEmpty)
            )
               .ObserveOnDispatcher()
               .Subscribe(x =>
               {
                   var (loading, isPokedexEmpty) = x;

                   OpenPokedexButton.ShowIf(!loading && isPokedexEmpty);

                   GlobalLoadingProgressRing.IsActive = loading && isPokedexEmpty;
                   GlobalLoadingProgressRing.ShowIf(loading && isPokedexEmpty);
                   RootStackPanel.ShowIf(!isPokedexEmpty);
               });

            Store.Select(SelectSuggestions, 5)
                .ObserveOnDispatcher()
                .Subscribe(suggestions =>
                {
                    AutoSuggestBox.ItemsSource = suggestions;
                });

            Store.Select(SelectPokemon)
                .ObserveOnDispatcher()
                .Subscribe(pokemon =>
                {
                    PokemonPanel.ShowIf(pokemon.HasValue);
                    PokemonIdTextBlock.Text = pokemon.HasValue ? $"#{pokemon.Value.Id}" : string.Empty;
                    PokemonNameTextBlock.Text = pokemon.HasValue ? pokemon.Value.Name : string.Empty;
                    PokemonImage.Source = pokemon.HasValue ? new BitmapImage(new Uri(pokemon.Value.Image)) : null;
                });

            Store.Select(SelectErrors)
                .ObserveOnDispatcher()
                .Subscribe(errors =>
                {
                    ErrorsListView.ItemsSource = errors;
                });

            // Observe UI events
            OpenPokedexButton.Events().Click
               .ObserveOnDispatcher()
               .Subscribe(_ => Store.Dispatch(new GetPokemonListAction()));

            AutoSuggestBox.Events().TextChanged
               .ObserveOnDispatcher()
               .Subscribe(_ =>
               {
                    Store.Dispatch(new UpdateSearchStringAction { Search = AutoSuggestBox.Text });
               });

            AutoSuggestBox.Events().SuggestionChosen
                .ObserveOnDispatcher()
                .Subscribe(e =>
                {
                    var selectedPokemonOption = (e.SelectedItem as PokemonGeneralInfo).ToOption();
                    if (selectedPokemonOption.HasValue)
                    {
                        AutoSuggestBox.Text = selectedPokemonOption.Value.Name; // Avoid the automatic change of the Text property of SuggestBox
                        Store.Dispatch(new UpdateSearchStringAction { Search = selectedPokemonOption.Value.Name });
                    }
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
        }
    }
}
