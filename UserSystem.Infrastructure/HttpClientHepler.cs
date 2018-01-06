using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace UserSystem.Infrastructure
{
   public class HttpClientHepler
    {
        private HttpClient _httpClient;

        public HttpClientHepler(string authorizationType, string value)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
            _httpClient.DefaultRequestHeaders.Add("Authorization",
                string.Format("{0} {1}", authorizationType, value));
        }

        public async Task<WebApiResponse<string>> GetAsync(string url , params object[] args)
        {
            string internalUrl = string.Format(url, args);
            var result = await _httpClient.GetAsync(internalUrl);
            return await result.Content.ReadAsAsync<WebApiResponse<string>>();
        }

        public async Task<WebApiResponse<string>> PostAysnc<T>(string url, T parm)
        {
            var result = await _httpClient.PostAsync<T>(url, parm , new JsonMediaTypeFormatter());
            return await result.Content.ReadAsAsync<WebApiResponse<string>>();        
        }

        public void SetTimeout(double seconds)
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(seconds);
        }
    }
}
