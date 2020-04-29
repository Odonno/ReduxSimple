using System;
using System.Collections.Generic;

namespace ReduxSimple
{
	public interface IStateLens<TState, TFeatureState>
		where TState : class, new()
		where TFeatureState : class, new()
	{
		IStateLens<TState, TFeatureState> On<TAction>(Func<TFeatureState, TAction, TFeatureState> featureReducer)
			where TAction : class;
		IStateLens<TState, TFeatureState> On<TAction>(Func<TFeatureState, TFeatureState> featureReducer)
			where TAction : class;
		IStateLens<TState, TFeatureState> On<TAction1, TAction2>(Func<TFeatureState, TFeatureState> featureReducer)
			where TAction1 : class
			where TAction2 : class;
		IStateLens<TState, TFeatureState> On<TAction1, TAction2, TAction3>(Func<TFeatureState, TFeatureState> featureReducer)
			where TAction1 : class
			where TAction2 : class
			where TAction3 : class;
		IStateLens<TState, TFeatureState> On<TAction1, TAction2, TAction3, TAction4>(Func<TFeatureState, TFeatureState> featureReducer)
			where TAction1 : class
			where TAction2 : class
			where TAction3 : class
			where TAction4 : class;
		IEnumerable<On<TState>> ToList();
	}
}
