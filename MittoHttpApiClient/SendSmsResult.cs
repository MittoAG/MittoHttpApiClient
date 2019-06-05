using System;
using System.Diagnostics.Contracts;

namespace MittoHttpApiClient
{
    public sealed class SendSmsResult
    {
        public Guid? Id { get; set; }
        public DateTime Time { get; set; }
        public string To { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public int TextLength { get; set; }
        public string ResponseText { get; set; }
        public bool? Test { get; set; }

        internal static SendSmsResult FromSmsResponse(SendSmsResponse sendSmsResponse)
        {
            Contract.Requires(sendSmsResponse != null);
            Contract.Ensures(Contract.Result<SendSmsResult>() != null);

            return new SendSmsResult
            {
                Id = sendSmsResponse.Id,
                Time = sendSmsResponse.Time,
                To = sendSmsResponse.Receiver,
                ResponseCode = sendSmsResponse.ResponseCode,
                TextLength = sendSmsResponse.TextLength,
                ResponseText = sendSmsResponse.ResponseText,
                Test = sendSmsResponse.Test
            };
        }
    }
}
