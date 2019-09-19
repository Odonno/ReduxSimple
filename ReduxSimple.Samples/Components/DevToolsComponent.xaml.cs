using Converto;
using Newtonsoft.Json;
using SuccincT.Options;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace ReduxSimple.Samples.Components
{
    public sealed partial class DevToolsComponent : Page
    {
        private class ReduxActionInfo
        {
            public Type Type { get; set; }
            public object Data { get; set; }
        }

        private class HistoryComponentState
        {
            public ImmutableList<object> CurrentActions { get; set; } = ImmutableList<object>.Empty;
            public ImmutableList<object> FutureActions { get; set; } = ImmutableList<object>.Empty;
            public int MaxPosition { get; set; } = 0;
            public int CurrentPosition { get; set; } = 0;
            public bool PlaySessionActive { get; set; } = false;
            public Option<ReduxActionInfo> SelectedReduxActionInfo { get; set; } = Option<ReduxActionInfo>.None();
        }

        private class HistoryComponentStore : ReduxStore<HistoryComponentState>
        {
            protected override HistoryComponentState Reduce(HistoryComponentState state, object action)
            {
                if (action is GoBackAction)
                {
                    var lastAction = state.CurrentActions.Last();

                    return state.With(new
                    {
                        CurrentActions = state.CurrentActions.Remove(lastAction),
                        FutureActions = state.FutureActions.Add(lastAction),
                        CurrentPosition = state.CurrentPosition - 1
                    });
                }
                if (action is GoForwardAction goForwardAction)
                {
                    var futureActionOption = state.FutureActions.TryLast();

                    if (futureActionOption.HasValue && !goForwardAction.BreaksTimeline)
                    {
                        // Continue on existing timeline
                        var futureAction = futureActionOption.Value;
                        return state.With(new
                        {
                            CurrentActions = state.CurrentActions.Add(futureAction),
                            FutureActions = state.FutureActions.Remove(futureAction),
                            CurrentPosition = state.CurrentPosition + 1
                        });
                    }
                    else
                    {
                        // Create a new timeline
                        return state.With(new
                        {
                            CurrentActions = state.CurrentActions.Add(goForwardAction.Action),
                            FutureActions = ImmutableList<object>.Empty,
                            MaxPosition = state.CurrentActions.Count + 1,
                            CurrentPosition = state.CurrentPosition + 1
                        });
                    }
                }
                if (action is ResetAction)
                {
                    return state.With(new
                    {
                        CurrentActions = ImmutableList<object>.Empty,
                        FutureActions = ImmutableList<object>.Empty,
                        MaxPosition = 0,
                        CurrentPosition = 0
                    });
                }
                if (action is TogglePlayPauseAction)
                {
                    return state.With(new { PlaySessionActive = !state.PlaySessionActive });
                }
                if (action is SelectReduxActionInfoAction selectReduxActionInfoAction)
                {
                    return state.With(new
                    {
                        SelectedReduxActionInfo = Option<ReduxActionInfo>.Some(selectReduxActionInfoAction.ActionInfo)
                    });
                }
                return base.Reduce(state, action);
            }
        }

        private class GoBackAction { }
        private class GoForwardAction
        {
            public object Action { get; set; }
            public bool BreaksTimeline { get; set; }
        }
        private class ResetAction { }
        private class MoveToPositionAction
        {
            public int Position { get; set; }
        }
        private class TogglePlayPauseAction { }

        private class SelectReduxActionInfoAction
        {
            public ReduxActionInfo ActionInfo { get; set; }
        }

        private readonly HistoryComponentStore _internalStore = new HistoryComponentStore();

        public DevToolsComponent()
        {
            InitializeComponent();
        }

        public void Initialize<TState>(ReduxStoreWithHistory<TState> store) where TState : class, new()
        {
            // Observe UI events
            UndoButton.Events().Click
                .Subscribe(_ => store.Undo());
            RedoButton.Events().Click
                .Subscribe(_ => store.Redo());
            ResetButton.Events().Click
                .Subscribe(_ => store.Reset());

            PlayPauseButton.Events().Click
                .Subscribe(_ => _internalStore.Dispatch(new TogglePlayPauseAction()));

            Slider.Events().ValueChanged
                .Where(_ => Slider.FocusState != FocusState.Unfocused)
                .Subscribe(e =>
                {
                    int newPosition = (int)e.NewValue;
                    _internalStore.Dispatch(new MoveToPositionAction { Position = newPosition });
                });

            // Observe changes on internal state
            _internalStore.Select(state => state.MaxPosition)
                .Subscribe(maxPosition =>
                {
                    Slider.Maximum = maxPosition;
                });

            Observable.CombineLatest(
                _internalStore.Select(state => state.CurrentPosition),
                _internalStore.Select(state => state.PlaySessionActive),
                _internalStore.Select(state => state.MaxPosition),
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

            _internalStore.ObserveAction<MoveToPositionAction>()
                .Subscribe(a =>
                {
                    if (a.Position < _internalStore.State.CurrentPosition)
                    {
                        for (int i = 0; i < _internalStore.State.CurrentPosition - a.Position; i++)
                        {
                            store.Undo();
                        }
                    }
                    if (a.Position > _internalStore.State.CurrentPosition)
                    {
                        for (int i = 0; i < a.Position - _internalStore.State.CurrentPosition; i++)
                        {
                            store.Redo();
                        }
                    }
                });

            _internalStore.Select(state => state.CurrentActions)
                .Subscribe(actions =>
                {
                    ReduxActionInfosListView.ItemsSource = actions.Select(action =>
                    {
                        return new ReduxActionInfo
                        {
                            Type = action.GetType(),
                            Data = action
                        };
                    });
                });

            _internalStore.Select(state => state.SelectedReduxActionInfo)
                .Subscribe(reduxActionOption =>
                {
                    reduxActionOption.Match()
                        .Some().Do(reduxAction =>
                        {
                            SelectedReduxActionTypeTextBlock.Text = reduxAction.Type.Name;
                            SelectedReduxActionDataTextBlock.Text = JsonConvert.SerializeObject(reduxAction.Data, Formatting.Indented);

                            var state = store.FindStateFromAction(reduxAction.Data);
                            var jsonState = JsonConvert.SerializeObject(state, Formatting.Indented);

                            SelectedStateTextBlock.Text = jsonState;

                            var previousState = store.FindPreviousState(state);

                            // Find diffs between selected state and previous state
                            // TODO : use new/replace/removed pattern (green/red updates)
                            SelectedDiffStateTextBlock.Text = previousState == null
                                ? jsonState
                                : JsonConvert.SerializeObject(
                                    FindDiffs(previousState, state),
                                    Formatting.Indented
                                );
                        })
                        .None().Do(() =>
                        {
                            SelectedReduxActionTypeTextBlock.Text = string.Empty;
                            SelectedReduxActionDataTextBlock.Text = string.Empty;
                            SelectedStateTextBlock.Text = string.Empty;
                            SelectedDiffStateTextBlock.Text = string.Empty;
                        })
                    .Exec();
                });

            // Observe UI events
            ReduxActionInfosListView.Events().ItemClick
                .Subscribe(@event =>
                {
                    var reduxActionInfo = @event.ClickedItem as ReduxActionInfo;
                    _internalStore.Dispatch(new SelectReduxActionInfoAction
                    {
                        ActionInfo = reduxActionInfo
                    });
                });

            // Observe changes on listened state
            var goForwardNormalActionOrigin = store.ObserveAction(ActionOriginFilter.Normal)
                .Select(action => new { Action = action, BreaksTimeline = true });
            var goForwardRedoneActionOrigin = store.ObserveAction(ActionOriginFilter.Redone)
                .Select(action => new { Action = action, BreaksTimeline = false });

            goForwardNormalActionOrigin.Merge(goForwardRedoneActionOrigin)
                .ObserveOnDispatcher()
                .Subscribe(x =>
                {
                    _internalStore.Dispatch(new GoForwardAction { Action = x.Action, BreaksTimeline = x.BreaksTimeline });
                    if (_internalStore.State.PlaySessionActive && !store.CanRedo)
                    {
                        _internalStore.Dispatch(new TogglePlayPauseAction());
                    }
                });

            store.ObserveUndoneAction()
                .ObserveOnDispatcher()
                .Subscribe(_ => _internalStore.Dispatch(new GoBackAction()));

            store.ObserveReset()
                .ObserveOnDispatcher()
                .Subscribe(_ => _internalStore.Dispatch(new ResetAction()));

            // TODO : Effect
            _internalStore.Select(state => state.PlaySessionActive)
                .Select(playSessionActive =>
                    playSessionActive ? Observable.Interval(TimeSpan.FromSeconds(1)) : Observable.Empty<long>()
                )
                .Switch()
                .ObserveOnDispatcher()
                .Subscribe(_ =>
                {
                    bool canRedo = store.Redo();
                    if (!canRedo)
                    {
                        _internalStore.Dispatch(new TogglePlayPauseAction());
                    }
                });
        }

        private static Dictionary<string, object> FindDiffs<TState>(TState state1, TState state2)
            where TState : class, new()
        {
            // TODO
            return new Dictionary<string, object>();
        }
    }
}
