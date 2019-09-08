```csharp
public static class Selectors
{
    public static Func<PokedexState, ImmutableList<PokemonGeneralInfo>> SelectPokedex = state => state.Pokedex;
    public static MemoizedSelector<PokedexState, ImmutableList<PokemonGeneralInfo>, bool> SelectIsPokedexEmpty = CreateSelector(
        SelectPokedex,
        pokedex => pokedex.IsEmpty
    );

    public static Func<PokedexState, bool> SelectLoading = state => state.Loading;
    public static Func<PokedexState, string> SelectSearch = state => state.Search;
    public static Func<PokedexState, ImmutableList<PokemonGeneralInfo>> SelectSuggestions = state => state.Suggestions;
    public static Func<PokedexState, Option<Pokemon>> SelectPokemon = state => state.Pokemon;
    public static Func<PokedexState, ImmutableList<string>> SelectErrors = state => state.Errors;
}
```