namespace ReduxSimple;

/// <summary>
/// The container of a state lens (selector + reducer) to simplify the creation of nested/sub reducers.
/// </summary>
/// <typeparam name="TState">Type of the target state.</typeparam>
/// <typeparam name="TFeatureState">Type of the nested state stored inside <typeparamref name="TState"/>.</typeparam>
public interface IStateLens<TState, TFeatureState>
	where TState : class, new()
	where TFeatureState : class, new()
{
	/// <summary>
	/// Adds a new reducer.
	/// </summary>
	/// <typeparam name="TAction">Type of the action used for the reducer.</typeparam>
	/// <param name="featureReducer">Reducer function.</param>
	/// <returns>Returns the current state lens.</returns>
	IStateLens<TState, TFeatureState> On<TAction>(Func<TFeatureState, TAction, TFeatureState> featureReducer)
		where TAction : class;

	/// <summary>
	/// Adds a new reducer.
	/// </summary>
	/// <typeparam name="TAction">Type of the action used for the reducer.</typeparam>
	/// <param name="featureReducer">Reducer function.</param>
	/// <returns>Returns the current state lens.</returns>
	IStateLens<TState, TFeatureState> On<TAction>(Func<TFeatureState, TFeatureState> featureReducer)
		where TAction : class;

	/// <summary>
	/// Adds a new reducer.
	/// </summary>
	/// <typeparam name="TAction1">First type of action used for the reducer.</typeparam>
	/// <typeparam name="TAction2">Second type of action used for the reducer.</typeparam>
	/// <param name="featureReducer">Reducer function.</param>
	/// <returns>Returns the current state lens.</returns>
	IStateLens<TState, TFeatureState> On<TAction1, TAction2>(Func<TFeatureState, TFeatureState> featureReducer)
		where TAction1 : class
		where TAction2 : class;

	/// <summary>
	/// Adds a new reducer.
	/// </summary>
	/// <typeparam name="TAction1">First type of action used for the reducer.</typeparam>
	/// <typeparam name="TAction2">Second type of action used for the reducer.</typeparam>
	/// <typeparam name="TAction3">Third type of action used for the reducer.</typeparam>
	/// <param name="featureReducer">Reducer function.</param>
	/// <returns>Returns the current state lens.</returns>
	IStateLens<TState, TFeatureState> On<TAction1, TAction2, TAction3>(Func<TFeatureState, TFeatureState> featureReducer)
		where TAction1 : class
		where TAction2 : class
		where TAction3 : class;

	/// <summary>
	/// Adds a new reducer.
	/// </summary>
	/// <typeparam name="TAction1">First type of action used for the reducer.</typeparam>
	/// <typeparam name="TAction2">Second type of action used for the reducer.</typeparam>
	/// <typeparam name="TAction3">Third type of action used for the reducer.</typeparam>
	/// <typeparam name="TAction4">Fourth type of action used for the reducer.</typeparam>
	/// <param name="featureReducer">Reducer function.</param>
	/// <returns>Returns the current state lens.</returns>
	IStateLens<TState, TFeatureState> On<TAction1, TAction2, TAction3, TAction4>(Func<TFeatureState, TFeatureState> featureReducer)
		where TAction1 : class
		where TAction2 : class
		where TAction3 : class
		where TAction4 : class;

	/// <summary>
	/// Returns the list of all reducers contained in the state lens.
	/// </summary>
	/// <returns>The list of reducers to use on <typeparamref name="TState"/>.</returns>
	IEnumerable<On<TState>> ToList();
}
