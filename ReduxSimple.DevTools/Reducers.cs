using Converto;
using System.Collections.Generic;
using static ReduxSimple.Reducers;

namespace ReduxSimple.DevTools
{
    public static class Reducers
    {
        public static IEnumerable<On<DevToolsState>> CreateReducers()
        {
            return new List<On<DevToolsState>>
            {
                On<TogglePlayPauseAction, DevToolsState>(
                    state => state.With(new { PlaySessionActive = !state.PlaySessionActive })
                ),
                On<SelectPositionAction, DevToolsState>(
                    (state, action) => state.With(new { SelectedActionPosition = action.Position })
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
                                SelectedActionPosition = setPositionToLastAction && action.CurrentActions != null
                                    ? action.CurrentActions.Count - 1
                                    : state.SelectedActionPosition
                            }
                        );
                    }
                )
            };
        }
    }
}
