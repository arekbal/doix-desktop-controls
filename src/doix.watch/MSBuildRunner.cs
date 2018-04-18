using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.watch
{
  static class MSBuildRunner
  {
    public static Task<int> Run(string projPath)
    {
      return ProcessRunner.Run("msbuild.exe", projPath + " /t:Rebuild");
    }
  }
}
