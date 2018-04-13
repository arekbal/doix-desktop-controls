using GlmNet;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static SharpGL.OpenGL;

namespace doix.core.controls
{
  public class SpriteBatch : IDisposable
  {
    public static class VertexAttributes
    {
      static uint _index = 0;
      public static readonly uint Pos = _index++;
      public static readonly uint Color = _index++;
      public static readonly uint TexCoord = _index++;
    }

    SharpGL.Shaders.ShaderProgram _shaderProgram = new SharpGL.Shaders.ShaderProgram();

    VertexPosTexColor[] vertices;
    int[] indices;

    int vertexCount;
    int indexCount;
    Texture currTexture;

    uint[] vertexBufferIds = new uint[1];
    uint[] indexBufferIds = new uint[1];
    uint[] vaos = new uint[1];

    Texture white1x1Tex = new Texture();

    OpenGL gl;

    public SpriteBatch(OpenGL gl, int triCount = 4000)
    {
      this.gl = gl;

      var textureLoaded = this.white1x1Tex.Create(gl, Properties.Resources.white1x1);
      if (textureLoaded == false)
        throw new Exception("white1x1Tex loading failed");

      _shaderProgram.Create(gl, Properties.Resources.SimpleVS, Properties.Resources.SimplePS, null);

      _shaderProgram.BindAttributeLocation(gl, VertexAttributes.Pos, nameof(VertexAttributes.Pos));
      _shaderProgram.BindAttributeLocation(gl, VertexAttributes.Color, nameof(VertexAttributes.Color));
      _shaderProgram.BindAttributeLocation(gl, VertexAttributes.TexCoord, nameof(VertexAttributes.TexCoord));

      _shaderProgram.Bind(gl);

      vertices = new VertexPosTexColor[triCount * 3];
      indices = new int[triCount * 3];

      gl.GenBuffers(1, vertexBufferIds);
      gl.BindBuffer(GL_ARRAY_BUFFER, vertexBufferIds[0]);
      gl.BufferData(GL_ARRAY_BUFFER, vertices.Length * VertexPosTexColor.SizeOf, IntPtr.Zero, GL_DYNAMIC_DRAW);

      gl.GenBuffers(1, indexBufferIds);
      gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, indexBufferIds[0]);
      gl.BufferData(GL_ELEMENT_ARRAY_BUFFER, indices.Length * sizeof(int), IntPtr.Zero, GL_DYNAMIC_DRAW);

      gl.GenVertexArrays(1, vaos);
      gl.BindVertexArray(vaos[0]);

      gl.EnableVertexAttribArray(VertexAttributes.Pos);
      gl.VertexAttribPointer(VertexAttributes.Pos, 3, GL_FLOAT, false, VertexPosTexColor.SizeOf, new IntPtr(0));

      gl.EnableVertexAttribArray(VertexAttributes.Color);
      gl.VertexAttribPointer(VertexAttributes.Color, 4, GL_UNSIGNED_BYTE, true, VertexPosTexColor.SizeOf, new IntPtr(3 * sizeof(float)));

      gl.EnableVertexAttribArray(VertexAttributes.TexCoord);
      gl.VertexAttribPointer(VertexAttributes.TexCoord, 2, GL_FLOAT, false, VertexPosTexColor.SizeOf, new IntPtr(3 * sizeof(float) + 4 * sizeof(byte)));
    }

    public void Begin()
    {
      gl.Disable(GL_CULL_FACE);

      gl.Enable(GL_DEPTH_TEST);

      gl.Enable(GL_BLEND);

      gl.BlendFunc(GL_ONE, GL_ONE_MINUS_SRC_ALPHA);

      gl.BlendEquation(GL_FUNC_ADD_EXT);

      _shaderProgram.Bind(gl);

      gl.BindBuffer(GL_ARRAY_BUFFER, vertexBufferIds[0]);

      gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, indexBufferIds[0]);

      gl.BindVertexArray(vaos[0]);

