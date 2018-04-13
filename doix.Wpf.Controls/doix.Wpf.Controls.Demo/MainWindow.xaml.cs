using doix.forms.controls;
using System;
using System.Collections.Generic;
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

using ColumnInfo = doix.forms.controls.ColumnInfo;
using RowInfo = doix.forms.controls.RowInfo;

namespace doix.Wpf.Controls.Demo
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

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
  }
}
