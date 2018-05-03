using doix.utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace doix.collections
{
  public class UnmanagedVector<T> : IMove<ArraySegment<T>>, IEnumerable<T>
    where T : struct
  {
    public static readonly int SizeOfT = Marshal.SizeOf<T>();

    int _length;
    IntPtr _ptr;
    int _count;

    public int Count => _count;

    public int Capacity
    {
      get
      {
        AssertData();
        return _length;
      }
      set
      {
        AssertData();
        if (_length < value)
          ResizePtr(value);
      }
    }

    public UnmanagedVector()
    {
      _length = 4;
      _ptr = Marshal.AllocHGlobal(_length * SizeOfT);
    }

    public UnmanagedVector(int initialCapacity)
    {
      _length = initialCapacity;
      _ptr = Marshal.AllocHGlobal(initialCapacity * SizeOfT);
    }

    public void Add(T item)
    {
      AssertData();

      if (_length < _count + 1)
        ResizePtr(_count + 1);

      _count++;

      Span[_count - 1] = item;      
    }

    public void AddRange(T[] items)
    {
      AssertData();

      if (_length < _count + items.Length)
        ResizePtr(_count + items.Length);

      var span = Span;
      for (var i = 0; i < items.Length; i++)
        span[_count + i] = items[i];

      _count += items.Length;
    }

    public void AddRange(Span<T> items)
    {
      AssertData();

      if (_length < _count + items.Length)
        ResizePtr(_count + items.Length);

      var span = Span;

      for (int i = 0; i < items.Length; ++i)
        span[_count + i] = items[i];

      _count += items.Length;
    }

    public unsafe Span<T> Span
    {
      get
      {
        AssertData();
        return new Span<T>((void*)_ptr, _count);
      }
    }

    public unsafe void RemoveInOrder(int index)
    {
      AssertIndex(index);

      if (index != _count - 1)
      {
        var bytesToCopy = (_count - index) * SizeOfT;
        Buffer.MemoryCopy((void*)(_ptr + SizeOfT * (index + 1)), (void*)(_ptr + SizeOfT * index), bytesToCopy, bytesToCopy);
      }

      _count--;
    }

    public void RemoveWithSwap(int index)
    {
      AssertIndex(index);

      if (index != _count - 1)
      {
        var span = Span;
        span[index] = span[_count];
      }

      _count--;
    }

    public void RemoveWhereWithSwap(Func<T, bool> filter)
    {
      var span = Span;
      for (var i = _count - 1; i > -1; --i)
      {
        if (filter(span[i]))
          RemoveWithSwap(i);
      }
    }

    public void Reverse()
    {
      AssertData();

      var span = Span;

      for(int i = 0; i < _count / 2; i++)
      {
        var iBack = _count - 1 - i;

        var temp = span[i];
        span[i] = span[iBack];
        span[iBack] = temp;
      }
    }

    public ArraySegment<T> Move()
    {
      AssertData();

      var result = new ArraySegment<T>(Span.ToArray());

      if(_ptr != IntPtr.Zero)
        Marshal.FreeHGlobal(_ptr);

      _ptr = IntPtr.Zero;
      _length = -1;
      _count = -1;

      return result;
    }

    public void Trim()
    {
      ResizePtr(_count, forceCapacity: true);
    }

    unsafe void ResizePtr(int newCapacity, bool forceCapacity = false)
    {
      AssertData();

      var oldArr = _ptr;

      if (forceCapacity)
      {
        if (newCapacity == _length)
          return;

        if (_ptr != IntPtr.Zero)
          _ptr = Marshal.ReAllocHGlobal(_ptr, (IntPtr)(newCapacity * SizeOfT));

        _length = newCapacity;
      }
      else
      {
        var minBits = (int)Math.Log(newCapacity, 2) + 1;

        int newSize = 1;
        for (var iBits = 0; iBits < minBits; iBits++)
          newSize = newSize * 2;

        if (newSize == _length)
          return;

        if (_ptr != IntPtr.Zero)
          _ptr = Marshal.ReAllocHGlobal(_ptr, (IntPtr)(newSize * SizeOfT));
       
        _length = newSize;
      }

      if (_count > 0)
      {
        
//#error Copy ?!?
//        Buffer.MemoryCopy()
 //       // Array.Copy(oldArr, _data, _count);
      }
    }

    public ref T this[int index]
    {
      get
      {
        AssertIndex(index);
        return ref Span[index];
      }
    }

    bool _isDisposed;

    void OnDispose(bool disposing)
    {
      if (_isDisposed)
        return;

      _isDisposed = true;

      if(_ptr != IntPtr.Zero)
        Marshal.FreeHGlobal(_ptr);
    }

    public void Dispose()
    {
      OnDispose(true);
      GC.SuppressFinalize(this);
    }

    ~UnmanagedVector()
    {
      OnDispose(false);
    }

    [Conditional(RuntimeMode.DEBUG), DebuggerHidden]
    void AssertData()
    {
      if (_ptr == IntPtr.Zero)
        throw new ArgumentOutOfRangeException("unmanaged vector has moved and is inaccessible");
    }

    [Conditional(RuntimeMode.DEBUG), DebuggerHidden]
    void AssertIndex(int index)
    {
      AssertData();

      if (index >= _count)
        throw new ArgumentOutOfRangeException(nameof(index));
    }

    public IEnumerator<T> GetEnumerator()
    {
      for(int i = 0; i < _count; i++)
        yield return Marshal.PtrToStructure<T>(_ptr + (i * SizeOfT));
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      for (int i = 0; i < _count; i++)
        yield return Marshal.PtrToStructure<T>(_ptr + (i * SizeOfT));
    }
  }
}
