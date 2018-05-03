using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace doix.collections
{
  public class UnmanagedArray<T> : IDisposable
    where T : struct
  {
    public static readonly int SizeOfT = Marshal.SizeOf<T>();

    IntPtr _ptr;

    public readonly int Length;

    public UnmanagedArray(int length, bool initElems = true)
    {     
      _ptr = Marshal.AllocHGlobal(length * Marshal.SizeOf<T>());
      Length = length;

      if (initElems)
      {
        var span = this.Span;
        for (var i = 0; i < length; i++)
        {
          span[i] = new T();
        }
      }
    }

    public unsafe Span<T> Span => new Span<T>((void*)_ptr, Length);

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
    }

    ~UnmanagedArray()
    {
      OnDispose(false);
    }
  }
}
