namespace doix.desktop.forms.controls.demo
{
  partial class TestForm
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
      this.cbScenarios = new System.Windows.Forms.ComboBox();
      this.viewport = new System.Windows.Forms.Panel();
      this.SuspendLayout();
      // 
      // cbScenarios
      // 
      this.cbScenarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbScenarios.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbScenarios.FormattingEnabled = true;
      this.cbScenarios.Location = new System.Drawing.Point(13, 13);
      this.cbScenarios.Name = "cbScenarios";
      this.cbScenarios.Size = new System.Drawing.Size(921, 21);
      this.cbScenarios.TabIndex = 0;
      // 
      // viewport
      // 
      this.viewport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.viewport.BackColor = System.Drawing.SystemColors.Window;
      this.viewport.Location = new System.Drawing.Point(13, 41);
      this.viewport.Name = "viewport";
      this.viewport.Size = new System.Drawing.Size(921, 538);
      this.viewport.TabIndex = 1;
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(946, 591);
      this.Controls.Add(this.viewport);
      this.Controls.Add(this.cbScenarios);
      this.Name = "TestForm";
      this.ShowIcon = false;
      this.Text = "Scenarios";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox cbScenarios;
    private System.Windows.Forms.Panel viewport;
  }
}

