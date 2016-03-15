using System;
using System.Net.Http;
using System.Text;
using Couverts.Example.Services.Helpers;
using Newtonsoft.Json;
using System.Web;

namespace Couverts.Example.Services {
    public class WebClient : IDisposable {
        private readonly AuthenticatedWebClient _authenticatedWebClient;

        public WebClient() {
            _authenticatedWebClient = new AuthenticatedWebClient();
        }

        public T Get<T>(string url) {
            var response = _authenticatedWebClient.GetAsync(url).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<T>(content);
        }

        public T Get<T>(string url, object data) {
            return Get<T>($"{url}?{ParametersFromAnonymousType(data)}");
        }

        public TOut Post<TOut, TIn>(string url, TIn data) {
            var requestJson = JsonConvert.SerializeObject(data);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var response = _authenticatedWebClient.PostAsync(url, requestContent).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<TOut>(content);
        }

        private static string ParametersFromAnonymousType(object obj) {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            foreach (var pi in obj.GetType().GetProperties()) {
                queryString[pi.Name] = pi.GetValue(obj, null).ToString();
            }

            return queryString.ToString();
        }

        public void Dispose() {
            _authenticatedWebClient.Dispose();
        }
    }
}