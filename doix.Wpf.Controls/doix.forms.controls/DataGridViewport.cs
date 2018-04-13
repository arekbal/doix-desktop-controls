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
using doix.core.controls;
using GlmNet;
using System.Diagnostics;

namespace doix.forms.controls
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

    protected virtual void OnInitialize(object sender, EventArgs e)
    {
      Initializing?.Invoke();

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

        DrawColumns(borderThickness, ref iLoCol, ref iHiCol);

        DrawRows(borderThickness, ref iLoRow, ref iHiRow);

        for (var iCol = iLoCol; iCol < iHiCol; iCol++)
          for (var iRow = iLoRow; iRow < iHiRow; iRow++)
          {
            var offsetX = Columns[iCol]._OffsetX;
            var offsetY = Rows[iRow]._OffsetY;
            spriteBatch.Text(offsetX - (float)HOffset + 15, offsetY - (float)VOffset, names[(iCol + iRow) % names.Length], font, Color.DarkGray, .5f, .5f);
          }

        for (var iCol = iLoCol; iCol < iHiCol; iCol++)
          for (var iRow = iLoRow; iRow < iHiRow; iRow++)
          {
            var offsetX = Columns[iCol]._OffsetX;
            var offsetY = Rows[iRow]._OffsetY;
            spriteBatch.Rect(offsetX - (float)HOffset + 3, offsetY - (float)VOffset + 3, 9, 9, colors[(iCol + iRow) % colors.Length]);
            spriteBatch.Rect(offsetX - (float)HOffset + 2, offsetY - (float)VOffset + 2, 11, 11, Color.Black, -.1f);
          }

        //spriteBatch.Triangle(
        //    new vec2(-1.0f, -1.0f),
        //    new vec2(1.0f, -1.0f),
        //    new vec2(1.0f, 1.0f),
        //    Color.Chocolate, 1.0f);

        var n = .39f;

        spriteBatch.Quad(
           new vec2(-n, -n),
           new vec2(n, -n),
           new vec2(n, n),
           new vec2(-n, n),
           // _texture,
           Color.Red);

        //spriteBatch.Rect(
        // 100, 105,
        // 1000.0f, 1200.0f,                 
        // Color.Black);

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

        spriteBatch.Text((float)HOffset, (float)VOffset, "Hello World!!!", font, Color.DarkGray);
      }
    }

    private void DrawColumns(float borderThickness, ref int iLoCol, ref int iHiCol)
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

      spriteBatch.Rect(xLoOffset - (float)HOffset, 0, borderThickness, 2000, Color.LightGray);

      float xOffset = xLoOffset;

      var iCol = iLoCol;

      for (; iCol < iHiCol; iCol++)
      {
        var colWidth = Columns[iCol].Width + borderThickness;

        xOffset += colWidth;

        spriteBatch.Rect(xOffset - (float)HOffset, 0, borderThickness, 2000, Color.LightGray);
      }
    }

    private void DrawRows(float borderThickness, ref int iLoRow, ref int iHiRow)
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

      spriteBatch.Rect(0, yLoOffset - (float)VOffset, 5000, borderThickness, Color.DarkGray);

      float yOffset = yLoOffset;

      var iRow = iLoRow;

      for (; iRow < iHiRow; iRow++)
      {
        var rowHeight = Rows[iRow].Height + borderThickness;

        yOffset += rowHeight;

        spriteBatch.Rect(0, yOffset - (float)VOffset, 5000, borderThickness, Color.DarkGray);
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
