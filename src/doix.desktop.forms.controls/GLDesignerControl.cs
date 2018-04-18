using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SharpGL.OpenGL;

namespace doix.desktop.forms.controls
{
  [Browsable(false)]
  public class GLDesignerControl : OpenGLControl
  {
    protected bool IsDesignTime { get; }

    public GLDesignerControl()
    {
      IsDesignTime = LicenseManager.UsageMode == LicenseUsageMode.Designtime;      

      if (IsDesignTime)
      {       
        OpenGLInitialized += this.OnGLInitialized;
        OpenGLDraw += this.OnGLDraw;
      }

      this.SizeChanged += (s, e) => { };
    }

    private void OnGLInitialized(object sender, EventArgs e)
    {
    }

    int x = 0;

    private void OnGLDraw(object sender, RenderEventArgs args)
    {
      var z = Math.Abs((x - 128) * 2) / 255.0f;

      OpenGL.ClearColor(z * .38f + .3f, z * .84f + .08f, z * .68f + .2f, 1f);
      OpenGL.Clear(GL_COLOR_BUFFER_BIT);
    
      if (255 < x++)
        x = 0;
    }

    private void InitializeComponent()
    {
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // GLDesignerControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.FrameRate = 60;
      this.Name = "GLDesignerControl";
      this.RenderContextType = SharpGL.RenderContextType.FBO;
      this.Size = new System.Drawing.Size(1002, 673);
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);

    }
  }
}
