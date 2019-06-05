using System.Threading.Tasks;

namespace MittoHttpApiClient
{
    public sealed class MittoApiClientAsync : IMittoHttpApiClientAsync
    {
        private readonly string ApiUrl;
        private readonly string ApiKey;

        public MittoApiClientAsync(string apiUrl, string apiKey)
        {
            ApiUrl = apiUrl;
            ApiKey = apiKey;
        }

        public async Task<SendSmsResult> SendSmsAsync(Sms sms)
        {
            using (var client = new MittoHttpApiWrapperAsync(ApiUrl, ApiKey))
            {
                var request = sms.ToSendSmsRequest();
                var response = await client.SendSmsAsync(request);

                return SendSmsResult.FromSmsResponse(response);
            }
        }
    }

}
