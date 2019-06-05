using System;
using System.Diagnostics.Contracts;

namespace MittoHttpApiClient
{
    public sealed class BinarySms : Sms
    {
        public byte[] Data { get; set; }

        internal override SendSmsRequest ToSendSmsRequest()
        {
            Contract.Ensures(Contract.Result<SendSmsRequest>() != null);

            var request = base.ToSendSmsRequest();
            request.Type = MessageContentType.Binary;
            request.Text = Data == null ? null : BitConverter.ToString(Data).Replace("-", string.Empty);
            return request;
        }
    }
}
