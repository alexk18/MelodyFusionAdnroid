using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Models.Request;
using MelodyFusionAdnroid.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MelodyFusionAdnroid.Service
{
    public class SongService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocalStorage _localStorage;

        public SongService(IHttpClientFactory httpClientFactory, LocalStorage localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }

        public async Task<List<SongDbResponse>> GetSongsBySearchString(string searchString)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = $"/api/Song?searchString={searchString}";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var response = await httpClient.GetAsync(url);

                var responseContent = await response.Content.ReadAsStringAsync();

                var songResponse = JsonConvert.DeserializeObject<List<SongDbResponse>>(responseContent);

                return songResponse;
            }
            catch (HttpRequestException ex)
            {
                // Обработка ошибок при выполнении HTTP-запроса
                Console.WriteLine($"HTTP Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }

        public async Task<List<SongSpotifyResponse>> GetRecommendation(SongRecommendationRequest songRecommendationRequest)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = $"/get-recommendation?FirstSongId={songRecommendationRequest.FirstSongId}&SecondSongId={songRecommendationRequest.SecondSongId}";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var response = await httpClient.GetAsync(url);

                var responseContent = await response.Content.ReadAsStringAsync();

                var songSpotifyResponse = JsonConvert.DeserializeObject<List<SongSpotifyResponse>>(responseContent);

                return songSpotifyResponse;
            }
            catch (HttpRequestException ex)
            {
                // Обработка ошибок при выполнении HTTP-запроса
                Console.WriteLine($"HTTP Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Обработка других исключений
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

        }
    }
}
