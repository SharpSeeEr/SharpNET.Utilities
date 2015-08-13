using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNET.Utilities
{
    /// <summary>
    /// Provides all methods needed to implement a Disposable class.
    /// Inheriting class overrides the DisposeManaged() and DisposeUnmanaged() to dispose of IDisposable members.
    /// </summary>
    public abstract class DisposableClass : IDisposable
    {
        bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableClass()
        {
            Dispose(false);
        }

        protected virtual void DisposeManaged()
        {

        }

        protected virtual void DisposeUnmanaged()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                DisposeManaged();
            }

            // release any unmanaged objects
            // set the object references to null
            DisposeUnmanaged();

            _disposed = true;
        }
    }
}
