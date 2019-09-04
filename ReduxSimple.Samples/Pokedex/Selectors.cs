using SuccincT.Options;
using System;
using System.Collections.Immutable;
using static ReduxSimple.Selectors;

namespace ReduxSimple.Samples.Pokedex
{
    public static class Selectors
    {
        public static Func<PokedexState, ImmutableList<PokemonGeneralInfo>> SelectPokedex = state => state.Pokedex;
        public static Func<PokedexState, bool> SelectIsPokedexEmpty = CreateSelector(
            SelectPokedex,
            pokedex => pokedex.IsEmpty
        );

        public static Func<PokedexState, bool> SelectLoading = state => state.Loading;
        public static Func<PokedexState, string> SelectSearch = state => state.Search;
        public static Func<PokedexState, ImmutableList<PokemonGeneralInfo>> SelectSuggestions = state => state.Suggestions;
        public static Func<PokedexState, Option<Pokemon>> SelectPokemon = state => state.Pokemon;
        public static Func<PokedexState, ImmutableList<string>> SelectErrors = state => state.Errors;
    }
}
