namespace doix.desktop.forms.controls
{
  partial class DataGridControl
  {
    /// <summary>  Required designer variable. </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> Clean up any resources being used. </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.vScrollBar = new System.Windows.Forms.VScrollBar();
      this.hScrollBar = new System.Windows.Forms.HScrollBar();
      this.viewport = new doix.desktop.forms.controls.DataGridViewport();
      ((System.ComponentModel.ISupportInitialize)(this.viewport)).BeginInit();
      this.SuspendLayout();
      // 
      // vScrollBar
      // 
      this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.vScrollBar.Location = new System.Drawing.Point(842, 0);
      this.vScrollBar.Maximum = 10000;
      this.vScrollBar.Name = "vScrollBar";
      this.vScrollBar.Size = new System.Drawing.Size(17, 526);
      this.vScrollBar.TabIndex = 1;
      // 
      // hScrollBar
      // 
      this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.hScrollBar.Location = new System.Drawing.Point(0, 526);
      this.hScrollBar.Maximum = 10000;
      this.hScrollBar.Name = "hScrollBar";
      this.hScrollBar.Size = new System.Drawing.Size(842, 17);
      this.hScrollBar.TabIndex = 2;
      // 
      // viewport
      // 
      this.viewport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.viewport.DrawFPS = false;
      this.viewport.Location = new System.Drawing.Point(0, 0);
      this.viewport.Margin = new System.Windows.Forms.Padding(0);
      this.viewport.Name = "viewport";
      this.viewport.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
      this.viewport.RenderContextType = SharpGL.RenderContextType.FBO;
      this.viewport.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
      this.viewport.Size = new System.Drawing.Size(842, 526);
      this.viewport.TabIndex = 0;
      // 
      // DataGridControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ControlDark;
      this.Controls.Add(this.hScrollBar);
      this.Controls.Add(this.vScrollBar);
      this.Controls.Add(this.viewport);
      this.Name = "DataGridControl";
      this.Size = new System.Drawing.Size(859, 543);
      ((System.ComponentModel.ISupportInitialize)(this.viewport)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private DataGridViewport viewport;
    private System.Windows.Forms.VScrollBar vScrollBar;
    private System.Windows.Forms.HScrollBar hScrollBar;
  }
}
