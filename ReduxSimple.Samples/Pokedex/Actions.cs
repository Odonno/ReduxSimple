using System;
using System.Collections.Generic;

namespace ReduxSimple.Uwp.Samples.Pokedex
{
    public class GetPokemonListAction { }

    public class GetPokemonListFullfilledAction
    {
        public List<PokemonGeneralInfo> Pokedex { get; set; }
    }

    public class GetPokemonListFailedAction
    {
        public Exception Exception { get; set; }
    }

    public class GetPokemonByIdAction
    {
        public int Id { get; set; }
    }

    public class GetPokemonByIdFullfilledAction
    {
        public Pokemon Pokemon { get; set; }
    }

    public class GetPokemonByIdFailedAction
    {
        public Exception Exception { get; set; }
    }

    public class UpdateSearchStringAction
    {
        public string Search { get; set; }
    }

    public class ResetPokemonAction { }
}
