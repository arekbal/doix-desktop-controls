namespace doix.forms.controls
{
  partial class DataGridDemoForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.dg = new doix.forms.controls.DataGridControl();
      this.SuspendLayout();
      // 
      // dg
      // 
      this.dg.AnimationFrameRate = 60;
      this.dg.BackColor = System.Drawing.SystemColors.ControlDark;
      this.dg.Columns = new doix.forms.controls.ColumnInfo[0];
      this.dg.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dg.FntPath = null;
      this.dg.Location = new System.Drawing.Point(0, 0);
      this.dg.Margin = new System.Windows.Forms.Padding(0);
      this.dg.MouseWheelHorizontalScrollModifierKeys = doix.forms.controls.ModifierKeys.Ctrl;
      this.dg.Name = "dg";
      this.dg.Rows = new doix.forms.controls.RowInfo[0];
      this.dg.ScrollSmoothing = true;
      this.dg.Size = new System.Drawing.Size(1029, 766);
      this.dg.TabIndex = 4;
      // 
      // DataGridDemoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1029, 766);
      this.Controls.Add(this.dg);
      this.Name = "DataGridDemoForm";
      this.Text = "DataGridDemoForm";
      this.ResumeLayout(false);

    }

    #endregion
    private DataGridControl dg;
  }
}