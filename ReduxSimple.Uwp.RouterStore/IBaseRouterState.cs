namespace ReduxSimple.Uwp.RouterStore
{
    /// <summary>
    /// Interface of a state used to store router navigation details.
    /// </summary>
    public interface IBaseRouterState
    {
        /// <summary>
        /// Router state.
        /// </summary>
        RouterState Router { get; set; }
    }
}
