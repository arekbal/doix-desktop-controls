namespace doix.desktop.forms.controls
{
  partial class GLViewportForm
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
      this.glControl1 = new doix.desktop.forms.controls.GLControl();
      ((System.ComponentModel.ISupportInitialize)(this.glControl1)).BeginInit();
      this.SuspendLayout();
      // 
      // glControl1
      // 
      this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.glControl1.DrawFPS = true;
      this.glControl1.Location = new System.Drawing.Point(0, 0);
      this.glControl1.Name = "glControl1";
      this.glControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
      this.glControl1.RenderContextType = SharpGL.RenderContextType.FBO;
      this.glControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
      this.glControl1.Size = new System.Drawing.Size(946, 638);
      this.glControl1.TabIndex = 0;
      // 
      // GLViewportForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(946, 638);
      this.Controls.Add(this.glControl1);
      this.Name = "GLViewportForm";
      this.Text = "GLViewportForm";
      ((System.ComponentModel.ISupportInitialize)(this.glControl1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private GLControl glControl1;
  }
}