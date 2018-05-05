using Microsoft.Toolkit.Uwp.UI;
using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ReduxSimple.Samples.TodoList
{
    public sealed partial class TodoListPage : Page
    {
        public static TodoListStore Store = new TodoListStore();

        public TodoListPage()
        {
            InitializeComponent();

            // Create backend properties
            var advancedCollectionView = new AdvancedCollectionView();
            advancedCollectionView.SortDescriptions.Add(new SortDescription("Id", SortDirection.Ascending));

            // Observe changes on state
            Store.ObserveState(state => state.Filter)
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
                });

            Store.ObserveState(state => state.Items)
                .Subscribe(items =>
                {
                    advancedCollectionView.Source = items;
                });

            // Observe UI events
            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => FilterAllButton.Click += h,
                h => FilterAllButton.Click -= h
            )
                .Subscribe(e =>
                {
                    Store.Dispatch(new SetFilterAction { Filter = TodoFilter.All });
                });
            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => FilterTodoButton.Click += h,
                h => FilterTodoButton.Click -= h
            )
                .Subscribe(e =>
                {
                    Store.Dispatch(new SetFilterAction { Filter = TodoFilter.Todo });
                });
            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => FilterCompletedButton.Click += h,
                h => FilterCompletedButton.Click -= h
            )
                .Subscribe(e =>
                {
                    Store.Dispatch(new SetFilterAction { Filter = TodoFilter.Completed });
                });

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
               h => AddNewItemButton.Click += h,
               h => AddNewItemButton.Click -= h
           )
               .Subscribe(e =>
               {
                   Store.Dispatch(new CreateTodoItemAction());
               });

            // Initialize UI
            TodoItemsListView.ItemsSource = advancedCollectionView;
        }
    }
}
