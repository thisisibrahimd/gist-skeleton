using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Gist.ApplicationCore.Constants;
using Gist.ApplicationCore.Interfaces;
using Newtonsoft.Json;

namespace Gist.Infrastructure.Services
{
    public class SvmApiService : ISvmService
    {
        private readonly HttpClient _httpClient;

        public SvmApiService(string baseUrl)
        {
            Guard.Against.NullOrEmpty(baseUrl, nameof(baseUrl));

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        public async Task<double[]> SvcAsync(double[][] features,
            double[] labels,
            CancellationToken cancellationToken = default)
        {
            Guard.Against.Null(features, nameof(features));
            Guard.Against.Null(labels, nameof(labels));

            if (features.Length != labels.Length)
                throw new ArgumentException(
                    "the length of features and labels must be the same");

            if (features.Any(feature => feature.Length != features[0].Length))
                throw new ArgumentException(
                    "the arrays in the features array must contain the same length");

            var request = new
            {
                features, labels
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                SvmApiEndpointConstants.Svc, content, cancellationToken);

            var jsonString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(jsonString);
                throw new InvalidOperationException(error.Description);
            }

            var result = JsonConvert.DeserializeObject<SvcEndpointResponse>(jsonString);
            var predictions = result.Result;
            return predictions;
        }

        internal class SvcEndpointResponse
        {
            public double[] Result { get; set; }
        }

        internal class ErrorResponse
        {
            public int Code { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
        }
    }
}