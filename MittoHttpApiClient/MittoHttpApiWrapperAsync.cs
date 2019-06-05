using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MittoHttpApiClient
{
    public class MittoHttpApiWrapperAsync : IDisposable
    {
        #region Init

        private bool _disposed;
        private Lazy<HttpClient> _httpClient;

        private string RequestType = "GET";


        public MittoHttpApiWrapperAsync(string apiUrl, string apikey)
        {
            _httpClient = new Lazy<HttpClient>(() => {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(apiUrl, UriKind.Absolute),
                };
                client.DefaultRequestHeaders.Add("X-Mitto-API-Key", apikey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client;
            });

            
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MittoHttpApiWrapperAsync()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_httpClient.IsValueCreated)
                    {
                        _httpClient.Value.Dispose();
                    }
                }

                _disposed = true;
            }
        }

        #endregion

        #region Methods

        public async Task<SendSmsResponse> SendSmsAsync(SendSmsRequest request)
        {
            Contract.Requires(request != null);
            Contract.Ensures(Contract.Result<Task<SendSmsResponse>>() != null);

            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MittoHttpApiWrapperAsync));
            }
            HttpResponseMessage httpResponse = null;
            //if (method == HttpMethod.Get)
            //{
            //    httpResponse = await _httpClient.Value.GetAsync(new Uri($"sms.json?{request.ToQueryParameterString()}", UriKind.Relative));
            //}
            //if (method == HttpMethod.Post)
            //{
            var stringContent = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            var strContent = new StringContent(stringContent, Encoding.Default, "application/json");
            httpResponse = await _httpClient.Value.PostAsync(new Uri("sms", UriKind.Relative), strContent);
            //}

            var response = await httpResponse.Content.ReadAsStringAsync();
            var sendSmsResponse = JsonConvert.DeserializeObject<SendSmsResponse>(response);
            return sendSmsResponse;
        }

        #endregion
    }
}
