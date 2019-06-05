using Newtonsoft.Json;
using System;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;

namespace MittoHttpApiClient
{
    public class MittoHttpApiWrapperAsync : IDisposable
    {
        #region Init

        private bool _disposed;
        private Lazy<HttpClient> _httpClient;


        public MittoHttpApiWrapperAsync(string apiUrl, string apikey)
        {
            _httpClient = new Lazy<HttpClient>(() => new HttpClient
            {
                BaseAddress = new Uri(apiUrl, UriKind.Absolute),
                DefaultRequestHeaders = { { "X-Mitto-API-Key", apikey } }
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

            var httpResponse = await _httpClient.Value.GetAsync(new Uri($"sms?{request.ToQueryParameterString()}", UriKind.Relative));

            httpResponse.EnsureSuccessStatusCode();

            var sendSmsResponse = JsonConvert.DeserializeObject<SendSmsResponse>(await httpResponse.Content.ReadAsStringAsync());
            return sendSmsResponse;
        }

        #endregion
    }
}
