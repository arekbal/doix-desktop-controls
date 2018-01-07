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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }
    }
}
