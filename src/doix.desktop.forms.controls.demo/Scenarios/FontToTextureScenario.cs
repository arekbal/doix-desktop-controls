using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using doix.desktop.core.controls;
using doix.desktop.core.controls.Helpers;
using GlmNet;
using SharpGL;

using static SharpGL.OpenGL;

namespace doix.forms.controls.demo.Scenarios
{
  class FontToTextureScenario : Base.SpriteBatchScenarioBase
  {
    FrameBuffer fbo;
    Color color;
    vec2[] points = new vec2[] { new vec2(0, 0), new vec2(100, 0), new vec2(150, 50), new vec2(150, 150), new vec2(100, 200), new vec2(50, 150) };

    protected override void OnInitialize()
    {
      base.OnInitialize();

      color = TestColor;

      fbo = FrameBuffer.Create(GL, 500, 500);
    }

    protected override void OnOpenGLDraw(object sender, RenderEventArgs args)
    {
      fbo.Bind();

      GL.Clear(0, 1, 1, 1);
      GL.Viewport(0, 0, 500, 500);
      GL.OrthoProj(500, 500);

      //using (SpriteBatch.BeginScope())
      //{
        //SpriteBatch.Rect(0, 0, 200, 200, Color.Red);

        // SpriteBatch.Fan(points.Reverse().ToArray(), color);
      //}

      //byte[] pixels = new byte[4];
      //GL.ReadPixels(1, 1, 1, 1, GL_BGRA, GL_UNSIGNED_BYTE, pixels);

      GL.Disable(GL_CULL_FACE);
      GL.Enable(GL_DEPTH_TEST);
      GL.Enable(GL_BLEND);
      GL.BlendFunc(GL_ONE, GL_ONE_MINUS_SRC_ALPHA);
      GL.BlendEquation(GL_FUNC_ADD_EXT);

      GL.DrawText(0, 0, 1, 1, 1, "Segoe UI Light", 80, "Hello Doods");

      FrameBuffer.Unbind(GL);
      GL.Clear(0, 0.6f, 1, 1);
      GL.Viewport(0, 0, Control.Width, Control.Height);
      GL.OrthoProj(Control.Width, Control.Height);

      using (SpriteBatch.BeginScope())
      {
        SpriteBatch.Rect(200, 100, 300, 300, fbo.Texture, Color.White);

        SpriteBatch.Rect(10, 10, 50, 50, Color.Green);

        //SpriteBatch.Fan(points.Reverse().ToArray(), color);
      }
    }

    protected override void Dispose(bool disposing)
    {
      //fbo.Destroy(); 
    }
  }
}
