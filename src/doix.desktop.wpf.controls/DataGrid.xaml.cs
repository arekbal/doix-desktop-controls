using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Assets;
using SharpGL.VertexBuffers;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using SharpGL.Shaders;
using SharpGL.SceneGraph.Shaders;
using SharpGL.SceneGraph.Primitives;
using GlmNet;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;

using static SharpGL.OpenGL;

namespace doix.desktop.wpf.controls
{
  delegate void b<T>(ref T x);

  public partial class DataGrid : UserControl
  {
    FntFontData font;

    SpriteBatch spriteBatch;

    public RowInfo[] Rows = null;

    public ColumnInfo[] Columns = null;

    public DataGrid()
    {
      InitializeComponent();
      TotalWidth = 4000;
      TotalHeight = 4000;
    }

    public string FntPath
    {
      get => (string)GetValue(FntPathProperty);
      set => SetValue(FntPathProperty, value);
    }
    public static readonly DependencyProperty FntPathProperty =
        DependencyProperty.Register("FntPath", typeof(string), typeof(DataGrid), new PropertyMetadata(""));

    public double TotalWidth
    {
      get => (double)GetValue(TotalWidthProperty);
      set => SetValue(TotalWidthProperty, value);
    }
    public static readonly DependencyProperty TotalWidthProperty =
        DependencyProperty.Register("TotalWidth", typeof(double), typeof(DataGrid), new PropertyMetadata(0.0));

    public double TotalHeight
    {
      get => (double)GetValue(TotalHeightProperty);
      set => SetValue(TotalHeightProperty, value);
    }
    public static readonly DependencyProperty TotalHeightProperty =
        DependencyProperty.Register("TotalHeight", typeof(double), typeof(DataGrid), new PropertyMetadata(0.0));

    void GLInitialized(object sender, OpenGLEventArgs args)
    {
      var gl = args.OpenGL;

      gl.ClearColor(1, 0.6f, 1, 1);

      if (DesignerProperties.GetIsInDesignMode(this))
        return;

      var total = 0.0f;
      foreach (var col in Columns)
        total += col.Width;

      TotalWidth = total;

      total = 0;
      foreach (var row in Rows)
        total += row.Height;

      TotalHeight = total;

      font = new FntFontData(gl, FntPath);

      spriteBatch = new SpriteBatch(gl, 100000);
    }

    string[] names = new string[] { "PO", "GM", "WZ", "SM", "MG", "WP", "GG", "GL" };

    Color[] colors = new Color[] { Color.Bisque, Color.Azure, Color.AliceBlue, Color.Aqua, Color.Brown, Color.Chocolate };

    private void GLDraw(object sender, OpenGLEventArgs args)
    {
      var gl = args.OpenGL;

      gl.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

      if (DesignerProperties.GetIsInDesignMode(this))
        return;     

      gl.MatrixMode(GL_PROJECTION);
      gl.LoadIdentity();
      gl.Ortho(0, glControl.ActualWidth, glControl.ActualHeight, 0, -1, 1);

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
            spriteBatch.Text(offsetX - (float)sbHorizontal.Value + 15, offsetY - (float)sbVertical.Value, names[(iCol + iRow) % names.Length], font, Color.Black, .5f, .5f);
          }

        for (var iCol = iLoCol; iCol < iHiCol; iCol++)
          for (var iRow = iLoRow; iRow < iHiRow; iRow++)
          {
            var offsetX = Columns[iCol]._OffsetX;
            var offsetY = Rows[iRow]._OffsetY;
            spriteBatch.Rect(offsetX - (float)sbHorizontal.Value + 3, offsetY - (float)sbVertical.Value + 3, 9, 9, colors[(iCol + iRow) % colors.Length]);
            spriteBatch.Rect(offsetX - (float)sbHorizontal.Value + 2, offsetY - (float)sbVertical.Value + 2, 11, 11, Color.Black, -.1f);
          }

        //spriteBatch.Triangle(
        //    new vec2(-1.0f, -1.0f),
        //    new vec2(1.0f, -1.0f),
        //    new vec2(1.0f, 1.0f),
        //    Color.Chocolate, 1.0f);

        //spriteBatch.Quad(
        //   new vec2(-4.0f, -4.0f),
        //   new vec2(4.0f, -4.0f),
        //   new vec2(4.0f, 4.0f),
        //   new vec2(-4.0f, 4.0f),
        //  // _texture,
        //   Color.PeachPuff);

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

        spriteBatch.Text((float)sbHorizontal.Value, (float)sbVertical.Value, "Hello World!!!", font, Color.Black);
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

        if (sbHorizontal.Value < (colWidth + xLoOffset))
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

        if (glControl.ActualWidth < (xHiOffset - xLoOffset))
          break;
      }

      spriteBatch.Rect(xLoOffset - (float)sbHorizontal.Value, 0, borderThickness, 2000, Color.DarkGray);

      float xOffset = xLoOffset;

      var iCol = iLoCol;

      for (; iCol < iHiCol; iCol++)
      {
        var colWidth = Columns[iCol].Width + borderThickness;

        xOffset += colWidth;

        spriteBatch.Rect(xOffset - (float)sbHorizontal.Value, 0, borderThickness, 2000, Color.DarkGray);
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

        if (sbVertical.Value < (rowHeight + yLoOffset))
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

        if (glControl.ActualHeight < (yHiOffset - yLoOffset))
          break;
      }

      spriteBatch.Rect(0, yLoOffset - (float)sbVertical.Value, 5000, borderThickness, Color.DarkGray);

      float yOffset = yLoOffset;

      var iRow = iLoRow;

      for (; iRow < iHiRow; iRow++)
      {
        var rowHeight = Rows[iRow].Height + borderThickness;

        yOffset += rowHeight;

        spriteBatch.Rect(0, yOffset - (float)sbVertical.Value, 5000, borderThickness, Color.DarkGray);
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
