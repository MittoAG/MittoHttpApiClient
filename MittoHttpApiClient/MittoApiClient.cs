namespace MittoHttpApiClient
{
    public sealed class MittoApiClient
    {
        private readonly string ApiUrl;
        private readonly string ApiKey;

        public MittoApiClient(string apiUrl, string apiKey)
        {
            ApiUrl = apiUrl;
            ApiKey = apiKey;
        }

        public SendSmsResult SendSms(Sms sms)
        {
            using (var client = new MittoHttpApiWrapperSync(ApiUrl, ApiKey))
            {
                var request = sms.ToSendSmsRequest();
                var response = client.SendSms(request);

                return SendSmsResult.FromSmsResponse(response);
            }
        }

    }
}
