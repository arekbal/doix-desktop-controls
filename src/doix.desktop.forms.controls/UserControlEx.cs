using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace doix.desktop.forms.controls
{
  public partial class UserControlEx : UserControl
  {
    List<IDisposable> behaviors = new List<IDisposable>();

    public UserControlEx()
    {
      InitializeComponent();
    }

    public TBehavior AddBehavior<TBehavior>(TBehavior behavior) where TBehavior : IDisposable
    {
      behaviors.Add(behavior);
      return behavior;
    }

    protected virtual void OnCustomDispose(bool disposing)
    {
      foreach (var disposable in behaviors)
        disposable.Dispose();
    }

    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      OnCustomDispose(disposing);

      if (disposing && (components != null))
      {
        components.Dispose();       
      }
      base.Dispose(disposing);
    }
  }
}
