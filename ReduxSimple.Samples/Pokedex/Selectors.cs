using SuccincT.Options;
using System.Collections.Immutable;
using System.Linq;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.Pokedex
{
    public static class Selectors
    {
        public static ISelectorWithoutProps<RootState, PokedexState> SelectPokedexState = CreateSelector(
            (RootState state) => state.Pokedex
        );

        public static ISelectorWithoutProps<RootState, ImmutableList<PokemonGeneralInfo>> SelectPokedex = CreateSelector(
            SelectPokedexState,
            state => state.Pokedex
        );
        public static ISelectorWithoutProps<RootState, bool> SelectIsPokedexEmpty = CreateSelector(
            SelectPokedex,
            pokedex => pokedex.IsEmpty
        );

        public static ISelectorWithoutProps<RootState, bool> SelectLoading = CreateSelector(
            SelectPokedexState,
            state => state.Loading
        );
        public static ISelectorWithoutProps<RootState, string> SelectSearch = CreateSelector(
            SelectPokedexState,
            state => state.Search
        );
        public static ISelectorWithoutProps<RootState, Option<Pokemon>> SelectPokemon = CreateSelector(
            SelectPokedexState,
            state => state.Pokemon
        );
        public static ISelectorWithoutProps<RootState, ImmutableList<string>> SelectErrors = CreateSelector(
            SelectPokedexState,
            state => state.Errors
        );

        public static ISelectorWithProps<RootState, int, ImmutableList<PokemonGeneralInfo>> SelectSuggestions = CreateSelector(
            SelectSearch,
            SelectPokedex,
            (string search, ImmutableList<PokemonGeneralInfo> pokedex, int maximumOfSuggestions) =>
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
