using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doix.forms.controls
{
  public partial class DataGridDemoForm : Form
  {
    public DataGridDemoForm()
    {
      InitializeComponent();
      
      dg.Initializing += () =>
      {
        dg.FntPath = "Content/Arial.fnt";

        var columns = new ColumnInfo[7 * 10000];
        for (var i = 0; i < 10000; i++)
        {
          columns[i * 7 + 0].Width = 64;
          columns[i * 7 + 1].Width = 55;
          columns[i * 7 + 2].Width = 123;
          columns[i * 7 + 3].Width = 40;
          columns[i * 7 + 4].Width = 48;
          columns[i * 7 + 5].Width = 63;
          columns[i * 7 + 6].Width = 92;
        };

        dg.Columns = columns;

        var rows = new RowInfo[7 * 10000];
        for (var i = 0; i < 10000; i++)
        {
          rows[i * 7 + 0].Height = 17;
          rows[i * 7 + 1].Height = 15;
          rows[i * 7 + 2].Height = 19;
          rows[i * 7 + 3].Height = 21;
          rows[i * 7 + 4].Height = 20;
          rows[i * 7 + 5].Height = 22;
          rows[i * 7 + 6].Height = 24;
        }

        dg.Rows = rows;
      };
    }    
  }
}
