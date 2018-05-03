using doix.desktop.core.controls;
using GlmNet;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.desktop.controls.tests
{
  [TestFixture]
  public class RectComparerTest
  {
    [Test]
    public void HitTest()
    {
      vec4 rect0 = new vec4(0, 0, 1, 1);
      vec4 rect1 = new vec4(-1, 0, 1.5f, 1);

      var overlap0 = HitTests.HitTest(rect0, rect1) != HitTestResult.Apart;
      Assert.IsTrue(overlap0);

      rect0 = new vec4(5, 0, 1, 1);
      rect1 = new vec4(-1, 0, 1.5f, 1);

      var overlap1 = HitTests.HitTest(rect0, rect1) != HitTestResult.Apart;
      Assert.IsFalse(overlap1);

      rect0 = new vec4(0, 0, 1, 1);
      rect1 = new vec4(1, 0, 1, 1);

      var overlap2 = HitTests.HitTest(rect0, rect1) != HitTestResult.Apart;
      Assert.IsFalse(overlap2);
    }
  }
}
