using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.desktop.core.controls
{
  public enum HitTestResult
  {
    Apart = 0,

    A_Contains_B = 2,
    B_Contains_A = 3,

    Intersection = 4,
    A_Intersects_B = 5,
    B_Intersects_A = 6,   
  }
}
