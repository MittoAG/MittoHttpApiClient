using MittoHttpApiClient;
using Xunit;

namespace MittoHttpApiClientTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var bla = Sms.CreateText().initClient("", "").From("").To("").Text("").Send();
        }
    }
}
