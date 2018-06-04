using SuccincT.Options;
using System.Collections.Immutable;
using System.Linq;
using static ReduxSimple.Samples.Common.EventTracking;

namespace ReduxSimple.Samples.Pokedex
{
    public class PokedexStore : ReduxStoreWithHistory<PokedexState>
    {
        protected override PokedexState Reduce(PokedexState state, object action)
        {
            TrackReduxAction(action, action.GetType().Name != nameof(GetPokemonListFullfilledAction));

            if (action is GetPokemonListAction _)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = state.Pokemon,
                    Loading = true,
                    Errors = state.Errors
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
                    Loading = false,
                    Errors = ImmutableList<string>.Empty
                };
            }
            if (action is GetPokemonListFailedAction getPokemonListFailedAction)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = state.Pokemon,
                    Loading = false,
                    Errors = state.Errors.Add(getPokemonListFailedAction.Exception.Message)
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
                    Loading = true,
                    Errors = state.Errors
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
                    Loading = false,
                    Errors = ImmutableList<string>.Empty
                };
            }
            if (action is GetPokemonByIdFailedAction getPokemonByIdFailedAction)
            {
                return new PokedexState
                {
                    Pokedex = state.Pokedex,
                    Search = state.Search,
                    Suggestions = state.Suggestions,
                    Pokemon = state.Pokemon,
                    Loading = false,
                    Errors = state.Errors.Add(getPokemonByIdFailedAction.Exception.Message)
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
                    Loading = state.Loading,
                    Errors = state.Errors
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
                    Loading = state.Loading,
                    Errors = state.Errors
                };
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
}
