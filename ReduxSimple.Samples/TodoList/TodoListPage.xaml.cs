using Microsoft.Toolkit.Uwp.UI;
using ReduxSimple.Samples.Extensions;
using System;
using System.Reactive.Linq;
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
            FilterAllButton.ObserveOnClick()
                .Subscribe(_ => Store.Dispatch(new SetFilterAction { Filter = TodoFilter.All }));
            FilterTodoButton.ObserveOnClick()
                .Subscribe(_ => Store.Dispatch(new SetFilterAction { Filter = TodoFilter.Todo }));
            FilterCompletedButton.ObserveOnClick()
                .Subscribe(_ => Store.Dispatch(new SetFilterAction { Filter = TodoFilter.Completed }));

            AddNewItemButton.ObserveOnClick()
               .Subscribe(_ => Store.Dispatch(new CreateTodoItemAction()));

            // Initialize UI
            TodoItemsListView.ItemsSource = advancedCollectionView;
        }
    }
}
