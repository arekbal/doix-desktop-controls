using doix.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace doix.collections
{
  public static class MoveOutExtensions
  {
    public static ArraySegment<T> Sorted<T>(this IMove<ArraySegment<T>> that)
    {
      var moved = that.Move();
      Array.Sort(moved.Array, moved.Offset, moved.Count);
      return moved;
    }

    public static ArraySegment<T> SortedDescending<T>(this IMove<ArraySegment<T>> that)
    {
      var moved = that.Move();
      Array.Sort(moved.Array, moved.Offset, moved.Count, ReversedComparer<T>.Once);
      return moved;
    }
  }
}
