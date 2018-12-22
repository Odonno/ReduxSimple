This example contains 2 elements.

## The main page

```csharp
public sealed partial class TodoListPage : Page
{
    public static TodoListStore Store = new TodoListStore();

    public TodoListPage()
    {
        InitializeComponent();

        // Create backend properties
        var advancedCollectionView = new AdvancedCollectionView();
        advancedCollectionView.SortDescriptions.Add(new SortDescription("Id", SortDirection.Ascending));

        var selectedButtonStyle = App.Current.Resources["SelectedButtonStyle"] as Style;

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
                
                FilterAllButton.Style = (filter == TodoFilter.All) ? selectedButtonStyle : null;
                FilterTodoButton.Style = (filter == TodoFilter.Todo) ? selectedButtonStyle : null;
                FilterCompletedButton.Style = (filter == TodoFilter.Completed) ? selectedButtonStyle : null;
            });

        Store.ObserveState(state => state.Items)
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
    }
}
```

## The Todo Item user control


```csharp
public sealed partial class TodoItemComponent : UserControl
{
    public TodoItem TodoItem
    {
        get { return (TodoItem)GetValue(TodoItemProperty); }
        set { SetValue(TodoItemProperty, value); }
    }

    public static readonly DependencyProperty TodoItemProperty =
        DependencyProperty.Register(nameof(TodoItem), typeof(TodoItem), typeof(TodoItemComponent), new PropertyMetadata(null, TodoItemChanged));

    private static void TodoItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TodoItemComponent component)
        {
            component.Initialize();
        }
    }

    public TodoItemComponent()
    {
        InitializeComponent();

        // Observe UI events
        CompleteButton.Events().Click
            .Subscribe(_ => Store.Dispatch(new CompleteTodoItemAction { Id = TodoItem.Id }));

        RevertCompleteButton.Events().Click
            .Subscribe(_ => Store.Dispatch(new RevertCompleteTodoItemAction { Id = TodoItem.Id }));

        RemoveButton.Events().Click
            .Subscribe(_ => Store.Dispatch(new RemoveTodoItemAction { Id = TodoItem.Id }));

        TextBox.Events().LostFocus
            .Subscribe(e => Store.Dispatch(new UpdateTodoItemAction { Id = TodoItem.Id, Content = TextBox.Text }));
    }

    private void Initialize()
    {
        if (TodoItem != null)
        {
            // Initialize UI
            TextBox.Text = TodoItem.Content ?? string.Empty;

            CompleteButton.HideIf(TodoItem.Completed);
            RevertCompleteButton.ShowIf(TodoItem.Completed);
        }
    }
}
```