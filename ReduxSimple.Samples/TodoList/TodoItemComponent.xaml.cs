using ReduxSimple.Samples.Extensions;
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
            CompleteButton.ObserveOnClick()
                .Subscribe(_ => Store.Dispatch(new CompleteTodoItemAction { Id = TodoItem.Id }));

            RemoveButton.ObserveOnClick()
                .Subscribe(_ => Store.Dispatch(new RemoveTodoItemAction { Id = TodoItem.Id }));

            TextBox.ObserveOnLostFocus()
                .Subscribe(e => Store.Dispatch(new UpdateTodoItemAction { Id = TodoItem.Id, Content = TextBox.Text }));
        }

        private void Initialize()
        {
            if (TodoItem != null)
            {
                // Initialize UI
                TextBox.Text = TodoItem.Content ?? string.Empty;
            }
        }
    }
}
