using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace doix.core.controls
{
  [StructLayout(LayoutKind.Sequential, Pack = 0)]
  public struct VertexPosTexColor
  {
    public static readonly int SizeOf = Marshal.SizeOf<VertexPosTexColor>();

    public float PosX;
    public float PosY;
    public float PosZ;

    public byte ColorR;
    public byte ColorG;
    public byte ColorB;
    public byte ColorA;

    public float TexCoordX;
    public float TexCoordY;

    public GLColor Color
    {
      get => new GLColor(ColorR / 255.0f, ColorG / 255.0f, ColorB / 255.0f, ColorA / 255.0f);
      set
      {
        ColorR = (byte)Math.Round(value.R * 255.0f);
        ColorG = (byte)Math.Round(value.G * 255.0f);
        ColorB = (byte)Math.Round(value.B * 255.0f);
        ColorA = (byte)Math.Round(value.A * 255.0f);
      }
    }
  }
}
