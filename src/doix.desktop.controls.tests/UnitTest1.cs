using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using doix.desktop.core.controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace doix.desktop.controls.tests
{
  [TestClass]
  public class AnimationTests
  {
    class HelloWorld
    {
      public void Hahah()
      {
        Debug.WriteLine("Running Hahahah");
      }
    }
    
    public TestContext TestContext { get; set; }

    [TestInitialize]
    public void Init()
    {     
    }

    [TestCleanup]
    public void Cleanup()
    {
    }

    [TestMethod]
    public void CubicInterpolation()
    {
      new HelloWorld().Hahah();

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
