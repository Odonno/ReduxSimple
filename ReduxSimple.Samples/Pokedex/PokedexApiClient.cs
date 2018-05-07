using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;

namespace ReduxSimple.Samples.Pokedex
{
    public class PokedexApiClient
    {
        private readonly CacheService _cacheService = new CacheService();

        private HttpClient HttpClient
        {
            get
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient;
            }
        }

        public IObservable<PokedexResponse> GetPokedex()
        {
            return Observable.Create<PokedexResponse>(async observer =>
                {
                    var response = await HttpClient.GetAsync("https://pokeapi.co/api/v1/pokedex");

                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsStringAsync();
                    observer.OnNext(JsonConvert.DeserializeObject<PokedexResponse>(data));
                })
                .WithCache(() => _cacheService.Get<PokedexResponse>("pokedex"), r => _cacheService.Set("pokedex", r));
        }

        public IObservable<PokemonResponse> GetPokemonById(int id)
        {
            return Observable.Create<PokemonResponse>(async observer =>
                {
                    var response = await HttpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{id}");

                    response.EnsureSuccessStatusCode();

                    var data = await response.Content.ReadAsStringAsync();
                    observer.OnNext(JsonConvert.DeserializeObject<PokemonResponse>(data));
                })
                .WithCache(() => _cacheService.Get<PokemonResponse>($"pokemon/{id}"), r => _cacheService.Set($"pokemon/{id}", r));
        }
    }
}
