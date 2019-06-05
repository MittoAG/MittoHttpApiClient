using System.Diagnostics.Contracts;

namespace MittoHttpApiClient
{
    public abstract partial class Sms
    {
        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string Text { get; set; }

        public MessageContentType Type { get; set; }

        public string Udh { get; set; }

        public string ClientReference { get; set; }

        public bool IsFlash { get; set; }

        public bool Test { get; set; }

        public int? PID { get; set; }

        public int? Validity { get; set; }

        internal virtual SendSmsRequest ToSendSmsRequest()
        {
            Contract.Ensures(Contract.Result<SendSmsRequest>() != null);

            return new SendSmsRequest
            {
                Sender = Sender,
                Receiver = Receiver,
                ClientReference = ClientReference,
                IsFlash = IsFlash,
                Test = Test,
                PID = PID,
                Text = Text,
                Type = Type,
                Udh = Udh,
                Validity = Validity
            };
        }
    }
}
