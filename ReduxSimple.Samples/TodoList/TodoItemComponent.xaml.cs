using System;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using static ReduxSimple.Samples.TodoList.TodoListPage;

namespace ReduxSimple.Samples.TodoList
{
    public sealed partial class TodoItemComponent : UserControl
    {
        public TodoItem TodoItem
        {
            get { return (TodoItem)GetValue(TodoItemProperty); }
            set { SetValue(TodoItemProperty, value); }
        }

        public static readonly DependencyProperty TodoItemProperty =
            DependencyProperty.Register("TodoItem", typeof(TodoItem), typeof(TodoItemComponent), new PropertyMetadata(null, TodoItemChanged));

        private static void TodoItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TodoItemComponent component)
            {
                var todoItem = e.NewValue as TodoItem;
                component.InitializeUI();
            }
        }

        public TodoItemComponent()
        {
            InitializeComponent();

            // Observe UI events
            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => CompleteButton.Click += h,
                h => CompleteButton.Click -= h
            )
                .Subscribe(e =>
                {
                    Store.Dispatch(new CompleteTodoItemAction { Id = TodoItem.Id });
                });

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => RemoveButton.Click += h,
                h => RemoveButton.Click -= h
            )
                .Subscribe(e =>
                {
                    Store.Dispatch(new RemoveTodoItemAction { Id = TodoItem.Id });
                });

            Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                h => TextBox.LostFocus += h,
                h => TextBox.LostFocus -= h
            )
                .Subscribe(e =>
                {
                    Store.Dispatch(new UpdateTodoItemAction { Id = TodoItem.Id, Content = TextBox.Text });
                });
        }

        private void InitializeUI()
        {
            if (TodoItem != null)
            {
                // Initialize UI
                TextBox.Text = TodoItem.Content ?? string.Empty;
            }
        }
    }
}
