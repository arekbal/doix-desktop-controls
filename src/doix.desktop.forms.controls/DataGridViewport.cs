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
using doix.desktop.core.controls;
using GlmNet;
using System.Diagnostics;

namespace doix.desktop.forms.controls
{
  public partial class DataGridViewport : GLDesignerControl
  {
    const double ScrollSmoothAnimationSpeedInMiliseconds = 230d;

    FntFontData font;

    SpriteBatch spriteBatch;

    public RowInfo[] Rows = Array.Empty<RowInfo>();

    public ColumnInfo[] Columns = Array.Empty<ColumnInfo>();

    public double TotalWidth;
    public double TotalHeight;

    public string FntPath;

    public double VOffset;

    public double HOffset;

    public Color TextColor = Color.Black;

    public Color BorderColor = Color.Black;

    Animation.BetweenValues VOffsetAnimation;

    Animation.BetweenValues HOffsetAnimation;

    public void AnimateVOffset(int newValue)
    {
      _isAnimating = true;
      VOffsetAnimation.Time.StartTime = watch.ElapsedMilliseconds;
      VOffsetAnimation.Time.EndTime = VOffsetAnimation.Time.StartTime + ScrollSmoothAnimationSpeedInMiliseconds;
      VOffsetAnimation.OldValue = VOffset;
      VOffsetAnimation.NewValue = newValue;
    }

    public void AnimateHOffset(int newValue)
    {
      _isAnimating = true;
      HOffsetAnimation.Time.StartTime = watch.ElapsedMilliseconds;
      HOffsetAnimation.Time.EndTime = HOffsetAnimation.Time.StartTime + ScrollSmoothAnimationSpeedInMiliseconds;
      HOffsetAnimation.OldValue = HOffset;
      HOffsetAnimation.NewValue = newValue;
    }

    bool _redrawRequired = true;
    public bool RedrawRequired => _redrawRequired;

    public void RequireRedraw()
      => _redrawRequired = true;

    bool _isAnimating;
    public bool IsAnimating => _isAnimating;

    string[] names = new string[] { "PO", "GM", "WZ", "SM", "MG", "WP", "GG", "GL" };

    Color[] colors = new Color[] { Color.Bisque, Color.Azure, Color.AliceBlue, Color.Aqua, Color.Brown, Color.Chocolate };

    public DataGridViewport()
    {
      InitializeComponent();

      if (!IsDesignTime)
      {
        OpenGLInitialized += this.OnGLInitialized;
        OpenGLDraw += this.OnGLDraw;
      }
    }

    public event Action Initializing;
    public event Action Initialized;
    bool isInitialized;

    Stopwatch watch = new Stopwatch();
    Random rand = new Random();
    vec2[] rectSizes = null;

    protected virtual void OnInitialize(object sender, EventArgs e)
    {
      Initializing?.Invoke();

      var scale = 100d;

      rectSizes = Enumerable.Range(0, 2000).Select(p => new vec2((float)(rand.NextDouble() * scale), (float)(rand.NextDouble() * scale))).ToArray();

      var total = 0.0f;
      foreach (var col in Columns)
        total += col.Width;

      TotalWidth = total;

      total = 0;
      foreach (var row in Rows)
        total += row.Height;

      TotalHeight = total;

      font = new FntFontData(OpenGL, FntPath);

      spriteBatch = new SpriteBatch(OpenGL, 100000);

      Initialized?.Invoke();
    }

    private void OnGLInitialized(object sender, EventArgs e)
    {
      OpenGL.ClearColor(0, 0.6f, 1, 1);
    }

    private void OnGLDraw(object sender, RenderEventArgs args)
    {
      if (!isInitialized)
      {
        OnInitialize(sender, EventArgs.Empty);
        watch.Start();

        isInitialized = true;
      }
      else
      {
        //frameTime = watch.Elapsed.TotalMilliseconds;
      }

      if (_redrawRequired)
      {
        OnRedraw(OpenGL);
      }

      OnUpdate();

      if (!IsAnimating)
        _redrawRequired = false;
    }

