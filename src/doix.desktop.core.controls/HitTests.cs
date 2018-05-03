using GlmNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static doix.desktop.core.controls.HitTestResult;

namespace doix.desktop.core.controls
{
  public static class HitTests
  {
    public static HitTestResult HitTest(vec4 currRect, vec4 otherRect)
    {
      var hitTestX = HitTest(currRect.x, currRect.x + currRect.z, otherRect.x, otherRect.x + otherRect.z);
      var hitTestY = HitTest(currRect.y, currRect.y + currRect.w, otherRect.y, otherRect.y + otherRect.w);

      if (hitTestX == A_Contains_B && hitTestY == A_Contains_B)
        return A_Contains_B;

      if (hitTestX == B_Contains_A && hitTestY == B_Contains_A)
        return B_Contains_A;

      if (hitTestX != Apart && hitTestY != Apart)
        return Intersection;

      return Apart;
    }

    public static HitTestResult HitTest(float a0, float a1, float b0, float b1)
    {
      if (a0 >= a1)
        throw new ArgumentException("a0 has to be lower than a1");

      if (b0 >= b1)
        throw new ArgumentException("b0 has to be lower than b1");

      if (a0 < b0)
      {
        if (a1 <= b0)
          return Apart;

        if (a1 >= b1)
          return A_Contains_B;

        return A_Intersects_B;
      }

      if (a0 >= b1)
        return Apart;

      // a0 is within b
      if (a1 <= b1)
        return B_Contains_A;

      return B_Intersects_A;
    }
  }
}
