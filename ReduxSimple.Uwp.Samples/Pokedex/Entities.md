```csharp
public class PokedexEntityState : EntityState<int, PokemonGeneralInfo>
{
}

public static class Entities
{
    public static EntityAdapter<int, PokemonGeneralInfo> PokedexAdapter = EntityAdapter<int, PokemonGeneralInfo>.Create(item => item.Id);
}
```