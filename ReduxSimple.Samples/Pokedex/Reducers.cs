using Converto;
using SuccincT.Options;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static ReduxSimple.Reducers;

namespace ReduxSimple.Samples.Pokedex
{
    public static class Reducers
    {
        public static IEnumerable<On<PokedexState>> CreateReducers()
        {
            return new List<On<PokedexState>>
            {
                On<GetPokemonListAction, GetPokemonByIdAction, PokedexState>(
                    state => state.With(new { Loading = true })
                ),
                On<GetPokemonListFullfilledAction, PokedexState>(
                    (state, action) => state.With(new {
                        Pokedex = action.Pokedex.ToImmutableList(),
                        Loading = false,
                        Errors = ImmutableList<string>.Empty
                    })
                ),
                On<GetPokemonListFailedAction, PokedexState>(
                    (state, action) => state.With(new {
                        Loading = false,
                        Errors = state.Errors.Add(action.Exception.Message)
                    })
                ),
                On<GetPokemonByIdFullfilledAction, PokedexState>(
                    (state, action) => state.With(new {
                        Pokemon = Option<Pokemon>.Some(action.Pokemon),
                        Loading = false,
                        Errors = ImmutableList<string>.Empty
                    })
                ),
                On<GetPokemonByIdFailedAction, PokedexState>(
                    (state, action) => state.With(new {
                        Loading = false,
                        Errors = state.Errors.Add(action.Exception.Message)
                    })
                ),
                On<UpdateSearchStringAction, PokedexState>(
                    (state, action) => state.With(new {
                        action.Search,
                        Suggestions = GetSuggestions(state.Pokedex, action.Search)
                    })
                ),
                On<ResetPokemonAction, PokedexState>(
                    state => state.With(new { Pokemon = Option<Pokemon>.None() })
                )
            };
        }

        private static ImmutableList<PokemonGeneralInfo> GetSuggestions(ImmutableList<PokemonGeneralInfo> pokedex, string search)
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
}
