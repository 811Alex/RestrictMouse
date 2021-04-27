namespace RestrictMouse
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.ProcessList = new System.Windows.Forms.ComboBox();
            this.labelTarget = new System.Windows.Forms.Label();
            this.RadioWindow = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.RadioProcess = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(308, 39);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(85, 21);
            this.RefreshBtn.TabIndex = 0;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // ProcessList
            // 
            this.ProcessList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ProcessList.FormattingEnabled = true;
            this.ProcessList.Location = new System.Drawing.Point(80, 12);
            this.ProcessList.Name = "ProcessList";
            this.ProcessList.Size = new System.Drawing.Size(313, 21);
            this.ProcessList.TabIndex = 1;
            // 
            // labelTarget
            // 
            this.labelTarget.AutoSize = true;
            this.labelTarget.Location = new System.Drawing.Point(12, 15);
            this.labelTarget.Name = "labelTarget";
            this.labelTarget.Size = new System.Drawing.Size(62, 13);
            this.labelTarget.TabIndex = 2;
            this.labelTarget.Text = "Target app:";
            // 
            // RadioWindow
            // 
            this.RadioWindow.AutoSize = true;
            this.RadioWindow.Checked = true;
            this.RadioWindow.Location = new System.Drawing.Point(111, 40);
            this.RadioWindow.Name = "RadioWindow";
            this.RadioWindow.Size = new System.Drawing.Size(93, 17);
            this.RadioWindow.TabIndex = 3;
            this.RadioWindow.TabStop = true;
            this.RadioWindow.Text = "Window name";
            this.RadioWindow.UseVisualStyleBackColor = true;
            this.RadioWindow.CheckedChanged += new System.EventHandler(this.RadioWindow_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Targeting method:";
            // 
            // RadioProcess
            // 
            this.RadioProcess.AutoSize = true;
            this.RadioProcess.Location = new System.Drawing.Point(210, 40);
            this.RadioProcess.Name = "RadioProcess";
            this.RadioProcess.Size = new System.Drawing.Size(92, 17);
            this.RadioProcess.TabIndex = 5;
            this.RadioProcess.Text = "Process name";
            this.RadioProcess.UseVisualStyleBackColor = true;
            this.RadioProcess.CheckedChanged += new System.EventHandler(this.RadioProcess_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 69);
            this.Controls.Add(this.RadioProcess);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RadioWindow);
            this.Controls.Add(this.labelTarget);
            this.Controls.Add(this.ProcessList);
            this.Controls.Add(this.RefreshBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "RestrictMouse";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.ComboBox ProcessList;
        private System.Windows.Forms.Label labelTarget;
        private System.Windows.Forms.RadioButton RadioWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton RadioProcess;
    }
}

