using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MelodyFusionAdnroid.Models;



namespace MelodyFusionAdnroid.Service
{
    public class RegisterService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<RegistrationResponse> Registr(RegistrationRequest requestBody)
        {
            try
            {
                // Используем IHttpClientFactory для создания экземпляра HttpClient
                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = "/api/Authentication/Registration";

                // Сериализуем объект в формат JSON
                var json = JsonConvert.SerializeObject(requestBody);

                // Создаем StringContent с JSON для отправки в теле запроса
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем POST-запрос
                var response = await httpClient.PostAsync(url, content);


                // Проверяем успешность запроса
                response.EnsureSuccessStatusCode();

                // Читаем ответ и десериализуем его в объект RegistrationResponse
                var responseContent = await response.Content.ReadAsStringAsync();
                var registrationResponse = JsonConvert.DeserializeObject<RegistrationResponse>(responseContent);

                return registrationResponse;
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
