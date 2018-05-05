using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using doix.desktop.core.controls;
using doix.desktop.core.controls.Helpers;
using doix.desktop.core.controls.Packing;
using GlmNet;
using Mapper;
using SharpGL;

namespace doix.forms.controls.demo.Scenarios
{
  class SomeSquaresScenario : Base.SpriteBatchScenarioBase
  {
    class Sprite : ISprite
    {
      public int Width { get; set; }
      public int Height { get; set; }
      public int Area { get; set; }

      public List<IMappedImageInfo> MappedImages { get; } = new List<IMappedImageInfo>();

      public void AddMappedImage(IMappedImageInfo mappedImage)
      {
        MappedImages.Add(mappedImage);
      }
    }

    class ImageInfo : IImageInfo
    {
      public int Width { get; set; }
      public int Height { get; set; }
    }

    vec2[] rectSizes = null;

    Rect[] rects;

    Random rand = new Random();

    static readonly Color[] colors = new Color[] { Color.Bisque, Color.Azure, Color.AliceBlue, Color.Aqua, Color.Brown, Color.Chocolate };

    public override void Clear() { }

    int i = 0;

    MaxRectsBinPack maxRectsBinPack;

    protected override void OnInitialize()
    {
      base.OnInitialize();

      var scale = 100d;
      var rectCount = 500;
      
      rectSizes = Enumerable.Range(0, rectCount)
        .Select(p => new vec2((float)(rand.NextDouble() * scale), (float)(rand.NextDouble() * scale)))
        .Where(p => 0 != (int)p.x && 0 != (int)p.y)
        .ToArray();

      rects = new Rect[rectCount];

      var totalWidth = 1024;
      var extraWidth = 128;

      maxRectsBinPack = new MaxRectsBinPack(totalWidth, totalWidth, true);
      for (var i = 0; i < rectSizes.Length; i++)
      {
        var rect = rectSizes[i];
        rects[i] = maxRectsBinPack.Insert((int)rect.x, (int)rect.y, MaxRectsBinPack.FreeRectChoiceHeuristic.RectBestLongSideFit);
        while (rects[i].width == 0)
        {
          maxRectsBinPack.freeRectangles.Add(new Rect { x = totalWidth, y = 0, width = extraWidth, height = totalWidth + extraWidth });
          maxRectsBinPack.freeRectangles.Add(new Rect { x = 0, y = totalWidth, width = totalWidth, height = extraWidth });
          
          totalWidth += extraWidth;
          rects[i] = maxRectsBinPack.Insert((int)rect.x, (int)rect.y, MaxRectsBinPack.FreeRectChoiceHeuristic.RectBestLongSideFit);
        }
      }
    }

    protected override void OnOpenGLDraw(object sender, RenderEventArgs args)
    {
      base.OnOpenGLDraw(sender, args);

      GL.Clear(0, 0.6f, 1, 1);
      GL.OrthoProj(Control.Width, Control.Height);

      using (SpriteBatch.BeginScope())
      {
        foreach (var rect in rects /*maxRectsBinPack.usedRectangles*/)
        {
          i++;
          SpriteBatch.Rect(rect.x, rect.y, rect.width, rect.height, colors[i % colors.Length]);
        }
      }
    }
  }
}
