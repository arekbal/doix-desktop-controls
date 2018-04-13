using doix.core.controls.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doix.forms.controls.Behaviors
{
  public class MouseWheelOnMouseOverBehavior : IMessageFilter, IDisposable
  {
    Control _control;

    public MouseWheelOnMouseOverBehavior(Control control)
    {
      Application.AddMessageFilter(this);

      _control = control;
    }

    public bool PreFilterMessage(ref Message m)
    {
      if (m.Msg == 0x20a)
      {
        // WM_MOUSEWHEEL, find the control at screen position m.LParam
        var pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
        var hWnd = WinAPI.WindowFromPoint(pos);
        if (hWnd != IntPtr.Zero && hWnd != m.HWnd && Control.FromHandle(hWnd) == _control)
        {
          WinAPI.SendMessage(hWnd, m.Msg, m.WParam, m.LParam);
          return true;
        }
      }
      return false;
    }

    public void Dispose() 
      => Application.RemoveMessageFilter(this);
  }
}
