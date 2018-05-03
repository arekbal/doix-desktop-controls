using doix.utils;
using System;
using System.Collections.Immutable;
using System.Diagnostics;

namespace doix.collections
{
  [DebuggerDisplay("Count = {Count}")]
  public class Vector<T> : IMove<ArraySegment<T>>
    where T : struct
  {
    T[] _data;
    int _count;

    public int Count => _count;

    public int Capacity
    {
      get
      {
        AssertData();
        return _data.Length;
      }
      set
      {
        AssertData();
        if (_data.Length < value)
          ResizeArray(value);
      }
    }

    public Vector()
    {
      _data = new T[4];
    }

    public Vector(int initialCapacity)
    {
      _data = new T[initialCapacity];
    }

    public Vector(T[] items)
    {
      _data = new T[items.Length];

      Array.Copy(items, _data, items.Length);
    }

    public void Add(T item)
    {
      AssertData();

      if (_data.Length < _count + 1)
        ResizeArray(_count + 1);

      _count++;

      _data[_count - 1] = item;
    }

    public void AddRange(T[] items)
    {
      AssertData();

      if (_data.Length < _count + items.Length)
        ResizeArray(_count + items.Length);

      Array.Copy(items, 0, _data, _count - 1, items.Length);

      _count += items.Length;
    }

    public void AddRange(Span<T> items)
    {
      AssertData();

      if (_data.Length < _count + items.Length)
        ResizeArray(_count + items.Length);
   
      for(int i = 0; i < items.Length; ++i)
        _data[_count + i] = items[i];

      _count += items.Length;
    }

    public Span<T> Span
    {
      get
      {
        AssertData();
        return new Span<T>(_data, 0, _count);
      }
    }

    public void RemoveInOrder(int index)
    {
      AssertIndex(index);

      if (index != _count - 1)
      {
        Array.Copy(_data, index + 1, _data, index, _count - index - 1);       
      }    

      _count--;
    }
    
    public void RemoveWithSwap(int index)
    {  
      AssertIndex(index);

      if (index != _count - 1)
      {
        _data[index] = _data[_count];
      }

      _count--;
    }

    public void RemoveWhereWithSwap(Func<T, bool> filter)
    {
      for(var i = _count - 1; i > -1; --i)
      {
        if (filter(_data[i]))
          RemoveWithSwap(i);
      }
    }

    public void Reverse()
    {
      AssertData();

      Array.Reverse(_data, 0, _count);
    }

    public void Sort()
    {
      AssertData();

      Array.Sort(_data, 0, _count);
    }

    public ArraySegment<T> Move()
    {
      AssertData();

      var result = new ArraySegment<T>(_data, 0, _count);

      _data = null;
      _count = -1;

      return result;
    }

    public void Trim()
    {
      ResizeArray(_count, forceCapacity: true);
    }

    public void TrimToCapacity()
    {
      ResizeArray(_count, forceCapacity: true);
    }

    public void ResizeArray(int newCapacity, bool forceCapacity = false)
    {
      AssertData();

      var oldArr = _data;

      if (forceCapacity)
      {
        if (newCapacity == _data.Length)
          return;

        _data = new T[newCapacity];
      }
      else
      {
        var minBits = (int)Math.Log(newCapacity, 2) + 1;

        int newSize = 1;
        for (var iBits = 0; iBits < minBits; iBits++)
          newSize = newSize * 2;

        if (newSize == _data.Length)
          return;

        _data = new T[newSize];
      }

      if (_count > 0) 
        Array.Copy(oldArr, _data, _count);
    }

    public ref T this[int index]
    {
      get
      {
        AssertIndex(index);
        return ref _data[index];
      }
    }

    [Conditional(RuntimeMode.DEBUG), DebuggerHidden]
    void AssertData()
    {
      if(_data == null)
        throw new ArgumentOutOfRangeException("vector has moved and is inaccessible");
    }

    [Conditional(RuntimeMode.DEBUG), DebuggerHidden]
    void AssertIndex(int index)
    {
      AssertData();

      if (index >= _count)
        throw new ArgumentOutOfRangeException(nameof(index));
    }
  }
}
