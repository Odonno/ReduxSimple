using Microsoft.Toolkit.Uwp.UI;
using Microsoft.Toolkit.Uwp.UI.Animations;
using ReduxSimple.Uwp.DevTools;
using System;
using System.Reactive.Linq;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using static ReduxSimple.Uwp.Samples.App;
using static ReduxSimple.Uwp.Samples.TodoList.Selectors;

namespace ReduxSimple.Uwp.Samples.TodoList
{
    public sealed partial class TodoListPage : Page
    {
        public TodoListPage()
        {
            InitializeComponent();

            // Create backend properties
            var advancedCollectionView = new AdvancedCollectionView();
            advancedCollectionView.SortDescriptions.Add(new SortDescription("Id", SortDirection.Ascending));

            var selectedButtonStyle = App.Current.Resources["SelectedButtonStyle"] as Style;

            // Observe changes on state
            Store.Select(SelectFilter)
                .Subscribe(filter =>
                {
                    switch (filter)
                    {
                        case TodoFilter.All:
                            advancedCollectionView.Filter = (_ => true);
                            break;
                        case TodoFilter.Todo:
                            advancedCollectionView.Filter = (x => !((TodoItem)x).Completed);
                            break;
                        case TodoFilter.Completed:
                            advancedCollectionView.Filter = (x => ((TodoItem)x).Completed);
                            break;
                    }
                    
                    FilterAllButton.Style = (filter == TodoFilter.All) ? selectedButtonStyle : null;
                    FilterTodoButton.Style = (filter == TodoFilter.Todo) ? selectedButtonStyle : null;
                    FilterCompletedButton.Style = (filter == TodoFilter.Completed) ? selectedButtonStyle : null;
                });

            Store.Select(SelectItems)
                .Subscribe(items =>
                {
                    if (TodoItemsListView.ItemsSource != advancedCollectionView)
                        TodoItemsListView.ItemsSource = advancedCollectionView;

                    advancedCollectionView.Source = items;
                });

            // Observe UI events
            FilterAllButton.Events().Click
                .Subscribe(_ => Store.Dispatch(new SetFilterAction { Filter = TodoFilter.All }));
            FilterTodoButton.Events().Click
                .Subscribe(_ => Store.Dispatch(new SetFilterAction { Filter = TodoFilter.Todo }));
            FilterCompletedButton.Events().Click
                .Subscribe(_ => Store.Dispatch(new SetFilterAction { Filter = TodoFilter.Completed }));

            AddNewItemButton.Events().Click
               .Subscribe(_ => Store.Dispatch(new CreateTodoItemAction()));

            // Initialize Documentation
            DocumentationComponent.LoadMarkdownFilesAsync("TodoList");

            GoToGitHubButton.Events().Click
                .Subscribe(async _ =>
                {
                    var uri = new Uri("https://github.com/Odonno/ReduxSimple/tree/master/ReduxSimple.Samples/TodoList");
                    await Launcher.LaunchUriAsync(uri);
                });

            OpenDevToolsButton.Events().Click
                .Subscribe(async _ =>
                {
                    await Store.OpenDevToolsAsync();
                });

            ContentGrid.Events().Tapped
                .Subscribe(_ => DocumentationComponent.Collapse());
            DocumentationComponent.ObserveOnExpanded()
                .Subscribe(_ => ContentGrid.Blur(5).Start());
            DocumentationComponent.ObserveOnCollapsed()
                .Subscribe(_ => ContentGrid.Blur(0).Start());
        }
    }
}
