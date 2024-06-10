using MelodyFusion.DLL.Infrastructure;
using MelodyFusion.DLL.Models.Request;
using MelodyFusion.DLL.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MelodyFusion.DLL.Service
{
    public class UserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly LocalStorage _localStorage;

        public UserService(IHttpClientFactory httpClientFactory, LocalStorage localStorage)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
        }

        public async Task<UserResponse> GetUserInfo()
        {
            try
            {
                
                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url =  "/api/UserAccount/GetUserInfo";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                var response = await httpClient.GetAsync(url);

                var result = await response.Content.ReadFromJsonAsync <UserResponse>();
             
                return result;
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

        public async Task<UserResponse> UpdateUserInfo(UserRequest requestBody)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = "/api/UserAccount/UpdateUserInfo";

                // Сериализуем объект в формат JSON
                var json = JsonConvert.SerializeObject(requestBody);

                // Создаем StringContent с JSON для отправки в теле запроса
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                // Отправляем POST-запрос
                var response = await httpClient.PutAsync(url, content);

                // Проверяем успешность запроса
                response.EnsureSuccessStatusCode();

                // Читаем ответ и десериализуем его в объект RegistrationResponse
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

        public async Task<UserResponse> UpdatePassword(ChangePasswordRequest requestBody)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = "/api/UserAccount/UpdatePassword";

                // Сериализуем объект в формат JSON
                var json = JsonConvert.SerializeObject(requestBody);

                // Создаем StringContent с JSON для отправки в теле запроса
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                // Отправляем POST-запрос
                var response = await httpClient.PatchAsync(url, content);

                // Проверяем успешность запроса
                response.EnsureSuccessStatusCode();

                // Читаем ответ и десериализуем его в объект RegistrationResponse
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


        public async Task<UserResponse> DeleteProfile()
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = "/api/UserAccount/DeleteAccount";

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                // Отправляем POST-запрос
                var response = await httpClient.DeleteAsync(url);

                // Проверяем успешность запроса
                response.EnsureSuccessStatusCode();

                // Читаем ответ и десериализуем его в объект RegistrationResponse
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

        public async Task<PhotoResponse> ChangePhoto(Stream stream)
        {
            try
            {

                var httpClient = _httpClientFactory.CreateClient("MelodyFusion");

                var url = "/api/UserAccount/ChangeAvatar";

                // Создаем MultipartFormDataContent для отправки файлов
                var content = new MultipartFormDataContent();

                // Создаем StreamContent из переданного потока
                var streamContent = new StreamContent(stream);

                // Добавляем StreamContent в MultipartFormDataContent
                content.Add(streamContent, "file", "avatar.jpg"); // "file" должно совпадать с ожидаемым именем файла на сервере

                //// Сериализуем объект в формат JSON
                //var json = JsonConvert.SerializeObject(requestBody);

                //// Создаем StringContent с JSON для отправки в теле запроса
                //var content = new StringContent(json, Encoding.UTF8, "application/json");

                var bearerToken = _localStorage.GetValue<string>(LocalStorageKeys.AuthToken);

                if (string.IsNullOrEmpty(bearerToken))
                {
                    throw new InvalidOperationException("Bearer token is not available.");
                }

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                // Отправляем POST-запрос
                var response = await httpClient.PutAsync(url, content);

                // Проверяем успешность запроса
                response.EnsureSuccessStatusCode();

                // Читаем ответ и десериализуем его в объект RegistrationResponse
                var responseContent = await response.Content.ReadAsStringAsync();
                var photoResponse = JsonConvert.DeserializeObject<PhotoResponse>(responseContent);

                return photoResponse;

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
