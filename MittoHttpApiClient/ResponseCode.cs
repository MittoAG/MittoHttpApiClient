using System.ComponentModel;

namespace MittoHttpApiClient
{
    public enum ResponseCode
    {
        [Description("SMS sent")]
        Success = 0,
        [Description("Internal error")]
        InternalError = 1,
        [Description("Invalid type")]
        TypeInvalid = 2,
        [Description("Empty message")]
        MessageEmpty = 3,
        [Description("Message text is invalid")]
        MessageTextInvalid = 4,
        [Description("Empty sender")]
        NoSender = 5,
        [Description("Invalid sender")]
        SenderInvalid = 6,
        [Description("Empty receiver")]
        NoReceiver = 7,
        [Description("Invalid receiver")]
        ReceiverInvalid = 8
    }
}
