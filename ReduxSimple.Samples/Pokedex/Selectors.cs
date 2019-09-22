using SuccincT.Options;
using System;
using System.Collections.Immutable;
using System.Linq;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.Pokedex
{
    public static class Selectors
    {
        public static Func<RootState, PokedexState> SelectPokedexState = state => state.Pokedex;

        public static MemoizedSelector<RootState, PokedexState, ImmutableList<PokemonGeneralInfo>> SelectPokedex = CreateSelector(
            SelectPokedexState,
            state => state.Pokedex
        );
        public static MemoizedSelector<RootState, ImmutableList<PokemonGeneralInfo>, bool> SelectIsPokedexEmpty = CreateSelector(
            SelectPokedex,
            pokedex => pokedex.IsEmpty
        );

        public static MemoizedSelector<RootState, PokedexState, bool> SelectLoading = CreateSelector(
            SelectPokedexState,
            state => state.Loading
        );
        public static MemoizedSelector<RootState, PokedexState, string> SelectSearch = CreateSelector(
            SelectPokedexState,
            state => state.Search
        );
        public static MemoizedSelector<RootState, PokedexState, Option<Pokemon>> SelectPokemon = CreateSelector(
            SelectPokedexState,
            state => state.Pokemon
        );
        public static MemoizedSelector<RootState, PokedexState, ImmutableList<string>> SelectErrors = CreateSelector(
            SelectPokedexState,
            state => state.Errors
        );

        public static MemoizedSelectorWithProps<RootState, int, string, ImmutableList<PokemonGeneralInfo>, ImmutableList<PokemonGeneralInfo>> SelectSuggestions = CreateSelector<RootState, int, PokedexState, string, PokedexState, ImmutableList<PokemonGeneralInfo>, ImmutableList<PokemonGeneralInfo>>(
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
