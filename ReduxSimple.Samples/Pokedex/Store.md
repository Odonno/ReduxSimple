```csharp
public class PokedexStore : ReduxStore<PokedexState>
{
    protected override PokedexState Reduce(PokedexState state, object action)
    {
        if (action is GetPokemonListAction _ || action is GetPokemonByIdAction _)
        {
            return state.With(new { Loading = true });
        }
        if (action is GetPokemonListFullfilledAction getPokemonListFullfilledAction)
        {
            return state.With(new
            {
                Pokedex = getPokemonListFullfilledAction.Pokedex.ToImmutableList(),
                Loading = false,
                Errors = ImmutableList<string>.Empty
            });
        }
        if (action is GetPokemonListFailedAction getPokemonListFailedAction)
        {
            return state.With(new
            {
                Loading = false,
                Errors = state.Errors.Add(getPokemonListFailedAction.Exception.Message)
            });
        }
        if (action is GetPokemonByIdFullfilledAction getPokemonByIdFullfilledAction)
        {
            return state.With(new
            {
                Loading = false,
                Errors = ImmutableList<string>.Empty
            });
        }
        if (action is GetPokemonByIdFailedAction getPokemonByIdFailedAction)
        {
            return state.With(new
            {
                Loading = false,
                Errors = state.Errors.Add(getPokemonByIdFailedAction.Exception.Message)
            });
        }

        if (action is UpdateSearchStringAction updateSearchAction)
        {
            return state.With(new
            {
                Suggestions = GetSuggestions(state.Pokedex, updateSearchAction.Search)
            });
        }

        if (action is ResetPokemonAction _)
        {
            return state.With(new
            {
                Pokemon = Option<Pokemon>.None()
            });
        }

        return base.Reduce(state, action);
    }

    private ImmutableList<PokemonGeneralInfo> GetSuggestions(ImmutableList<PokemonGeneralInfo> pokedex, string search)
    {
        const int maximumOfSuggestions = 5;

        if (!string.IsNullOrWhiteSpace(search))
        {
            if (search.StartsWith("#"))
            {
                // Search Pokemon by Id
                if (int.TryParse(search.Substring(1), out int searchedId))
                {
                    return pokedex
                        .Where(p => p.Id.ToString().StartsWith(searchedId.ToString()))
                        .OrderBy(p => p.Id)
                        .Take(maximumOfSuggestions)
                        .ToImmutableList();
                }
            }
            else
            {
                // Search Pokemon by both Id and Name
                return pokedex
                    .Where(p => p.Id.ToString().Contains(search) || p.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(p => p.Id)
                    .Take(maximumOfSuggestions)
                    .ToImmutableList();
            }
        }

        return ImmutableList<PokemonGeneralInfo>.Empty;
    }
}
```