using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using doix.desktop.core.controls;
using doix.desktop.forms.controls;
using doix.desktop.core.controls.Helpers;

using static SharpGL.OpenGL;
using SharpGL;
using GlmNet;

namespace doix.forms.controls.demo.Scenarios
{
  class SoopaScenario : Base.SpriteBatchScenarioBase
  {
    vec2[] rectSizes = null;

    Random rand = new Random();

    static readonly Color[] colors = new Color[] { Color.Bisque, Color.Azure, Color.AliceBlue, Color.Aqua, Color.Brown, Color.Chocolate };

    protected override void OnInitialize()
    {
      base.OnInitialize();

      var scale = 200d;
      var rectCount = 10;

      rectSizes = Enumerable.Range(0, rectCount).Select(p => new vec2((float)(rand.NextDouble() * scale), (float)(rand.NextDouble() * scale))).ToArray();
    }

    protected override void OnOpenGLDraw(object sender, RenderEventArgs args)
    {
      GL.Clear(0, 0.6f, 1, 1);
      GL.OrthoProj(Control.Width, Control.Height);

      Array.Sort(rectSizes, new Comparison<vec2>((a, b) => (int)(b.x * b.y - a.x * a.y)));

      var spaces = new List<vec4>();

      spaces.Add(new vec4(rectSizes[0].x, 0, 9999f, 9999f));
      spaces.Add(new vec4(0, rectSizes[0].y, 9999f, 9999f));

      var rects = new List<vec4> { new vec4(0, 0, rectSizes[0].x, rectSizes[0].y) };

      var width = Math.Max(rectSizes[0].x, rectSizes[0].y);

      for (var i = 1; i < rectSizes.Length; ++i)
      { // go through all rectangles and place them somewhere
        var rectSize = rectSizes[i];

        var spaceFoundIndex = -1;
        vec4 foundSpace = new vec4(0, 0, 9999f, 9999f);
        for (var iSpace = 0; iSpace < spaces.Count; ++iSpace)
        {
          var space = spaces[iSpace];
          if (space.z >= rectSize.x && space.w >= rectSize.y)
          { // would fit
            if (spaceFoundIndex == -1)
            {
              spaceFoundIndex = iSpace;
              foundSpace = space;
            }
            else
            {
              if (space.x + rectSize.x < width && space.y + rectSize.y < width)
              {
                //if (space.x + rectSize.x + space.y + rectSize.y < foundSpace.x + rectSize.x + foundSpace.y + rectSize.y)
                //{
                spaceFoundIndex = iSpace;
                foundSpace = space;
                //}
              }
            }
          }
        }

        if (spaceFoundIndex == -1)
          throw new Exception("not found");

        var newSpace0 = new vec4(foundSpace.x + rectSize.x, foundSpace.y, 9999f, 9999f);
        spaces[spaceFoundIndex] = newSpace0;
        spaces.Add(new vec4(foundSpace.x, foundSpace.y + rectSize.y, 9999f, 9999f));

        var newRect = new vec4(foundSpace.x, foundSpace.y, rectSize.x, rectSize.y);

        rects.Add(newRect);

        width = Math.Max(width, Math.Max(newRect.x + newRect.z, newRect.y + newRect.w));

        for (var iSpace = 0; iSpace < spaces.Count; ++iSpace)
        {
          var space = spaces[iSpace];

          var hitTestX = HitTests.HitTest(newRect.x, newRect.x + newRect.z, space.x, space.x + space.z);
          var hitTestY = HitTests.HitTest(newRect.y, newRect.y + newRect.w, space.y, space.y + space.w);

          if (hitTestX != HitTestResult.Apart && hitTestY != HitTestResult.Apart)
          {
            if (hitTestX == HitTestResult.B_Contains_A)
            {
              space.z = newRect.x - space.x;
              continue;
            }

            if (hitTestY == HitTestResult.B_Contains_A)
            {
              space.w = newRect.y - space.y;
              continue;
            }

            if (hitTestX == HitTestResult.A_Intersects_B)
            {
              space.z = space.z - (newRect.x + newRect.z) - space.x;
              space.x = newRect.x + newRect.z;
              continue;
            }

            if (hitTestY == HitTestResult.A_Intersects_B)
            {
              space.w = space.w - (newRect.y + newRect.w) - space.y;
              space.y = newRect.y + newRect.w;
              continue;
            }
          }
        }
      }

      Validate(rects);

      using (SpriteBatch.BeginScope())
      {
        for (var i = 0; i < rects.Count; ++i)
        {
          var rect = rects[i];
          SpriteBatch.Rect(rect.x, rect.y, rect.z, rect.w, colors[i % colors.Length]);
        }
      }
    }
    private static void Validate(List<vec4> rects)
    {
      for (var iCurrentRect = 0; iCurrentRect < rects.Count; ++iCurrentRect)
      {
        for (var iOtherRect = 0; iOtherRect < rects.Count; ++iOtherRect)
        {
          if (iCurrentRect == iOtherRect)
            continue;

          var currRect = rects[iCurrentRect];
          var otherRect = rects[iOtherRect];

          //if (HitTests.HitTest(currRect, otherRect) != HitTestResult.Apart)
          //  throw new Exception("collision");
        }
      }
    }
  }
}
