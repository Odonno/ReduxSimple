using SuccincT.Options;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ReduxSimple.Uwp.Samples.Pokedex
{
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
        public ImmutableList<PokemonGeneralInfo> Pokedex { get; set; } = ImmutableList<PokemonGeneralInfo>.Empty;
        public string Search { get; set; } = string.Empty;
        public Option<Pokemon> Pokemon { get; set; } = Option<Pokemon>.None();
        public bool Loading { get; set; }
        public ImmutableList<string> Errors { get; set; } = ImmutableList<string>.Empty;
    }
}
