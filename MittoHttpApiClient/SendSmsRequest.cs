using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace MittoHttpApiClient
{
    public class SendSmsRequest
    {
        public string Key { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string Sender { get; set; }

        [JsonProperty(PropertyName = "to")]
        public string Receiver { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MessageContentType Type { get; set; }

        [JsonProperty(PropertyName = "udh")]
        public string Udh { get; set; }

        [JsonProperty(PropertyName = "validity")]
        public int? Validity { get; set; }

        [JsonProperty(PropertyName = "pid")]
        public int? PID { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string ClientReference { get; set; }

        [JsonProperty(PropertyName = "test")]
        public bool? Test { get; set; }

        [JsonProperty(PropertyName = "flash")]
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
