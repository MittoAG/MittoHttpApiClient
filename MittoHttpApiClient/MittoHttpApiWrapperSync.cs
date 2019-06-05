using System;
using System.Diagnostics.Contracts;

namespace MittoHttpApiClient
{
    internal sealed class MittoHttpApiWrapperSync : IDisposable
    {
        #region Init

        private bool _disposed;
        private readonly Lazy<MittoHttpApiWrapperAsync> _asyncWrapper;

        public MittoHttpApiWrapperSync(string apiUrl, string apiKey)
        {
            _asyncWrapper = new Lazy<MittoHttpApiWrapperAsync>(() => new MittoHttpApiWrapperAsync(apiUrl, apiKey));
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MittoHttpApiWrapperSync()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_asyncWrapper.IsValueCreated)
                    {
                        _asyncWrapper.Value.Dispose();
                    }
                }

                _disposed = true;
            }
        }

        #endregion

        #region Methods

        public SendSmsResponse SendSms(SendSmsRequest request)
        {
            Contract.Requires(request != null);
            Contract.Ensures(Contract.Result<SendSmsResponse>() != null);

            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(MittoHttpApiWrapperSync));
            }

            return _asyncWrapper.Value.SendSmsAsync(request).Result;
        }

        #endregion

    }
}
