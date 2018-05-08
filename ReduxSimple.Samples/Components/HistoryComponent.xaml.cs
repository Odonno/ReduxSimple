using ReduxSimple.Samples.Extensions;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HistoryStore = SuccincT.Unions.Union<ReduxSimple.Samples.Counter.CounterStore, ReduxSimple.Samples.TicTacToe.TicTacToeStore, ReduxSimple.Samples.TodoList.TodoListStore, ReduxSimple.Samples.Pokedex.PokedexStore>;

namespace ReduxSimple.Samples.Components
{
    public sealed partial class HistoryComponent : UserControl
    {
        public HistoryStore Store
        {
            get { return (HistoryStore)GetValue(StoreProperty); }
            set { SetValue(StoreProperty, value); }
        }

        public static readonly DependencyProperty StoreProperty =
            DependencyProperty.Register(nameof(Store), typeof(HistoryStore), typeof(HistoryComponent), new PropertyMetadata(null, StoreChanged));

        private static void StoreChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HistoryComponent component)
            {
                component.Initialize();
            }
        }

        public HistoryComponent()
        {
            InitializeComponent();
        }

        private void Initialize()
        {
            if (Store != null)
            {
                Store.Match()
                    .Case1().Do(s => InitializeFromStore(s))
                    .Case2().Do(s => InitializeFromStore(s))
                    .Case3().Do(s => InitializeFromStore(s))
                    .Case4().Do(s => InitializeFromStore(s))
                    .Exec();
            }
        }

        private void InitializeFromStore<TState>(ReduxStoreWithHistory<TState> store) where TState : class, new()
        {
            // TODO : Create internal ReduxStore instead of backend properties
            // Create backend properties
            var actions = new Stack<object>();
            int maxPosition = 0;
            int currentPosition = 0;
            bool playSessionActive = false;

            // Observe UI events
            UndoButton.ObserveOnClick()
                .Subscribe(_ => store.Undo());
            RedoButton.ObserveOnClick()
                .Subscribe(_ => store.Redo());
            ResetButton.ObserveOnClick()
                .Subscribe(_ => store.Reset());

            PlayPauseButton.ObserveOnClick()
                .Subscribe(_ =>
                {
                    playSessionActive = !playSessionActive;

                    RefreshUI(store.CanUndo, store.CanRedo, currentPosition, maxPosition, playSessionActive);
                });

            Slider.ObserveOnValueChanged()
                .Subscribe(e =>
                {
                    int newPosition = (int)e.EventArgs.NewValue;
                    if (newPosition > currentPosition)
                    {
                        // Redo N times
                        for (int i = 0; i < newPosition - currentPosition; i++)
                        {
                            store.Redo();
                        }
                    }
                    if (newPosition < currentPosition)
                    {
                        // Undo N times
                        for (int i = 0; i < currentPosition - newPosition; i++)
                        {
                            store.Undo();
                        }
                    }
                });

            // Observe changes on state
            store.ObserveAction()
                .ObserveOnDispatcher()
                .Subscribe(action =>
                {
                    actions.Push(action);
                    currentPosition++;
                    if (!store.CanRedo)
                    {
                        maxPosition = currentPosition;
                    }

                    if (playSessionActive && !store.CanRedo)
                    {
                        playSessionActive = false;
                    }

                    RefreshUI(store.CanUndo, store.CanRedo, currentPosition, maxPosition, playSessionActive);
                });

            store.ObserveUndoneAction()
                .ObserveOnDispatcher()
                .Subscribe(_ =>
                {
                    currentPosition--;

                    RefreshUI(store.CanUndo, store.CanRedo, currentPosition, maxPosition, playSessionActive);
                });

            store.ObserveReset()
                .ObserveOnDispatcher()
                .Subscribe(_ =>
                {
                    actions.Clear();
                    maxPosition = 0;
                    currentPosition = 0;

                    RefreshUI(store.CanUndo, store.CanRedo, currentPosition, maxPosition, playSessionActive);
                });

            Observable.Interval(TimeSpan.FromSeconds(1))
                .ObserveOnDispatcher()
                .Where(_ => playSessionActive)
                .Subscribe(_ =>
                {
                    store.Redo();
                });

            // Initialize UI
            RefreshUI(store.CanUndo, store.CanRedo, currentPosition, maxPosition, playSessionActive);
        }

        private void RefreshUI(bool canUndoAction, bool canRedoAction, int currentPosition, int maxPosition, bool playSessionActive)
        {
            Slider.Maximum = maxPosition;
            Slider.Value = currentPosition;

            if (playSessionActive)
            {
                UndoButton.IsEnabled = false;
                RedoButton.IsEnabled = false;
                ResetButton.IsEnabled = false;
                PlayPauseButton.IsEnabled = true;

                Slider.IsEnabled = false;

                PlayPauseButton.Content = "\xE769";
            }
            else
            {
                UndoButton.IsEnabled = canUndoAction;
                RedoButton.IsEnabled = canRedoAction;
                ResetButton.IsEnabled = canUndoAction || canRedoAction;
                PlayPauseButton.IsEnabled = canRedoAction;

                Slider.IsEnabled = maxPosition > 0;

                PlayPauseButton.Content = "\xE768";
            }
        }
    }
}
