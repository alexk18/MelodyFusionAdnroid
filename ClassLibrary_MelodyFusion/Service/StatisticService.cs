using MelodyFusion.DLL.Infrastructure;
using MelodyFusion.DLL.Models.Request;
using MelodyFusion.DLL.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MelodyFusion.DLL.Service
{
    public class StatisticService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocalStorage _localStorage;

        public StatisticService(IHttpClientFactory httpClientFactory, LocalStorage localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }

        public async Task<LoginStatisticResponse> GetLoginInfo(StatisticRequest request)
        {
            try
            {

                var dateFrom = request.DateFrom;
                var dateTo =  request.DateTo;

                // Форматируем даты как строки в формате yyyy-MM-dd
                string dateFromString = dateFrom.ToString("yyyy-MM-dd");
                string dateToString = dateTo.ToString("yyyy-MM-dd");

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = $"/api/Statistic/GetLoginInfo?DateFrom={dateFromString}&DateTo={dateToString}";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var response = await httpClient.GetAsync(url);

                var responseContent = await response.Content.ReadAsStringAsync();

                var statisticLogResponse = JsonConvert.DeserializeObject<LoginStatisticResponse>(responseContent);

                return statisticLogResponse ;
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

        public async Task<SubscriptionStatisticResponse> GetUserInfo(StatisticRequest request)
        {
            try
            {

                var dateFrom = request.DateFrom;
                var dateTo = request.DateTo;

                // Форматируем даты как строки в формате yyyy-MM-dd
                string dateFromString = dateFrom.ToString("yyyy-MM-dd");
                string dateToString = dateTo.ToString("yyyy-MM-dd");

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = $"/api/Statistic/GetUserInfo?DateFrom={dateFromString}&DateTo={dateToString}";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var response = await httpClient.GetAsync(url);

                var responseContent = await response.Content.ReadAsStringAsync();

                var statisticLogResponse = JsonConvert.DeserializeObject<SubscriptionStatisticResponse>(responseContent);

                return statisticLogResponse;
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
