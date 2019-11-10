using SuccincT.Options;
using System.Collections.Immutable;
using static ReduxSimple.Selectors;

namespace ReduxSimple.DevTools
{
    public static class Selectors
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
}
