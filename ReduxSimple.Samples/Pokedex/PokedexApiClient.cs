using Newtonsoft.Json;
using ReduxSimple.Uwp.Samples.Extensions;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ReduxSimple.Uwp.Samples.Pokedex
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

        private async Task<T> GetAsync<T>(string url)
        {
            using (var httpClient = HttpClient)
            {
                var response = await HttpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
        }

        public IObservable<PokedexResponse> GetPokedex()
        {
            return Observable.Create<PokedexResponse>(async observer =>
            {
                var result = await GetAsync<PokedexResponse>("https://pokeapi.co/api/v1/pokedex/1");
                observer.OnNext(result);
            })
            .WithCache(() => _cacheService.Get<PokedexResponse>("pokedex"), r => _cacheService.Set("pokedex", r));
        }

        public IObservable<PokemonResponse> GetPokemonById(int id)
        {
            return Observable.Create<PokemonResponse>(async observer =>
            {
                var result = await GetAsync<PokemonResponse>($"https://pokeapi.co/api/v2/pokemon/{id}");
                observer.OnNext(result);
            })
            .WithCache(() => _cacheService.Get<PokemonResponse>($"pokemon/{id}"), r => _cacheService.Set($"pokemon/{id}", r));
        }
    }
}
