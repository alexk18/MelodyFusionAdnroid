using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelodyFusionAdnroid.Models;
using RestSharp;
using System.Net.Http;
using MelodyFusionAdnroid.Infrastructure;



namespace MelodyFusionAdnroid.Service
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocalStorage _localStorage;

        public AuthService(IHttpClientFactory httpClientFactory, LocalStorage localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }


        public async Task<LoginResponse> Login(LoginRequest requestBody)
        {
            try
            {
                // Используем IHttpClientFactory для создания экземпляра HttpClient
                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url =  "/api/Authentication/Login";

                // Сериализуем объект в формат JSON
                var json = JsonConvert.SerializeObject(requestBody);

                // Создаем StringContent с JSON для отправки в теле запроса
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем POST-запрос
                var response = await httpClient.PostAsync(url, content);

                // Проверяем успешность запроса
                response.EnsureSuccessStatusCode();

                // Читаем ответ и десериализуем его в объект LoginResponse
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                return loginResponse;
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

