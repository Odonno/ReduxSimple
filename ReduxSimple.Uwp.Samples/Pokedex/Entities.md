```csharp
public class PokedexEntityState : EntityState<PokemonGeneralInfo, int>
{
}

public static class Entities
{
    public static EntityAdapter<PokemonGeneralInfo, int> PokedexAdapter = EntityAdapter<PokemonGeneralInfo, int>.Create(item => item.Id);
}
```