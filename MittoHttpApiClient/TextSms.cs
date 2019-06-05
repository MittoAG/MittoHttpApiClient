using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;

namespace MittoHttpApiClient
{
    public sealed class TextSms : Sms
    {
        private static class CharacterSetHelper
        {
            private const string BasicSetIndexed = "@£$¥èéùìòÇ\nØø\rÅåΔ_ΦΓΛΩΠΨΣΘΞ\x1bÆæßÉ !\"#¤%&'()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà";
            private const string ExtensionSetIndexed = "````````````````````^```````````````````{}`````\\````````````[~]`|````````````````````````````````````€``````````````````````````";
            private const int EscIndex = 27;
            private const char PlaceHolderChar = '`';

            private const string BasicSet = BasicSetIndexed;
            private static readonly string ExtensionSet = new string(ExtensionSetIndexed.Except(new[] { PlaceHolderChar }).ToArray());

            private static readonly string WholeSet = string.Concat(BasicSet, ExtensionSet);

            public static readonly Regex AllowedUtf8GsmCharsRegex = new Regex(string.Concat("[", EscapeForRegex(WholeSet), "]"), RegexOptions.Compiled);

            private static string EscapeForRegex(string source) => new string(source.Select(c => (int)c).SelectMany(i => i > 0xFF ? ((char)i).ToString() : string.Concat("\\x", i.ToString("X2"), "")).ToArray());

            public static string ConvertUtf8ToGsmEncodedString(string text)
            {
                // Use this list to store the index of the character in 
                // the basic/extension character sets
                var indicies = new List<int>();

                foreach (var c in text)
                {
                    if (c == PlaceHolderChar)
                    {
                        continue;
                    }

                    var index = BasicSetIndexed.IndexOf(c);
                    if (index != -1)
                    {
                        indicies.Add(index);
                        continue;
                    }

                    index = ExtensionSetIndexed.IndexOf(c);
                    if (index != -1)
                    {
                        // Add the 'ESC' character index before adding 
                        // the extension character index
                        indicies.Add(EscIndex);
                        indicies.Add(index);
                    }
                }

                return new string(indicies.Select(i => (char)i).ToArray());
            }

            public static string ConvertTextToUnicodeHexString(string text)
            {
                return string.Join(string.Empty, text.Select(c => ((int)c).ToString("X4")));
            }
        }

        public string Text { get; set; }

        public bool IsUnicode { get; set; }

        public bool AutomaticUnicodeRecognition { get; set; }

        internal override SendSmsRequest ToSendSmsRequest()
        {
            Contract.Ensures(Contract.Result<SendSmsRequest>() != null);

            var request = base.ToSendSmsRequest();
            if (IsUnicode || (AutomaticUnicodeRecognition && CharacterSetHelper.AllowedUtf8GsmCharsRegex.IsMatch(Text)))
            {
                request.Text = CharacterSetHelper.ConvertTextToUnicodeHexString(Text);
                request.Type = MessageContentType.Unicode;
            }
            else
            {
                request.Text = CharacterSetHelper.ConvertUtf8ToGsmEncodedString(Text);
                request.Type = MessageContentType.GSM;
            }

            return request;
        }
    }

}
