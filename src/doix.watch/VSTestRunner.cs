using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.watch
{
  static class VSTestRunner
  {
    public static Task<int> Run(IVSTestArgs args)
    {      
        var path = args.Path ?? Environment.CurrentDirectory;

        string TEST_ADAPTER_PATH = "/TESTADAPTERPATH:";

        bool foundTestAdapterPath = false;

        IEnumerable<string> iterArgs = args.Args;

        var arguments = args.Args.ToArray();

        var testAdapterPath = "/TestAdapterPath:";

        for (var i = 0; i < arguments.Length; i++)
        {
          if (arguments[i].ToUpper().StartsWith(TEST_ADAPTER_PATH))
          {
            foundTestAdapterPath = true;
          arguments[i] = $"{testAdapterPath}\"{arguments[i].Substring(TEST_ADAPTER_PATH.Length)}\"";
          }
        }

        if (foundTestAdapterPath == false)
        {
          iterArgs = arguments.Append($"{testAdapterPath}\"{path}\"");
        }

        var argsString = String.Join(" ", iterArgs) + " " + System.IO.Path.Combine(args.Path, args.FileName);

        Console.WriteLine("vstest.console.exe " + argsString);

        return ProcessRunner.Run("vstest.console.exe", argsString);      
    }
  }
}
