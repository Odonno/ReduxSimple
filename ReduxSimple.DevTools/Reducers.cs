using static ReduxSimple.Reducers;

namespace ReduxSimple.DevTools;

public static class Reducers
{
    public static IEnumerable<On<DevToolsState>> CreateReducers()
    {
        return new List<On<DevToolsState>>
        {
            On<TogglePlayPauseAction, DevToolsState>(
                state => state with { PlaySessionActive = !state.PlaySessionActive }
            ),
            On<SelectPositionAction, DevToolsState>(
                (state, action) => state with { SelectedActionPosition = action.Position }
            ),
            On<HistoryUpdated, DevToolsState>(
                (state, action) =>
                {
                    bool setPositionToLastAction = state.SelectedActionPosition >= state.CurrentActions.Count - 1;

                    return state with
                    {
                        CurrentActions = action.CurrentActions,
                        FutureActions = action.FutureActions,
                        SelectedActionPosition = setPositionToLastAction && action.CurrentActions != null
                            ? action.CurrentActions.Count - 1
                            : state.SelectedActionPosition
                    };
                }
            )
        };
    }
}
