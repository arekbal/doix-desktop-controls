using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace doix.Wpf.Controls
{
    [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class DisposableGCHandle : IDisposable
    {
        GCHandle _handle;

        public DisposableGCHandle(object o)
        {
            _handle = GCHandle.Alloc(o, GCHandleType.Normal);
        }

        public DisposableGCHandle(object o, GCHandleType type)
        {
            _handle = GCHandle.Alloc(o, type);
        }

        public IntPtr AddrOfPinnedObject()
            => _handle.AddrOfPinnedObject();

        public void Free()
            => _handle.Free();

        public Object Target
        {
            get => _handle.Target;
            set => _handle.Target = value;
        }

        public bool IsAllocated => _handle.IsAllocated;
        
        protected void Dispose(bool disposing)
        {
            GC.SuppressFinalize(this);

            if (disposing)
            {
                if (_handle.IsAllocated)
                {
                    this._handle.Free();
                }
            }
        }

        public void Dispose() => this.Dispose(true);

        ~DisposableGCHandle()
        {
            this.Dispose(false);
        }
    }
}
