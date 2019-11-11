```csharp
public class PokemonGeneralInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public List<PokemonType> Types { get; set; }
}

public class PokemonType
{
    public string Name { get; set; }
}

public class PokedexState
{
    public PokedexEntityState Pokedex { get; set; }
    public string Search { get; set; }
    public Option<Pokemon> Pokemon { get; set; }
    public bool Loading { get; set; }
    public ImmutableList<string> Errors { get; set; }

    public static PokedexState InitialState =>
        new PokedexState
        {
            Pokedex = new PokedexEntityState(),
            Search = string.Empty,
            Pokemon = Option<Pokemon>.None(),
            Errors = ImmutableList<string>.Empty
        };
}
```