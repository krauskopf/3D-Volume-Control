namespace _3DVolumeControl
{
  partial class FormMain
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
      this.btnVolumeUp = new System.Windows.Forms.Button();
      this.buttonVolumeDown = new System.Windows.Forms.Button();
      this.buttonMute = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnVolumeUp
      // 
      this.btnVolumeUp.Location = new System.Drawing.Point(27, 25);
      this.btnVolumeUp.Name = "btnVolumeUp";
      this.btnVolumeUp.Size = new System.Drawing.Size(79, 30);
      this.btnVolumeUp.TabIndex = 0;
      this.btnVolumeUp.Text = "Up";
      this.btnVolumeUp.UseVisualStyleBackColor = true;
      this.btnVolumeUp.Click += new System.EventHandler(this.btnVolumeUp_Click);
      // 
      // buttonVolumeDown
      // 
      this.buttonVolumeDown.Location = new System.Drawing.Point(26, 116);
      this.buttonVolumeDown.Name = "buttonVolumeDown";
      this.buttonVolumeDown.Size = new System.Drawing.Size(79, 31);
      this.buttonVolumeDown.TabIndex = 1;
      this.buttonVolumeDown.Text = "Down";
      this.buttonVolumeDown.UseVisualStyleBackColor = true;
      this.buttonVolumeDown.Click += new System.EventHandler(this.buttonVolumeDown_Click);
      // 
      // buttonMute
      // 
      this.buttonMute.Location = new System.Drawing.Point(27, 72);
      this.buttonMute.Name = "buttonMute";
      this.buttonMute.Size = new System.Drawing.Size(78, 26);
      this.buttonMute.TabIndex = 2;
      this.buttonMute.Text = "Mute";
      this.buttonMute.UseVisualStyleBackColor = true;
      this.buttonMute.Click += new System.EventHandler(this.buttonMute_Click);
      // 
      // FormMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(126, 200);
      this.Controls.Add(this.buttonMute);
      this.Controls.Add(this.buttonVolumeDown);
      this.Controls.Add(this.btnVolumeUp);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "FormMain";
      this.Text = "3DVolumeControl";
      this.Load += new System.EventHandler(this.FormMain_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnVolumeUp;
    private System.Windows.Forms.Button buttonVolumeDown;
    private System.Windows.Forms.Button buttonMute;
  }
}