    protected virtual void OnUpdate()
    {
      var ms = watch.ElapsedMilliseconds;
      VOffset = VOffsetAnimation.GetCubic(ms);
      HOffset = HOffsetAnimation.GetCubic(ms);
      if (VOffsetAnimation.Time.HasFinished(ms) && HOffsetAnimation.Time.HasFinished(ms))
        _isAnimating = false;
    }

    protected virtual void OnRedraw(OpenGL gl)
    {
      gl.ClearColor(1, 1, 1, 1);
      gl.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

      gl.MatrixMode(GL_PROJECTION);
      gl.LoadIdentity();

      gl.Ortho(0, Width, Height, 0, -1, 1);

      gl.MatrixMode(GL_MODELVIEW);
      gl.LoadIdentity();

      using (spriteBatch.BeginScope())
      {
        float borderThickness = 1;

        var iLoCol = 0;
        var iHiCol = 0;

        var iLoRow = 0;
        var iHiRow = 0;

        //DrawColumns(borderThickness, ref iLoCol, ref iHiCol);

        //DrawRows(borderThickness, ref iLoRow, ref iHiRow);

        //for (var iCol = iLoCol; iCol < iHiCol; iCol++)
        //  for (var iRow = iLoRow; iRow < iHiRow; iRow++)
        //  {
        //    var offsetX = Columns[iCol]._OffsetX;
        //    var offsetY = Rows[iRow]._OffsetY;
        //    spriteBatch.Text((float)Math.Round(offsetX - (float)HOffset + 15), (float)Math.Round(offsetY - (float)VOffset), names[(iCol + iRow) % names.Length], font, TextColor, .5f, .5f);
        //  }

        //for (var iCol = iLoCol; iCol < iHiCol; iCol++)
        //  for (var iRow = iLoRow; iRow < iHiRow; iRow++)
        //  {
        //    var offsetX = Columns[iCol]._OffsetX;
        //    var offsetY = Rows[iRow]._OffsetY;
        //    spriteBatch.Rect(offsetX - (float)HOffset + 3, offsetY - (float)VOffset + 3, 9, 9, colors[(iCol + iRow) % colors.Length]);
        //    spriteBatch.Rect(offsetX - (float)HOffset + 2, offsetY - (float)VOffset + 2, 11, 11, BorderColor, -.1f);
        //  }

        //spriteBatch.Triangle(
        //    new vec2(-1.0f, -1.0f),
        //    new vec2(1.0f, -1.0f),
        //    new vec2(1.0f, 1.0f),
        //    Color.Chocolate, 1.0f);

        var n = .39f;


        //var rects = new[] { new vec2(32, 146), new vec2(71, 24), new vec2(55, 132), new vec2(77, 38), new vec2(43, 79) };

        // reverse, longer into y
        //for (var i = 0; i < rects.Length; ++i)
        //{
        //  ref var rect = ref rects[i];

        //  if (rect.x > rect.y)
        //  {
        //    var oldX = rect.x;
        //    rect.x = rect.y;
        //    rect.y = rect.x;
        //  }
        //}

        var maxHeight = rectSizes.Max(z => z.y);

        var sumWidth = rectSizes.Sum(z => z.x);

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

        for (var i = 0; i < rects.Count; ++i)
        {
          var rect = rects[i];
          spriteBatch.Rect(rect.x, rect.y, rect.z, rect.w, colors[i % colors.Length]);
        }

        //for(var i = 0; i < rectSizes.Length; ++i)
        //{
        //  var rectSize = rectSizes[i];

        //  spriteBatch.Rect(
        //     x, 0f, rectSize.x, rectSize.y, colors[i % colors.Length]);

        //  x += rectSize.x;
        //}

        //spriteBatch.Quad(
        //   new vec2(-n, -n),
        //   new vec2(n, -n),
        //   new vec2(n, n),
        //   new vec2(-n, n),
        //   // _texture,
        //   Color.Red);

        //spriteBatch.Rect(
        // 100, 105,
        // 1000.0f, 1200.0f,
        //Color.Black);

        //spriteBatch.Rect(
        //  100, 105,
        //  1300.0f, 1200.0f,
        //  _texture,
        //  Color.Gainsboro);

        //for (var x = 0f; x < 1000f; x += 10)
        //    for (var y = 0f; y < 1000f; y += 10)
        //    {
        //        spriteBatch.Rect(
        //         x + _rotation % 100, y,
        //         9, 9,
        //         Color.PaleVioletRed);
        //    }

        //spriteBatch.Text((float)HOffset, (float)VOffset, "Hello World!!!", font, TextColor, 1f, 1f);
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

          if (HitTests.HitTest(ref currRect, ref otherRect) != HitTestResult.Apart)
            throw new Exception("collision");
        }
      }
    }

