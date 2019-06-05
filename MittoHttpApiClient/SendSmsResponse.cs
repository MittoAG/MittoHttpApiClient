using System;

namespace MittoHttpApiClient
{
    public class SendSmsResponse
    {
        public Guid? Id { get; set; }
        public DateTime Time { get; set; }
        public string Receiver { get; set; }
        public ResponseCode ResponseCode { get; set; }
        public int TextLength { get; set; }
        public string ResponseText { get; set; }
        public bool? Test { get; set; }

    }
}
