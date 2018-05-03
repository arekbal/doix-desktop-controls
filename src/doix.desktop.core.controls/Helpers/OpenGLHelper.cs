using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

using static SharpGL.OpenGL;

namespace doix.desktop.core.controls.Helpers
{
  public static class OpenGLHelper
  {
    /// <summary> Changes color and clears </summary>
    public static void Clear(this OpenGL that, float r, float g, float b, float a)
    {
      that.ClearColor(r, g, b, a);
      that.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
    }

    public static void OrthoProj(this OpenGL that, int width, int height)
    {
      that.MatrixMode(GL_PROJECTION);
      that.LoadIdentity();
      that.Ortho(0, width, height, 0, -1, 1);

      that.MatrixMode(GL_MODELVIEW);
      that.LoadIdentity();
    }
  }
}
