using System;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Reactive.Linq;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Controls;
using System.Collections.Immutable;
using System.Linq;
using Newtonsoft.Json;
using SuccincT.JSON;
using ReduxSimple.DevTools;
using static ReduxSimple.Selectors;
using static ReduxSimple.DevTools.Selectors;
using static ReduxSimple.DevTools.Reducers;

namespace ReduxSimple.Uwp.DevTools
{
    public sealed partial class DevToolsComponent : Page
    {
        private readonly ReduxStore<DevToolsState> _devToolsStore = new ReduxStore<DevToolsState>(CreateReducers());

        public DevToolsComponent()
        {
            InitializeComponent();

            PageNameTextBlock.Text = "Redux DevTools - " + SystemInformation.ApplicationName;
        }

        // TODO : Should be private/internal
        public void Initialize<TState>(ReduxStore<TState> store) where TState : class, new()
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
                .Select(e => (int)e.NewValue)
                .DistinctUntilChanged()
                .Subscribe(newPosition =>
                {
                    _devToolsStore.Dispatch(new MoveToPositionAction { Position = newPosition });
                });

            ReduxActionInfosListView.Events().ItemClick
                .Subscribe(e =>
                {
                    int index = ReduxActionInfosListView.Items.IndexOf(e.ClickedItem);
                    _devToolsStore.Dispatch(new SelectPositionAction { Position = index });
                });

            // Observe changes on DevTools state
            Observable.CombineLatest(
                _devToolsStore.Select(SelectCurrentPosition),
                _devToolsStore.Select(SelectPlaySessionActive),
                _devToolsStore.Select(SelectMaxPosition),
                store.ObserveCanUndo(),
                store.ObserveCanRedo(),
                Tuple.Create
            )
                .Subscribe(x =>
                {
                    var (currentPosition, playSessionActive, maxPosition, canUndo, canRedo) = x;

                    Slider.Value = currentPosition;
                    Slider.Maximum = maxPosition;

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

            _devToolsStore.Select(
                CombineSelectors(SelectCurrentActions, SelectSelectedActionPosition)
            )
                .Subscribe(x =>
                {
                    var (actions, selectedPosition) = x;

                    ReduxActionInfosListView.ItemsSource = actions;
                    ReduxActionInfosListView.SelectedIndex = Math.Clamp(selectedPosition, -1, actions.Count - 1);
                });

            _devToolsStore.Select(SelectSelectedReduxAction)
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

                            SelectedReduxActionDataTextBlock.Text = JsonConvert.SerializeObject(
                                reduxAction.Data,
                                serializerSettings
                            );
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
                    _devToolsStore.Select(SelectCurrentPosition),
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

            _devToolsStore.Select(SelectPlaySessionActive)
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
                        _devToolsStore.Dispatch(new TogglePlayPauseAction());
                    }
                });

            // Observe changes on listened state
            var storeHistoryAtInitialization = store.GetHistory();

            store.ObserveHistory()
                .StartWith(storeHistoryAtInitialization)
                .Subscribe(historyInfos =>
                {
                    var mementosOrderedByDate = historyInfos.PreviousStates
                        .OrderBy(reduxMemento => reduxMemento.Date)
                        .ToList();

                    // Set list of current actions
                    // Set list of future (undone) actions
                    _devToolsStore.Dispatch(new HistoryUpdated
                    {
                        CurrentActions = mementosOrderedByDate
                            .Select((reduxMemento, index) =>
                            {
                                int nextIndex = index + 1;
                                var nextState = nextIndex < mementosOrderedByDate.Count
                                    ? mementosOrderedByDate[nextIndex].PreviousState
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

            _devToolsStore.Dispatch(
                new SelectPositionAction { Position = storeHistoryAtInitialization.PreviousStates.Count - 1 }
            );
        }
    }
}
