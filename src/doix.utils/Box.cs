using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace doix.utils
{
  public class Box<T> : IDisposable
      where T : struct
  {
    public static readonly int SizeofT = Marshal.SizeOf<T>();

    IntPtr _ptr;

    public Box()
    {
      _ptr = Marshal.AllocHGlobal(Marshal.SizeOf<T>());
    }

    bool _isDisposed;

    void OnDispose(bool disposing)
    {
      if (_isDisposed)
        return;

      _isDisposed = true;

      Marshal.FreeHGlobal(_ptr);
    }

    public void Dispose()
    {
      OnDispose(true);
      GC.SuppressFinalize(this);
    }

    ~Box()
    {
      OnDispose(false);
    }
  }
}
