using doix.desktop.forms.controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ColumnInfo = doix.desktop.forms.controls.ColumnInfo;
using RowInfo = doix.desktop.forms.controls.RowInfo;

namespace doix.desktop.wpf.controls.demo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      Image myImage = new Image();

      var typeface = new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Normal, new FontStretch());
      var text = new FormattedText("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz ",
              new CultureInfo("en-us"),
              FlowDirection.LeftToRight,
              typeface,
              this.FontSize,
              this.Foreground);

      var drawingVisual = new DrawingVisual();
      using (var drawingContext = drawingVisual.RenderOpen())
      {
        drawingContext.DrawText(text, new Point(2, 2));
      }
      
      if(typeface.TryGetGlyphTypeface(out GlyphTypeface glyphTypeface))
      {
        //glyphTypeface.
      }

      RenderTargetBitmap bmp = new RenderTargetBitmap(180, 180, 120, 96, PixelFormats.Pbgra32);
      bmp.Render(drawingVisual);
      myImage.Source = bmp;

      Dispatcher.Invoke(new Action(() =>
      {
        var form = new DataGridDemoForm();
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        var wih = new WindowInteropHelper(this);
        wih.Owner = wih.Handle;
        form.Show();
      }));

      //dg.Initializing += (s, e) =>
      //{
      //  dg.VerticalScroll.Visible = true;
      //  dg.VerticalScroll.Maximum = 100000;

      //  dg.HorizontalScroll.Visible = true;
      //  dg.HorizontalScroll.Maximum = 100000;
      //  //dg.HorizontalScroll.Value = 10000;

      //  dg.MouseMove += (s2, e2) =>
      //  {
          
      //  };

      //  dg.Scroll += (s1, e1) =>
      //  {
      //    if (e1.ScrollOrientation == System.Windows.Forms.ScrollOrientation.HorizontalScroll)
      //    {
      //      //dg.AutoScrollPosition = new System.Drawing.Point(e1.NewValue, dg.AutoScrollOffset.Y);

      //      if(e1.NewValue == 0)            
      //        dg.HorizontalScroll.Value = dg.HOffset;
      //      else
      //        dg.HOffset = e1.NewValue;
      //    }
      //    else
      //    {
      //      if (e1.NewValue == 0)
      //        dg.VerticalScroll.Value = dg.VOffset;
      //      else
      //        dg.VOffset = e1.NewValue;

      //      //dg.AutoScrollPosition = new System.Drawing.Point(dg.AutoScrollOffset.X, e1.NewValue);

      //    }
      //    //dg.HOffset = dg.HorizontalScroll.Value;
      //    //dg.VOffset = dg.VerticalScroll.Value;

      //    dg.Invoke(new Action(() =>
      //    {
      //      dg.HorizontalScroll.Value = dg.HOffset;
      //      dg.VerticalScroll.Value = dg.VOffset;
      //    }));
      //  };

        dg.FntPath = "Content/Arial.fnt";
        dg.Columns = new ColumnInfo[7 * 10000];
        for (var i = 0; i < 10000; i++)
        {
          dg.Columns[i * 7 + 0].Width = 64;
          dg.Columns[i * 7 + 1].Width = 55;
          dg.Columns[i * 7 + 2].Width = 123;
          dg.Columns[i * 7 + 3].Width = 40;
          dg.Columns[i * 7 + 4].Width = 48;
          dg.Columns[i * 7 + 5].Width = 63;
          dg.Columns[i * 7 + 6].Width = 92;
        }

        dg.Rows = new RowInfo[7 * 10000];
        for (var i = 0; i < 10000; i++)
        {
          dg.Rows[i * 7 + 0].Height = 17;
          dg.Rows[i * 7 + 1].Height = 15;
          dg.Rows[i * 7 + 2].Height = 19;
          dg.Rows[i * 7 + 3].Height = 21;
          dg.Rows[i * 7 + 4].Height = 20;
          dg.Rows[i * 7 + 5].Height = 22;
          dg.Rows[i * 7 + 6].Height = 24;
        }
      //};
    }

    private byte[] BitmapSourceToArray(BitmapSource bitmapSource)
    {
      // Stride = (width) x (bytes per pixel)
      int stride = (int)bitmapSource.PixelWidth * (bitmapSource.Format.BitsPerPixel + 7) / 8;
      byte[] pixels = new byte[(int)bitmapSource.PixelHeight * stride];

      bitmapSource.CopyPixels(pixels, stride, 0);

      return pixels;
    } 
  }
}
