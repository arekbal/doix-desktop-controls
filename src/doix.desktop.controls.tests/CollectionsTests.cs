using doix.collections;
using doix.utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace doix.desktop.controls.tests
{
  struct Puple
  {
    public byte x;
    public byte y;
    public byte z;
    public int w;
    public double m;

    public Puple(byte x, byte y, byte z)
    {
      this.x = x;
      this.y = y;
      this.z = z;
      this.w = 7;
      this.m = 86;
    }
  }

  [TestFixture]
  public class CollectionsTests
  {
    [Test]
    public unsafe void ListVTest()
    {
      var count = 4000;
      using (var arr = new UnmanagedArray<Puple>(count, false))
      {
        var span = arr.Span;
        for (var i = 0; i < count; i++)
        {
          span[i] = new Puple((byte)i, (byte)(i + 1), (byte)(i + 2));
        }
      }

      var m = stackalloc Puple[3];

      var g = m;

      var e = new Span<Puple>(g, 1);

      //for (var i = 0; i < 3; i++)
     // {
        e[0] = new Puple((byte)1, (byte)2, (byte)3);
      //}

      var vector = new UnmanagedVector<Puple>(1000);

      for (var i = 0; i < 1000; i++)
      {
        vector.Add(new Puple((byte)i, (byte)(i + 1), (byte)(i + 2)));
      }

      vector.Reverse();

      var moveOut = vector.SortedDescending().ToArray().AsSpan();

      var x = moveOut;

      var y = x.Slice(3, 7);
      y[3] = new Puple(132, 1, 1);

      var z = y.Slice(0, 2);
    }
  }
}
