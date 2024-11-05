using R3;
using System;
using System.Threading;

namespace Life.Utilities
{
    public abstract class Service : IDisposable
    {
        readonly CancellationTokenSource disposeCancellationTokenSource = new();
        readonly CompositeDisposable disposables = new();

        public CancellationToken DisposeCancellationToken => disposeCancellationTokenSource.Token;
        public CompositeDisposable Disposables => disposables;

        void IDisposable.Dispose()
        {
            disposeCancellationTokenSource.Cancel();
            disposeCancellationTokenSource.Dispose();

            Dispose();
        }

        protected virtual void Dispose() { }
    }
}

