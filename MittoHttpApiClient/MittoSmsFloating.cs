using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace MittoHttpApiClient
{
    partial class Sms
    {
        internal MittoApiClientAsync ApiClientAsync = null;
        internal MittoApiClient ApiClient = null;

        public static TextSms CreateText()
        {
            Contract.Ensures(Contract.Result<TextSms>() != null);

            return new TextSms();
        }

        public static BinarySms CreateBinary()
        {
            Contract.Ensures(Contract.Result<BinarySms>() != null);

            return new BinarySms();
        }

        public Task<SendSmsResult> SendAsync<TSms>()
        {
            Contract.Ensures(ApiClientAsync != null);
            Contract.Ensures(Contract.Result<Task<SendSmsResult>>() != null);

            return ApiClientAsync.SendSmsAsync(this);
        }

        public SendSmsResult Send<TSms>()
        {
            Contract.Ensures(ApiClient != null);
            Contract.Ensures(Contract.Result<SendSmsResult>() != null);
            return ApiClient.SendSms(this);
        }
    }

    public static class SmsExtensions
    {
        public static SendSmsResult Send<TSms>(this TSms sms)
        {
            return sms.Send();
        }

        public static SendSmsResult SendAsync<TSms>(this TSms sms)
        {
            return sms.SendAsync();
        }

        public static TSms initAsyncClient<TSms>(this TSms sms, string apiUrl, string apiKey)
            where TSms : Sms
        {
            sms.ApiClientAsync = new MittoApiClientAsync(apiUrl, apiKey);
            return sms;
        }

        public static TSms initClient<TSms>(this TSms sms, string apiUrl, string apiKey)
            where TSms : Sms
        {
            sms.ApiClient = new MittoApiClient(apiUrl, apiKey);
            return sms;
        }

        private static TSms Assign<TSms>(this TSms sms, Action<TSms> assigner)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Requires(assigner != null);
            Contract.Ensures(Contract.Result<TSms>() != null);

            assigner.Invoke(sms);
            return sms;
        }

        public static TSms From<TSms>(this TSms sms, string from)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<TSms>() == sms);

            return sms.Assign(s => s.Sender = from);
        }

        public static TSms To<TSms>(this TSms sms, string to)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<TSms>() == sms);

            return sms.Assign(s => s.Receiver = to);
        }

        public static TextSms Text(this TextSms textSms, string text)
        {
            Contract.Requires(textSms != null);
            Contract.Ensures(Contract.Result<TextSms>() == textSms);

            return textSms.Assign(s => s.Text = text);
        }

        public static TextSms AsUnicode(this TextSms textSms)
        {
            Contract.Requires(textSms != null);
            Contract.Ensures(Contract.Result<TextSms>() == textSms);

            return textSms.Assign(s => s.IsUnicode = true);
        }

        public static TextSms WithAutomaticUnicodeRecognition(this TextSms textSms)
        {
            Contract.Requires(textSms != null);
            Contract.Ensures(Contract.Result<TextSms>() == textSms);

            return textSms.Assign(s => s.AutomaticUnicodeRecognition = true);
        }

        public static BinarySms Data(this BinarySms binarySms, byte[] data)
        {
            Contract.Requires(binarySms != null);
            Contract.Ensures(Contract.Result<BinarySms>() == binarySms);

            return binarySms.Assign(s => s.Data = data);
        }

        public static TSms AsFlash<TSms>(this TSms sms)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<TSms>() == sms);

            return sms.Assign(s => s.IsFlash = true);
        }

        public static TSms WithClientReference<TSms>(this TSms sms, string text)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<TSms>() == sms);

            return sms.Assign(s => s.ClientReference = text);
        }

        public static TSms WithProcessId<TSms>(this TSms sms, int pid)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<TSms>() == sms);

            return sms.Assign(s => s.PID = pid);
        }

        public static TSms AsType<TSms>(this TSms sms, MessageContentType type)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<TSms>() == sms);

            return sms.Assign(s => s.Type = type);
        }

        public static TSms WithUdh<TSms>(this TSms sms, string udh)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<TSms>() == sms);

            return sms.Assign(s => s.Udh = udh);
        }

        public static TSms ValidForMinutes<TSms>(this TSms sms, int vaidity)
            where TSms : Sms
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<TSms>() == sms);

            return sms.Assign(s => s.Validity = vaidity);
        }
    }

}
