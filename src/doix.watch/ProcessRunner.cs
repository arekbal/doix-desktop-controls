using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace doix.watch
{
  static class ProcessRunner
  {
    public static async Task<int> Run(string filepath, string args)
    {
      var procInfo = new ProcessStartInfo(filepath, args)
      {
        RedirectStandardError = true,
        RedirectStandardOutput = true,
        ErrorDialog = false,
        CreateNoWindow = true,
        UseShellExecute = false
      };

      try
      {
        using (var proc = Process.Start(procInfo))
        {
          do
          {
            await Task.Yield();
            Console.Out.Write(proc.StandardOutput.ReadToEnd());

            var color0 = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Out.Write(proc.StandardError.ReadToEnd());
            Console.ForegroundColor = color0;
          }
          while (!proc.HasExited);

          Console.Out.Write(proc.StandardOutput.ReadToEnd());

          var color1 = Console.ForegroundColor;
          Console.ForegroundColor = ConsoleColor.DarkRed;
          Console.Out.Write(proc.StandardError.ReadToEnd());
          Console.ForegroundColor = color1;
          
          return proc.ExitCode;
        }
      }
      catch (Exception ex)
      {
        var color2 = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.Out.Write(ex);
        Console.ForegroundColor = color2;

        return -1;
      }

    }
  }
}
