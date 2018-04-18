using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.watch
{
  [Verb("vstest", HelpText = "Runs MSTest console runner on every change, requires mstest directory to be set in PATH.")]
  class VSTestVerb : IVSTestArgs
  {
    [Obsolete("temporary solution")]
    [Option("projpath", HelpText = "Path")]
    public string ProjPath { get; set; }

    [Option('p', "path", HelpText = "Path")]
    public string Path { get; set; }

    [Option('f', "file", Required = true, HelpText = "File")]
    public string FileName { get; set; }

    [Option('a', "args", HelpText = "Args", Separator = ' ')]
    public IEnumerable<string> Args { get; set; }
  }
}
