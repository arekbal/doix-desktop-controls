using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using doix.forms.controls.Behaviors;

namespace doix.forms.controls
{
  public enum ModifierKeys
  {
    Shift = Keys.Shift,
    Ctrl = Keys.Control,
    Alt = Keys.Alt,
  }

  public partial class DataGridControl : UserControlEx
  {
    public event Action Initializing
    {
      add { viewport.Initializing += value; }
      remove { viewport.Initializing -= value; }
    }

    public ColumnInfo[] Columns
    {
      get => viewport.Columns;
      set => viewport.Columns = value;
    }

    public RowInfo[] Rows
    {
      get => viewport.Rows;
      set => viewport.Rows = value;
    }

    public int AnimationFrameRate
    {
      get => viewport.FrameRate;
      set => viewport.FrameRate = value;
    }

    public string FntPath
    {
      get => viewport.FntPath;
      set => viewport.FntPath = value;
    }

    public ModifierKeys MouseWheelHorizontalScrollModifierKeys { get; set; } = controls.ModifierKeys.Shift;

    public DataGridControl()
    {
      InitializeComponent();

      AddBehavior(new MouseWheelOnMouseOverBehavior(this));

      viewport.Initialized += () =>
      {
        hScrollBar.Maximum = (int)Math.Ceiling(viewport.TotalWidth);
        vScrollBar.Maximum = (int)Math.Ceiling(viewport.TotalHeight);
      };

      viewport.SizeChanged += (s, e) =>
      {
        viewport.RequireRedraw();
      };

      vScrollBar.ValueChanged += (s, e) =>
      {
        if (ScrollSmoothing)
          viewport.AnimateVOffset(vScrollBar.Value);
        else
          viewport.VOffset = vScrollBar.Value;

        viewport.RequireRedraw();
      };

      hScrollBar.ValueChanged += (s, e) =>
      {
        if (ScrollSmoothing)
          viewport.AnimateHOffset(hScrollBar.Value);
        else
          viewport.HOffset = hScrollBar.Value;

        viewport.RequireRedraw();
      };
    }

    public bool ScrollSmoothing { get; set; } = true;

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      ScrollBar scrollBar;
      if (!ModifierKeys.HasFlag((Keys)MouseWheelHorizontalScrollModifierKeys))// & Keys.Shift) != Keys.Shift)
        scrollBar = vScrollBar;
      else
        scrollBar = hScrollBar;

      scrollBar.Value = Math.Min(scrollBar.Maximum, Math.Max(scrollBar.Minimum, scrollBar.Value - e.Delta));
    }
  } 
}
