using SuccincT.Options;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace ReduxSimple.Samples.Pokedex
{
    public class PokedexStore : ReduxStore<PokedexState>
    {
        protected override PokedexState Reduce(PokedexState state, object action)
        {
            Debug.WriteLine(action.GetType().ToString());

            if (action is GetPokemonListAction _)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = state.Pokemon,
                    Loading = true
                };
            }
            if (action is GetPokemonListFullfilledAction getPokemonListFullfilledAction)
            {
                return new PokedexState
                {
                    Pokedex = getPokemonListFullfilledAction.Pokedex.ToImmutableList(),
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = state.Pokemon,
                    Loading = false
                };
            }
            if (action is GetPokemonListFailedAction _)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = state.Pokemon,
                    Loading = false
                };
            }

            if (action is GetPokemonByIdAction _)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = state.Pokemon,
                    Loading = true
                };
            }
            if (action is GetPokemonByIdFullfilledAction getPokemonByIdFullfilledAction)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = getPokemonByIdFullfilledAction.Pokemon,
                    Loading = false
                };
            }
            if (action is GetPokemonByIdFailedAction _)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = state.Pokemon,
                    Loading = false
                };
            }

            if (action is UpdateSearchStringAction updateSearchAction)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = updateSearchAction.Search,
                    Suggestions = GetSuggestions(state.Pokedex, updateSearchAction.Search),
                    Pokemon = state.Pokemon,
                    Loading = state.Loading
                };
            }
            
            if (action is ResetPokemonAction _)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = Option<Pokemon>.None(),
                    Loading = state.Loading
                };
            }

            return base.Reduce(state, action);
        }

        private ImmutableList<PokemonGeneralInfo> GetSuggestions(ImmutableList<PokemonGeneralInfo> pokedex, string search)
        {
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
                            .Take(10)
                            .ToImmutableList();
                    }
                }
                else
                {
                    // Search Pokemon by both Id and Name
                    return pokedex
                        .Where(p => p.Id.ToString().Contains(search) || p.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(p => p.Id)
                        .Take(10)
                        .ToImmutableList();
                }
            }

            return ImmutableList<PokemonGeneralInfo>.Empty;
        }
    }
}
