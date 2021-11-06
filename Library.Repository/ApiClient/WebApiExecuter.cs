using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository.ApiClient
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly string _baseUrl;
        private readonly HttpClient _httpClient;
        private readonly ITokenRepository _tokenRepository;

        public WebApiExecuter(string baseUrl, HttpClient httpClient, ITokenRepository tokenRepository)
        // public WebApiExecuter(string baseUrl, HttpClient httpClient, string clientId, string apiKey)
        {
            _baseUrl = baseUrl;
            _httpClient = httpClient;
            _tokenRepository = tokenRepository;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


            //_httpClient.DefaultRequestHeaders.Add("ApiKey", apiKey);
            //_httpClient.DefaultRequestHeaders.Add("ClientId", clientId);
        }

        public async Task<T> InvokeGet<T>(string uri)
        {
            await AddTokenHeader();
            return await _httpClient.GetFromJsonAsync<T>(Geturl(uri));
        }

        private async Task AddTokenHeader()
        {
            if (_tokenRepository != null && !string.IsNullOrEmpty(await _tokenRepository.GetToken()))
            {
                _httpClient.DefaultRequestHeaders.Remove("TokenHeader");
                _httpClient.DefaultRequestHeaders.Add("TokenHeader", await _tokenRepository.GetToken());
            }
        }

        public async Task<T> InvokePost<T>(string uri, T obj)
        {
            await AddTokenHeader();
            var response = await _httpClient.PostAsJsonAsync<T>(Geturl(uri), obj);
            await HandleError(response);

            return await response.Content.ReadFromJsonAsync<T>();
        }
        public async Task<string> InvokePostAsString<T>(string uri, T obj)
        {
            await AddTokenHeader();
            var response = await _httpClient.PostAsJsonAsync<T>(Geturl(uri), obj);
            await HandleError(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task InvokePut<T>(string uri, T obj)
        {
            var response = await _httpClient.PutAsJsonAsync<T>(Geturl(uri), obj);
            await HandleError(response);
        }

        public async Task InvokeDelete<T>(string uri)
        {
            await AddTokenHeader();
            var response = await _httpClient.DeleteAsync(Geturl(uri));
            response.EnsureSuccessStatusCode();
        }

        private string Geturl(string uri)
        {
            return $"{_baseUrl}/{uri}";
        }

        private async Task HandleError(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(error);
            }
        }
    }
}
