using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doix.forms.controls.demo
{
  static class Program
  {

//#error Scroll/MouseWheel smoothing

//#error pixel snapping with fonts or whatever is wrong.


    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new DataGridDemoForm());
    }
  }
}
