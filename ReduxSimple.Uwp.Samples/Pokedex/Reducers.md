```csharp
public static class Reducers
{
    public static IEnumerable<On<RootState>> GetReducers()
    {
        return CreateSubReducers(SelectPokedexState)
            .On<GetPokemonListAction, GetPokemonByIdAction>(state => state.With(new { Loading = true }))
            .On<GetPokemonListFullfilledAction>(
                (state, action) => state.With(new
                {
                    Pokedex = PokedexAdapter.AddAll(action.Pokedex, state.Pokedex),
                    Loading = false,
                    Errors = ImmutableList<string>.Empty
                })
            )
            .On<GetPokemonListFailedAction>(
                (state, action) => state.With(new
                {
                    Loading = false,
                    Errors = state.Errors.Add(action.Exception.Message)
                })
            )
            .On<GetPokemonByIdFullfilledAction>(
                (state, action) => state.With(new
                {
                    Pokemon = Option<Pokemon>.Some(action.Pokemon),
                    Loading = false,
                    Errors = ImmutableList<string>.Empty
                })
            )
            .On<GetPokemonByIdFailedAction>(
                (state, action) => state.With(new
                {
                    Loading = false,
                    Errors = state.Errors.Add(action.Exception.Message)
                })
            )
            .On<UpdateSearchStringAction>(
                (state, action) => state.With(new
                {
                    action.Search
                })
            )
            .On<ResetPokemonAction>(state => state.With(new { Pokemon = Option<Pokemon>.None() }))
            .ToList();
    }
}
```