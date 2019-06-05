using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace MittoHttpApiClient
{
    [ContractClass(typeof(MittoHttpApiClientAsyncContracts))]
    public interface IMittoHttpApiClientAsync
    {
        Task<SendSmsResult> SendSmsAsync(Sms sms);
    }

    [ContractClassFor(typeof(IMittoHttpApiClientAsync))]
    internal abstract class MittoHttpApiClientAsyncContracts : IMittoHttpApiClientAsync
    {
        public Task<SendSmsResult> SendSmsAsync(Sms sms)
        {
            Contract.Requires(sms != null);
            Contract.Ensures(Contract.Result<Task<SendSmsResult>>() != null);

            return default(Task<SendSmsResult>);
        }
    }
}
