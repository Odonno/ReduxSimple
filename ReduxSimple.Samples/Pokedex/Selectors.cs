using SuccincT.Options;
using System;
using System.Collections.Immutable;
using System.Linq;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.Pokedex
{
    public static class Selectors
    {
        public static Func<PokedexState, ImmutableList<PokemonGeneralInfo>> SelectPokedex = state => state.Pokedex;
        public static MemoizedSelector<PokedexState, ImmutableList<PokemonGeneralInfo>, bool> SelectIsPokedexEmpty = CreateSelector(
            SelectPokedex,
            pokedex => pokedex.IsEmpty
        );

        public static Func<PokedexState, bool> SelectLoading = state => state.Loading;
        public static Func<PokedexState, string> SelectSearch = state => state.Search;
        public static Func<PokedexState, Option<Pokemon>> SelectPokemon = state => state.Pokemon;
        public static Func<PokedexState, ImmutableList<string>> SelectErrors = state => state.Errors;

        public static MemoizedSelectorWithProps<PokedexState, int, string, ImmutableList<PokemonGeneralInfo>, ImmutableList<PokemonGeneralInfo>> SelectSuggestions = CreateSelector<PokedexState, int, string, ImmutableList<PokemonGeneralInfo>, ImmutableList<PokemonGeneralInfo>>(
            SelectSearch,
            SelectPokedex,
            (search, pokedex, maximumOfSuggestions) =>
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
        );
    }
}
