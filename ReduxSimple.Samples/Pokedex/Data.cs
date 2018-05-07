using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReduxSimple.Samples.Pokedex
{
    public class PokedexResponse
    {
        [JsonProperty("objects")]
        public List<PokedexObjectsResponse> Root { get; set; }
    }

    public class PokedexObjectsResponse
    {
        public string Name { get; set; }

        [JsonProperty("pokemon")]
        public List<PokemonInfoResponse> Pokemons { get; set; }
    }

    public class PokemonInfoResponse
    {
        public int Id => int.Parse(ResourceUri.Replace("api/v1/pokemon", string.Empty).Replace("/", string.Empty));

        public string Name { get; set; }

        [JsonProperty("resource_uri")]
        public string ResourceUri { get; set; }
    }

    public class PokemonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PokemonSpritesResponse Sprites { get; set; }
        public List<PokemonTypeResponse> Types { get; set; }
    }

    public class PokemonSpritesResponse
    {
        [JsonProperty("front_default")]
        public string Image { get; set; }
    }

    public class PokemonTypeResponse
    {
        public int Slot { get; set; }
        public PokemonSubTypeResponse Type { get; set; }
    }

    public class PokemonSubTypeResponse
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
