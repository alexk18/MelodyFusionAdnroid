using MelodyFusionAdnroid.Infrastructure;
using MelodyFusionAdnroid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MelodyFusionAdnroid.Service
{

    public class AdminService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocalStorage _localStorage;

        public AdminService(IHttpClientFactory httpClientFactory, LocalStorage localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }

        public async Task<List<UserResponse>> GetUserList(UserAllRequest requestBody)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = $"/api/Admin/GetUserList?PageSize={requestBody.PageSize}";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var response = await httpClient.GetAsync(url);

                var responseContent = await response.Content.ReadAsStringAsync();

                var userResponse = JsonConvert.DeserializeObject<List<UserResponse>>(responseContent);

                return userResponse;
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

        public async Task<UserResponse> AddAdminRole(ChangeRoleRequest requestBody)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = "/api/Admin/AddRole";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);


                // Сериализуем объект в формат JSON
                var json = JsonConvert.SerializeObject(requestBody);

                // Создаем StringContent с JSON для отправки в теле запроса
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PatchAsync(url, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                var userResponse = JsonConvert.DeserializeObject<UserResponse>(responseContent);

                return userResponse;
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

        public async Task<UserResponse> DeleteAdminRole(ChangeRoleRequest requestBody)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = "/api/Admin/DeleteRole";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);


                // Сериализуем объект в формат JSON
                var json = JsonConvert.SerializeObject(requestBody);

                // Создаем StringContent с JSON для отправки в теле запроса
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PatchAsync(url, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                var userResponse = JsonConvert.DeserializeObject<UserResponse>(responseContent);

                return userResponse;
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

        public async Task<UserResponse> BanUser(string userId)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = $"/api/Admin/DeleteRole/{userId}";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                // Сериализуем объект в формат JSON
                var json = JsonConvert.SerializeObject(userId);

                // Создаем StringContent с JSON для отправки в теле запроса
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PatchAsync(url, content);

                var responseContent = await response.Content.ReadAsStringAsync();

                var userResponse = JsonConvert.DeserializeObject<UserResponse>(responseContent);

                return userResponse;
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
