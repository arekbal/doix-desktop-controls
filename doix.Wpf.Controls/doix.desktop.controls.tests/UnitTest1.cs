using System;
using doix.core.controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace doix.desktop.controls.tests
{
  [TestClass]
  public class AnimationTests
  {
    [TestMethod]
    public void CubicInterpolation()
    {
      var anim = new Animation
      {
        StartTime = 0d,
        EndTime = 1d,
      };

      Func<double, double> method = anim.GetLinear;

      var x = method(0.25d);

      var y = method(0.5d);

      var z = method(0.75d);
      
      if (x != 0.25d)
        throw new Exception(x.ToString());

      if (z != 0.75d)
        throw new Exception(z.ToString());
    }
  }
}
