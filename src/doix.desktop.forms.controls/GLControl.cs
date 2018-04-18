using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;

using static SharpGL.OpenGL;

namespace doix.desktop.forms.controls
{
  public partial class GLControl : GLDesignerControl
  {
    public GLControl()
    {
      InitializeComponent();

      //base.OpenGLDraw += this.OnGLDraw;
    }

    private void OnGLDraw(object sender, RenderEventArgs args)
    {
      this.OpenGL.ClearColor(0.0f, 1.0f, 1.0f, 0.0f);
      this.OpenGL.Clear(GL_COLOR_BUFFER_BIT);

      //args.Graphics.Clear(Color.CornflowerBlue);
    }
  }
}
