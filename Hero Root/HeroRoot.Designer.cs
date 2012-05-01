namespace Hero_Root
{
    partial class RootForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RootForm));
            this.btnRoot = new System.Windows.Forms.Button();
            this.txtRootOut = new System.Windows.Forms.TextBox();
            this.timerCursor = new System.Windows.Forms.Timer(this.components);
            this.lblFocus = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblPhoneConnect = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerPhone = new System.Windows.Forms.Timer(this.components);
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRoot
            // 
            this.btnRoot.Font = new System.Drawing.Font("Consolas", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoot.Location = new System.Drawing.Point(99, 303);
            this.btnRoot.Name = "btnRoot";
            this.btnRoot.Size = new System.Drawing.Size(150, 49);
            this.btnRoot.TabIndex = 0;
            this.btnRoot.TabStop = false;
            this.btnRoot.Text = "Root Me!";
            this.btnRoot.UseVisualStyleBackColor = true;
            this.btnRoot.Visible = false;
            this.btnRoot.Click += new System.EventHandler(this.btnRoot_Click);
            // 
            // txtRootOut
            // 
            this.txtRootOut.BackColor = System.Drawing.Color.Black;
            this.txtRootOut.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRootOut.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtRootOut.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRootOut.ForeColor = System.Drawing.Color.Lime;
            this.txtRootOut.Location = new System.Drawing.Point(68, 80);
            this.txtRootOut.Multiline = true;
            this.txtRootOut.Name = "txtRootOut";
            this.txtRootOut.ReadOnly = true;
            this.txtRootOut.Size = new System.Drawing.Size(212, 315);
            this.txtRootOut.TabIndex = 1;
            this.txtRootOut.TabStop = false;
            this.txtRootOut.Text = "CDMA Hero Root by regaw_leinad\r\n\r\n>_";
            this.txtRootOut.Enter += new System.EventHandler(this.txtRootOut_Enter);
            // 
            // timerCursor
            // 
            this.timerCursor.Interval = 500;
            this.timerCursor.Tick += new System.EventHandler(this.timerCursor_Tick);
            // 
            // lblFocus
            // 
            this.lblFocus.AutoSize = true;
            this.lblFocus.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblFocus.Location = new System.Drawing.Point(12, 9);
            this.lblFocus.Name = "lblFocus";
            this.lblFocus.Size = new System.Drawing.Size(0, 13);
            this.lblFocus.TabIndex = 2;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPhoneConnect});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(348, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
            // 
            // lblPhoneConnect
            // 
            this.lblPhoneConnect.BackColor = System.Drawing.SystemColors.Control;
            this.lblPhoneConnect.Name = "lblPhoneConnect";
            this.lblPhoneConnect.Size = new System.Drawing.Size(0, 17);
            // 
            // timerPhone
            // 
            this.timerPhone.Interval = 1000;
            this.timerPhone.Tick += new System.EventHandler(this.timerPhone_Tick);
            // 
            // RootForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = global::Hero_Root.Properties.Resources.PHONE;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(348, 561);
            this.Controls.Add(this.btnRoot);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lblFocus);
            this.Controls.Add(this.txtRootOut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RootForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sprint CDMA Hero Rooter - regaw_leinad";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RootForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RootForm_FormClosed);
            this.Load += new System.EventHandler(this.RootForm_Load);
            this.Shown += new System.EventHandler(this.RootForm_Shown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRoot;
        private System.Windows.Forms.TextBox txtRootOut;
        private System.Windows.Forms.Timer timerCursor;
        private System.Windows.Forms.Label lblFocus;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblPhoneConnect;
        private System.Windows.Forms.Timer timerPhone;

    }
}

