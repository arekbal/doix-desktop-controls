using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using SharpGL.SceneGraph.Assets;
using static SharpGL.OpenGL;

namespace doix.desktop.core.controls
{
  public class FrameBuffer
  {
    public OpenGL GL { get; private set; }

    uint[] frameBufferIds;
    uint[] renderBufferIds;

    private FrameBuffer(OpenGL gl, uint[] frameBufferIds, uint[] renderBufferIds, Texture texture)
    {
      this.GL = gl;
      this.frameBufferIds = frameBufferIds;
      this.renderBufferIds = renderBufferIds;
      this.Texture = texture;
    }

    public void Bind()
    {
      GL.BindRenderbufferEXT(GL_RENDERBUFFER, renderBufferIds[0]);
      GL.BindFramebufferEXT(GL_FRAMEBUFFER_EXT, frameBufferIds[0]);
    }

    public static void Unbind(OpenGL gl)
    {
      gl.BindRenderbufferEXT(GL_RENDERBUFFER, 0);
      gl.BindFramebufferEXT(GL_FRAMEBUFFER_EXT, 0);
    }

    public Texture Texture
    {
      get; private set;
    }

    public static FrameBuffer Create(OpenGL gl, int width, int height)
    {
      Texture tex = new Texture();
      tex.Create(gl);
      tex.Bind(gl);

      gl.TexParameter(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
      gl.TexParameter(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
      gl.TexParameter(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
      gl.TexParameter(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);
      gl.TexImage2D(GL_TEXTURE_2D, 0, GL_RGBA8, width, height, 0, GL_BGRA, GL_UNSIGNED_BYTE, null);     
      gl.BindTexture(GL_TEXTURE_2D, 0);

      //------
      uint[] frameBufferIds = new uint[1];
      gl.GenFramebuffersEXT(1, frameBufferIds);
      gl.BindFramebufferEXT(GL_FRAMEBUFFER_EXT, frameBufferIds[0]);
      gl.FramebufferTexture2DEXT(GL_FRAMEBUFFER_EXT, GL_COLOR_ATTACHMENT0_EXT, GL_TEXTURE_2D, tex.TextureName, 0);

      //------
      uint[] renderBufferIds = new uint[1];
      gl.GenRenderbuffersEXT(1, renderBufferIds);
      gl.BindRenderbufferEXT(GL_RENDERBUFFER_EXT, renderBufferIds[0]);
      gl.RenderbufferStorageEXT(GL_RENDERBUFFER_EXT, GL_DEPTH_COMPONENT24, width, height);
      //-------------------------
      //Attach depth buffer to FBO
      gl.FramebufferRenderbufferEXT(GL_FRAMEBUFFER_EXT, GL_DEPTH_ATTACHMENT_EXT, GL_RENDERBUFFER_EXT, renderBufferIds[0]);
      //------

      var status = gl.CheckFramebufferStatusEXT(GL_FRAMEBUFFER_EXT);
      if (status == GL_FRAMEBUFFER_COMPLETE_EXT)
      {
        FrameBuffer.Unbind(gl);
        return new FrameBuffer(gl, frameBufferIds, renderBufferIds, tex);
      }

      switch (status)
      {
        //case GL_FRAMEBUFFER_UNDEFINED_EXT:
        //  throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_UNDEFINED_EXT));
        case GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT_EXT:
          throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT_EXT));
        case GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER_EXT:
          throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER_EXT));
        case GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT_EXT:
          throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT_EXT));
        case GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS_EXT:
          throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS_EXT));
        case GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE_EXT:
          throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE_EXT));
        case GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER_EXT:
          throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER_EXT));
        case GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS:
          throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS));
        case GL_FRAMEBUFFER_INCOMPLETE_FORMATS_EXT:
          throw new InvalidOperationException(nameof(GL_FRAMEBUFFER_INCOMPLETE_FORMATS_EXT));
        default:
          throw new InvalidOperationException("GL_FRAMEBUFFER unknown error");
      }
    }

    //public int Width
    //{
    //  get => GL.FramebufferParameter(GL_FRAMEBUFFER_EXT, GL_FRAME, )
    //}

    //public int Height 

    private bool isDisposed = false; // To detect redundant calls

    public void Destroy()
    {
      Texture.Destroy(GL);

      if (renderBufferIds[0] != 0)
        GL.DeleteRenderbuffersEXT(1, renderBufferIds);

      FrameBuffer.Unbind(GL);

      if (frameBufferIds[0] != 0)
        GL.DeleteFramebuffersEXT(1, frameBufferIds);

      GL = null;
    }
  }
}