      currTexture = white1x1Tex;
    }

    protected void Flush()
    {
      if (indexCount == 0)
        return;

      currTexture.Bind(gl);

      using (var handle = new DisposableGCHandle(vertices, GCHandleType.Pinned))
      {
        gl.BufferSubData(GL_ARRAY_BUFFER, 0, vertexCount * VertexPosTexColor.SizeOf, handle.AddrOfPinnedObject());
      }

      using (var handle = new DisposableGCHandle(indices, GCHandleType.Pinned))
      {
        gl.BufferSubData(GL_ELEMENT_ARRAY_BUFFER, 0, indexCount * sizeof(int), handle.AddrOfPinnedObject());
      }

      gl.DrawElements(GL_TRIANGLES, indexCount, GL_UNSIGNED_INT, new IntPtr(0));

      vertexCount = 0;
      indexCount = 0;
    }

    public void End()
    {
      if (indexCount == 0)
        return;

      Flush();
    }

    protected void Vertex(VertexPosTexColor vertex)
    {
      vertices[vertexCount] = vertex;
      vertexCount++;
    }

    protected void Index()
    {
      indices[indexCount] = vertexCount;

      indexCount++;
    }

    public void Triangle(VertexPosTexColor v0, VertexPosTexColor v1, VertexPosTexColor v2, Texture tex = null)
    {
      tex = tex ?? white1x1Tex;

      TrySetTexture(tex);

      Index();
      Vertex(v0);

      Index();
      Vertex(v1);

      Index();
      Vertex(v2);
    }

    public void Triangle(vec2 v0, vec2 v1, vec2 v2, Color c, float z = 0)
    {
      if (currTexture != white1x1Tex)
      {
        Flush();
        currTexture = white1x1Tex;
      }

      Index();
      Vertex(new VertexPosTexColor { PosX = v0.x, PosY = v0.y, PosZ = z, ColorR = c.R, ColorG = c.G, ColorB = c.B, ColorA = c.A });

      Index();
      Vertex(new VertexPosTexColor { PosX = v1.x, PosY = v1.y, PosZ = z, ColorR = c.R, ColorG = c.G, ColorB = c.B, ColorA = c.A });

      Index();
      Vertex(new VertexPosTexColor { PosX = v2.x, PosY = v2.y, PosZ = z, ColorR = c.R, ColorG = c.G, ColorB = c.B, ColorA = c.A });
    }

    /// <summary> have to be listed in winding order </summary>
    public void Quad(vec2 v0, vec2 v1, vec2 v2, vec2 v3, Color c, float z = 0)
        => Quad(v0, v1, v2, v3, white1x1Tex, c, z);

    /// <summary> have to be listed in winding order </summary>
    public void Quad(vec2 v0, vec2 v1, vec2 v2, vec2 v3, Texture tex, Color tint, float z = 0)
    {
      TrySetTexture(tex);

      Index();
      Vertex(new VertexPosTexColor { PosX = v0.x, PosY = v0.y, PosZ = z, TexCoordX = 0.0f, TexCoordY = 0.0f, ColorR = tint.R, ColorG = tint.G, ColorB = tint.B, ColorA = tint.A });

      Index();
      Vertex(new VertexPosTexColor { PosX = v1.x, PosY = v1.y, PosZ = z, TexCoordX = 1.0f, TexCoordY = 0.0f, ColorR = tint.R, ColorG = tint.G, ColorB = tint.B, ColorA = tint.A });

      Index();
      Vertex(new VertexPosTexColor { PosX = v2.x, PosY = v2.y, PosZ = z, TexCoordX = 1.0f, TexCoordY = 1.0f, ColorR = tint.R, ColorG = tint.G, ColorB = tint.B, ColorA = tint.A });

      // 2, 3, 0 
      indices[indexCount] = indices[indexCount - 1];
      indexCount++;

      indices[indexCount] = indices[indexCount - 1] + 1;
      indexCount++;

      indices[indexCount] = indices[indexCount - 5];
      indexCount++;

      Vertex(new VertexPosTexColor { PosX = v3.x, PosY = v3.y, PosZ = z, TexCoordX = 0.0f, TexCoordY = 1.0f, ColorR = tint.R, ColorG = tint.G, ColorB = tint.B, ColorA = tint.A });
    }

    public void Rect(float x, float y, float width, float height, Color c, float z = 0)
        => Quad(new vec2(x, y), new vec2(x + width, y), new vec2(x + width, y + height), new vec2(x, y + height), white1x1Tex, c, z);

    public void Rect(float x, float y, float width, float height, Texture tex, Color tint, float z = 0)
        => Quad(new vec2(x, y), new vec2(x + width, y), new vec2(x + width, y + height), new vec2(x, y + height), tex, tint, z);

    public void Text(float x, float y, string text, FntFontData font, Color tint, float scaleX = 1, float scaleY = 1, float z = 0)
    {
      TrySetTexture(font.Texture);

      float xOffset = 0;
      float yOffset = 0;

      float scaleW = font.FntFont.Common.ScaleWidth;
      float scaleH = font.FntFont.Common.ScaleHeight;

      foreach (var ch in text.Replace(Environment.NewLine, "\n"))
      {
        if (ch == '\n')
        {
          xOffset = 0;
          yOffset += font.FntFont.Common.LineHeight;
          continue;
        }
        var fntChar = font.FntCharacters[ch];

        if (Char.IsWhiteSpace(ch))
        {
          xOffset += fntChar.XAdvance + 2;
          continue;
        }

        Index();
        Vertex(new VertexPosTexColor
        {
          PosX = x + (fntChar.XOffset + xOffset) * scaleX,
          PosY = y + (fntChar.YOffset + yOffset) * scaleY,
          PosZ = z,
          TexCoordX = fntChar.X / scaleW,
          TexCoordY = fntChar.Y / scaleH,
          ColorR = tint.R,
          ColorG = tint.G,
          ColorB = tint.B,
          ColorA = tint.A
        });

        Index();
        Vertex(new VertexPosTexColor
        {
          PosX = x + (fntChar.XOffset + xOffset + fntChar.Width) * scaleX,
          PosY = y + (fntChar.YOffset + yOffset) * scaleY,
          PosZ = z,
          TexCoordX = (fntChar.X + fntChar.Width) / scaleW,
          TexCoordY = fntChar.Y / scaleH,
          ColorR = tint.R,
          ColorG = tint.G,
          ColorB = tint.B,
          ColorA = tint.A
        });

        Index();
        Vertex(new VertexPosTexColor
        {
          PosX = x + (fntChar.XOffset + xOffset + fntChar.Width) * scaleX,
          PosY = y + (fntChar.YOffset + yOffset + fntChar.Height) * scaleY,
          PosZ = z,
          TexCoordX = (fntChar.X + fntChar.Width) / scaleW,
          TexCoordY = (fntChar.Y + fntChar.Height) / scaleH,
          ColorR = tint.R,
          ColorG = tint.G,
          ColorB = tint.B,
          ColorA = tint.A
        });

        // 2, 3, 0 
        indices[indexCount] = indices[indexCount - 1];
        indexCount++;

        indices[indexCount] = indices[indexCount - 1] + 1;
        indexCount++;

        indices[indexCount] = indices[indexCount - 5];
        indexCount++;

        Vertex(new VertexPosTexColor
        {
          PosX = x + (fntChar.XOffset + xOffset) * scaleX,
          PosY = y + (fntChar.YOffset + yOffset + fntChar.Height) * scaleY,
          PosZ = z,
          TexCoordX = fntChar.X / scaleW,
          TexCoordY = (fntChar.Y + fntChar.Height) / scaleH,
          ColorR = tint.R,
          ColorG = tint.G,
          ColorB = tint.B,
          ColorA = tint.A
        });

        xOffset += fntChar.XAdvance + 2;
      }
    }

    void TrySetTexture(Texture tex)
    {
      if (currTexture != tex)
      {
        Flush();
        currTexture = tex;
      }
    }


    public void Dispose()
    {
      gl.DeleteBuffers(1, vertexBufferIds);
      gl.DeleteBuffers(1, indexBufferIds);
      gl.DeleteVertexArrays(1, vaos);

      white1x1Tex.Destroy(gl);
    }
  }
}