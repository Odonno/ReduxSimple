using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace ReduxSimple.Tests
{
    public class EffectsActionOrderTest
    {
        private readonly ITestOutputHelper _output;
        private readonly List<string> _trackedActions = new List<string>();
        
        public EffectsActionOrderTest(ITestOutputHelper output)
        {
            _output = output;
        }
        
        [Fact]
        public async Task EffectsSeeActionsInCorrectOrder()
        {
            ReduxStore<RootState> store = new ReduxStore<RootState>(CreateReducers(), RootState.InitialState, 
                enableTimeTravel: false);

            store.RegisterEffects(
                GetIncRandomActionEffect(),
                TrackActionEffect());

            _output.WriteLine($"Main thread: {Thread.CurrentThread.ManagedThreadId}");
            
            store.Dispatch(new IncRandomAction());

            await Task.Delay(100);
            
            _trackedActions.ShouldBe(new []
            {
                "IncRandomAction",
                "IncRandomFulfilledAction"
            });
        }

        private IEnumerable<On<RootState>> CreateReducers()
        {
            yield return Reducers.On<IncRandomAction, RootState>(
                (state, _) => state);
            
            yield return Reducers.On<IncRandomFulfilledAction, RootState>(
                (state, action) => state.WithCounter(action.RandomValue));
        }

        private Effect<RootState> GetIncRandomActionEffect() =>
            Effects.CreateEffect<RootState>(
                store => store.ObserveAction<IncRandomAction>()
                    .Do(_ => _output.WriteLine($"GetIncRandomActionEffect on thread {Thread.CurrentThread.ManagedThreadId}"))
                    .Select(_ => new IncRandomFulfilledAction {RandomValue = new Random().Next()}),
                dispatch: true);

        private Effect<RootState> TrackActionEffect() =>
            Effects.CreateEffect<RootState>(
                store => store.ObserveAction()
                    .Do(action =>
                    {
                        string actionType = action.GetType().Name;
                        _output.WriteLine($"TrackActionEffect with action {actionType} on thread {Thread.CurrentThread.ManagedThreadId}");
                        _trackedActions.Add(actionType);
                    }),
                false);
        
        class RootState
        {
            int Counter { get; set; }

            public static RootState InitialState => new RootState {Counter = 0};

            public RootState WithCounter(int val)
            {
                return new RootState {Counter = val};
            }
        }

        class IncRandomAction
        {
        }

        class IncRandomFulfilledAction
        {
            public int RandomValue { get; set; }
        }
    }
}