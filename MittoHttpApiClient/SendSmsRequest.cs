using System.Collections.Generic;

namespace MittoHttpApiClient
{
    public class SendSmsRequest
    {
        public string ApiKeyHeader { get; set; }
        public string Key { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Text { get; set; }
        public MessageContentType Type { get; set; }
        public string Udh { get; set; }
        public int? Validity { get; set; }
        public int? PID { get; set; }
        public string ClientReference { get; set; }
        public bool? Test { get; set; }
        public bool IsFlash { get; set; }

        public string ToQueryParameterString()
        {
            return string.Join("&", ToQueryParameters());
        }

        private IEnumerable<string> ToQueryParameters()
        {
            if (Type != MessageContentType.GSM) yield return $"type={Type:G}";
            if (!string.IsNullOrEmpty(Sender)) yield return $"from={Sender}";
            if (!string.IsNullOrEmpty(Receiver)) yield return $"to={Receiver}";
            yield return $"text={Text}";
            if (!string.IsNullOrEmpty(ClientReference)) yield return $"ClientRef={ClientReference}";
            if (Test.HasValue && Test.Value) yield return "test=1";
            if (IsFlash) yield return "flash=1";
            if (!string.IsNullOrEmpty(Udh)) yield return $"udh={Udh}";
            if (Validity.HasValue) yield return $"validity={Validity}";
            if (PID.HasValue) yield return $"pid={PID}";
        }

    }
}
