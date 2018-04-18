using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doix.watch
{
  interface IVSTestArgs
  {
    string Path { get; }
    string FileName { get; }
    IEnumerable<string> Args { get; }
  }
}
