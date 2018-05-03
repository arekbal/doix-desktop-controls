using System;
using System.Collections.Generic;
using System.Text;

namespace doix.utils
{
  public class ReversedComparer<T> : IComparer<T>
  {
    public int Compare(T x, T y) => Comparer<T>.Default.Compare(x, y) * -1;

    public static readonly ReversedComparer<T> Once = new ReversedComparer<T>();

    private ReversedComparer() { }
  }
}
