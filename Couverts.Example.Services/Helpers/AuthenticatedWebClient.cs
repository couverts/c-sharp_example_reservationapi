using System;
using System.Net.Http;
using System.Text;
using Couverts.Example.ApiCalls.Properties;

namespace Couverts.Example.Services.Helpers {
    internal class AuthenticatedWebClient : HttpClient {
        public AuthenticatedWebClient(int? version = null) {
            BaseAddress = new Uri(Settings.Default.ApiBaseUrl);
            DefaultRequestHeaders.Add("Authorization", "Basic " + Base64Encode(Settings.Default.RestaurantId + ":" + Settings.Default.ApiKey));
            DefaultRequestHeaders.Add("Accept", "application/json");

            if (version.HasValue) {
                DefaultRequestHeaders.Add("X-Version", version.Value.ToString());
            }
        }

        private static string Base64Encode(string plainText) {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}