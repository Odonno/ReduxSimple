using System;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Reactive.Linq;
using SuccincT.Options;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Controls;
using System.Collections.Immutable;
using Converto;
using System.Linq;
using Newtonsoft.Json;
using SuccincT.JSON;
using static ReduxSimple.Selectors;
using static ReduxSimple.Reducers;

namespace ReduxSimple.Uwp.Samples.Components
{
    public sealed partial class DevToolsComponent : Page
    {
        private class ReduxActionInfo
        {
            public DateTime? Date { get; set; }
            public Type Type { get; set; }
            public object Data { get; set; }
            public object PreviousState { get; set; }
            public object NextState { get; set; }
        }

        private class DevToolsState
        {
            public ImmutableList<ReduxActionInfo> CurrentActions { get; set; } = ImmutableList<ReduxActionInfo>.Empty;
            public ImmutableList<ReduxActionInfo> FutureActions { get; set; } = ImmutableList<ReduxActionInfo>.Empty;
            public int SelectedActionPosition { get; set; } = 0;
            public bool PlaySessionActive { get; set; } = false;
        }

        private static class Selectors
        {
            public static ISelectorWithoutProps<DevToolsState, ImmutableList<ReduxActionInfo>> SelectCurrentActions = CreateSelector(
                (DevToolsState state) => state.CurrentActions
            );
            public static ISelectorWithoutProps<DevToolsState, ImmutableList<ReduxActionInfo>> SelectFutureActions = CreateSelector(
                (DevToolsState state) => state.FutureActions
            );
            public static ISelectorWithoutProps<DevToolsState, int> SelectCurrentPosition = CreateSelector(
                SelectCurrentActions,
                SelectFutureActions,
                (currentActions, futureActions) => currentActions.Count - 1
            );
            public static ISelectorWithoutProps<DevToolsState, int> SelectMaxPosition = CreateSelector(
                SelectCurrentActions,
                SelectFutureActions,
                (currentActions, futureActions) => currentActions.Count + futureActions.Count - 1
            );
            public static ISelectorWithoutProps<DevToolsState, int> SelectSelectedActionPosition = CreateSelector(
                (DevToolsState state) => state.SelectedActionPosition
            );
            public static ISelectorWithoutProps<DevToolsState, bool> SelectPlaySessionActive = CreateSelector(
                (DevToolsState state) => state.PlaySessionActive
            );

            public static ISelectorWithoutProps<DevToolsState, Option<ReduxActionInfo>> SelectSelectedReduxAction = CreateSelector(
                SelectCurrentActions,
                SelectSelectedActionPosition,
                (currentActions, selectedActionPosition) =>
                {
                    if (selectedActionPosition < 0 || selectedActionPosition >= currentActions.Count)
                    {
                        return Option<ReduxActionInfo>.None();
                    }
                    return currentActions[selectedActionPosition].ToOption();
                }
            );
        }

        private static class Reducers
        {
            public static IEnumerable<On<DevToolsState>> CreateReducers()
            {
                return new List<On<DevToolsState>>
                {
                    On<TogglePlayPauseAction, DevToolsState>(
                        state => state.With(new { PlaySessionActive = !state.PlaySessionActive })
                    ),
                    On<HistoryUpdated, DevToolsState>(
                        (state, action) =>
                        {
                            bool setPositionToLastAction = state.SelectedActionPosition >= state.CurrentActions.Count - 1;

                            return state.With(
                                new
                                {
                                    action.CurrentActions,
                                    action.FutureActions,
                                    CurrentPosition = setPositionToLastAction
                                        ? action.CurrentActions.Count - 1
                                        : state.SelectedActionPosition
                                }
                            );
                        }
                    )
                };
            }
        }

        private class HistoryUpdated
        {
            public ImmutableList<ReduxActionInfo> CurrentActions { get; set; }
            public ImmutableList<ReduxActionInfo> FutureActions { get; set; }
        }
        private class MoveToPositionAction
        {
            public int Position { get; set; }
        }
        private class TogglePlayPauseAction { }

        private readonly ReduxStore<DevToolsState> _devToolsStore = new ReduxStore<DevToolsState>(Reducers.CreateReducers());

        public DevToolsComponent()
        {
            InitializeComponent();

            PageNameTextBlock.Text = "Redux DevTools - " + SystemInformation.ApplicationName;
        }

