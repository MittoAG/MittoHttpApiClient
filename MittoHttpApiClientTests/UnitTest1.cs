using MittoHttpApiClient;
using Xunit;

namespace MittoHttpApiClientTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var bla = Sms.CreateText().From("4915758918670").To("4915758918671").Text("TestText");
        }
    }
}
