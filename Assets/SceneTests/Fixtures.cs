using System;

namespace VContainer.Tests
{
    public interface I1
    {
    }

    interface I2
    {
    }

    public class DisposableServiceA : I1, IDisposable
    {
        public bool Disposed { get; private set; }
        public void Dispose() => Disposed = true;
    }

    public class DisposableServiceB : I2, IDisposable
    {
        public bool Disposed { get; private set; }
        public void Dispose() => Disposed = true;
    }
}