        internal void Initialize<TState>(ReduxStore<TState> store) where TState : class, new()
        {
            // Observe UI events
            UndoButton.Events().Click
                .Subscribe(_ => store.Undo());
            RedoButton.Events().Click
                .Subscribe(_ => store.Redo());
            ResetButton.Events().Click
                .Subscribe(_ => store.Reset());

            PlayPauseButton.Events().Click
                .Subscribe(_ => _devToolsStore.Dispatch(new TogglePlayPauseAction()));

            Slider.Events().ValueChanged
                .Where(_ => Slider.FocusState != Windows.UI.Xaml.FocusState.Unfocused)
                .Subscribe(e =>
                {
                    int newPosition = (int)e.NewValue;
                    _devToolsStore.Dispatch(new MoveToPositionAction { Position = newPosition });
                });

            // Observe changes on DevTools state
            _devToolsStore.Select(Selectors.SelectMaxPosition)
                .Subscribe(maxPosition =>
                {
                    Slider.Maximum = maxPosition;
                });

            Observable.CombineLatest(
                _devToolsStore.Select(Selectors.SelectCurrentPosition),
                _devToolsStore.Select(Selectors.SelectPlaySessionActive),
                _devToolsStore.Select(Selectors.SelectMaxPosition),
                store.ObserveCanUndo(),
                store.ObserveCanRedo(),
                Tuple.Create
            )
                .Subscribe(x =>
                {
                    var (currentPosition, playSessionActive, maxPosition, canUndo, canRedo) = x;

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
                        UndoButton.IsEnabled = canUndo;
                        RedoButton.IsEnabled = canRedo;
                        ResetButton.IsEnabled = canUndo || canRedo;
                        PlayPauseButton.IsEnabled = canRedo;
                        Slider.IsEnabled = maxPosition > 0;
                        PlayPauseButton.Content = "\xE768";
                    }
                });

            _devToolsStore.Select(Selectors.SelectCurrentActions)
                .Subscribe(actions =>
                {
                    ReduxActionInfosListView.ItemsSource = actions.OrderBy(a => a.Date);
                });

            _devToolsStore.Select(Selectors.SelectSelectedReduxAction)
                .Subscribe(reduxActionOption =>
                {
                    reduxActionOption.Match()
                        .Some().Do(reduxAction =>
                        {
                            var serializerSettings = new JsonSerializerSettings
                            {
                                ContractResolver = SuccinctContractResolver.Instance,
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                Formatting = Formatting.Indented
                            };

                            //SelectedReduxActionDataTextBlock.Text = JsonConvert.SerializeObject(
                            //    reduxAction.Data, 
                            //    serializerSettings
                            //);
                            SelectedStateTextBlock.Text = JsonConvert.SerializeObject(
                                reduxAction.NextState,
                                serializerSettings
                            );
                            SelectedDiffStateTextBlock.Text = "This feature will be available soon...";
                        })
                        .None().Do(() =>
                        {
                            SelectedReduxActionDataTextBlock.Text = string.Empty;
                            SelectedStateTextBlock.Text = string.Empty;
                            SelectedDiffStateTextBlock.Text = string.Empty;
                        })
                        .Exec();
                });

            _devToolsStore.ObserveAction<MoveToPositionAction>()
                .WithLatestFrom(
                    _devToolsStore.Select(Selectors.SelectCurrentPosition),
                    Tuple.Create
                )
                .Subscribe(x =>
                {
                    var (action, currentPosition) = x;

                    if (action.Position < currentPosition)
                    {
                        for (int i = 0; i < currentPosition - action.Position; i++)
                        {
                            store.Undo();
                        }
                    }
                    if (action.Position > currentPosition)
                    {
                        for (int i = 0; i < action.Position - currentPosition; i++)
                        {
                            store.Redo();
                        }
                    }
                });

            // Observe changes on listened state
            store.ObserveHistory()
                .StartWith(store.GetHistory())
                .Subscribe(historyInfos =>
                {
                    // Set list of current actions
                    // Set list of future (undone) actions
                    _devToolsStore.Dispatch(new HistoryUpdated
                    {
                        CurrentActions = historyInfos.PreviousStates
                            .Select((reduxMemento, index) =>
                            {
                                int nextIndex = index + 1;
                                var nextState = nextIndex < historyInfos.PreviousStates.Count
                                    ? historyInfos.PreviousStates[nextIndex].PreviousState
                                    : store.State;

                                return new ReduxActionInfo
                                {
                                    Date = reduxMemento.Date,
                                    Type = reduxMemento.Action.GetType(),
                                    Data = reduxMemento.Action,
                                    PreviousState = reduxMemento.PreviousState,
                                    NextState = nextState
                                };
                            })
                            .ToImmutableList(),
                        FutureActions = historyInfos.FutureActions
                            .Select(action =>
                            {
                                return new ReduxActionInfo
                                {
                                    Type = action.GetType(),
                                    Data = action
                                };
                            })
                            .ToImmutableList()
                    });
                });
        }
    }
}
