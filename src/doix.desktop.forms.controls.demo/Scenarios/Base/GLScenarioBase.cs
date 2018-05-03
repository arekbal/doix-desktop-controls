using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using doix.desktop.core.controls;
using doix.desktop.forms.controls;

namespace doix.forms.controls.demo.Scenarios.Base
{
  abstract class GLScenarioBase : ScenarioBase
  {
    protected virtual GLControl CreateControl()
      => new GLControl();

    protected GLControl Control { get; private set; }

    protected SharpGL.OpenGL GL => Control.OpenGL;

    protected abstract void OnInitialize();

    public sealed override Control Initialize()
    {
      Control = CreateControl();
      OnInitialize();

      Control.OpenGLInitialized += this.OnOpenGLInitialized;
      Control.OpenGLDraw += this.OnOpenGLDraw;

      return Control;
    }

    protected virtual void OnOpenGLDraw(object sender, SharpGL.RenderEventArgs args)
    { }

    protected virtual void OnOpenGLInitialized(object sender, EventArgs e)
    { }

    public override void Clear()
    {
      Control.OpenGLInitialized -= this.OnOpenGLInitialized;
      Control.OpenGLDraw -= this.OnOpenGLDraw;
    }
  }
}
