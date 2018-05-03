using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace doix.utils
{
  public static class RuntimeMode
  {
    public const string DEBUG = nameof(DEBUG);

    public static bool IsDebug { get; private set; }

    static RuntimeMode()
    {
      CheckDebug();
    }
 
    [Conditional(DEBUG)]
    static void CheckDebug()
    {
      IsDebug = true;
    }
  }
}
