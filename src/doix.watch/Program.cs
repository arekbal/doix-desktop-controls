using CommandLine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace doix.watch
{
  class Program
  {
    class Options
    {
      //[Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
      //public IEnumerable<string> InputFiles { get; set; }

      // Omitting long name, defaults to name of property, ie "--verbose"
      [Option(Default = false, HelpText = "Prints all messages to standard output.")]
      public bool Verbose { get; set; }

      [Option('i', "interactive", Default = false, HelpText = "Sometimes stops process and waits for user input")]
      public bool Interactive { get; set; }

      [Value(0, MetaName = "offset", HelpText = "File offset.")]
      public long? Offset { get; set; }
    }

    static void Main(string[] args)
    {
      var result = Parser
        .Default
        .ParseArguments<VSTestVerb, Options>(args)
        .MapResult(
         (VSTestVerb opts) =>
         {
           var fileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(opts.ProjPath), "*.cs")
           {
             EnableRaisingEvents = true,
             IncludeSubdirectories = true,
           };

           do
           {
             //TODO: don't ignore all errors

             var msbuildResult = MSBuildRunner.Run(opts.ProjPath).GetAwaiter().GetResult();
             if (msbuildResult == 0)
               VSTestRunner.Run(opts).GetAwaiter().GetResult();

             Console.WriteLine();
             Console.WriteLine("Waiting for some changes to files...");
             var changes = fileSystemWatcher.WaitForChanged(WatcherChangeTypes.All);
             Console.WriteLine("Found changes...");
           }
           while (true);
         },
         (Options opts) => 0,
         errs => 1);

      Console.ReadKey();
      return;
    }
  }
}