    void DrawColumns(float borderThickness, ref int iLoCol, ref int iHiCol)
    {
      float xLoOffset = 0;
      float xHiOffset = 0;

      for (; iLoCol < Columns.Length; iLoCol++)
      {
        Columns[iLoCol]._OffsetX = xLoOffset;
        var colWidth = Columns[iLoCol].Width + borderThickness;

        if (HOffset < (colWidth + xLoOffset))
          break;

        xLoOffset += colWidth;
      }

      iHiCol = iLoCol;
      xHiOffset = xLoOffset;

      for (; iHiCol < Columns.Length; iHiCol++)
      {
        Columns[iHiCol]._OffsetX = xHiOffset;
        var colWidth = Columns[iHiCol].Width + borderThickness;

        xHiOffset += colWidth;

        if (Width < (xHiOffset - xLoOffset))
          break;
      }

      spriteBatch.Rect(xLoOffset - (float)HOffset, 0, borderThickness, 2000, BorderColor);

      float xOffset = xLoOffset;

      var iCol = iLoCol;

      for (; iCol < iHiCol; iCol++)
      {
        var colWidth = Columns[iCol].Width + borderThickness;

        xOffset += colWidth;

        spriteBatch.Rect(xOffset - (float)HOffset, 0, borderThickness, 2000, BorderColor);
      }
    }

    void DrawRows(float borderThickness, ref int iLoRow, ref int iHiRow)
    {
      float yLoOffset = 0;
      float yHiOffset = 0;

      for (; iLoRow < Rows.Length; iLoRow++)
      {
        Rows[iLoRow]._OffsetY = yLoOffset;

        var rowHeight = Rows[iLoRow].Height + borderThickness;

        if (VOffset < (rowHeight + yLoOffset))
          break;

        yLoOffset += rowHeight;
      }

      iHiRow = iLoRow;
      yHiOffset = yLoOffset;

      for (; iHiRow < Rows.Length; iHiRow++)
      {
        Rows[iHiRow]._OffsetY = yHiOffset;

        var rowHeight = Rows[iHiRow].Height + borderThickness;

        yHiOffset += rowHeight;

        if (Height < (yHiOffset - yLoOffset))
          break;
      }

      spriteBatch.Rect(0, yLoOffset - (float)VOffset, 5000, borderThickness, BorderColor);

      float yOffset = yLoOffset;

      var iRow = iLoRow;

      for (; iRow < iHiRow; iRow++)
      {
        var rowHeight = Rows[iRow].Height + borderThickness;

        yOffset += rowHeight;

        spriteBatch.Rect(0, yOffset - (float)VOffset, 5000, borderThickness, BorderColor);
      }
    }

    public void SetColumnWidth(int iColumn, float width)
    {
      var diff = Columns[iColumn].Width - width;

      TotalWidth -= diff;
    }

    public void SetRowHeight(int iRow, float height)
    {
      var diff = Rows[iRow].Height - height;

      TotalHeight -= diff;
    }
  }
}
