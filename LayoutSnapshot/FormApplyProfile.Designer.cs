
namespace LayoutSnapshot
{
    partial class FormApplyProfile
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.checkBoxLaunchIfNotOpen = new System.Windows.Forms.CheckBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonEditProfiles = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timerCheckWindowsOpened = new System.Windows.Forms.Timer(this.components);
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxProfiles, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBoxLaunchIfNotOpen, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonApply, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonEditProfiles, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.progressBar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.textBoxLog, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(285, 332);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(3, 3);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(279, 21);
            this.comboBoxProfiles.TabIndex = 0;
            // 
            // checkBoxLaunchIfNotOpen
            // 
            this.checkBoxLaunchIfNotOpen.AutoSize = true;
            this.checkBoxLaunchIfNotOpen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxLaunchIfNotOpen.Location = new System.Drawing.Point(3, 38);
            this.checkBoxLaunchIfNotOpen.Name = "checkBoxLaunchIfNotOpen";
            this.checkBoxLaunchIfNotOpen.Size = new System.Drawing.Size(279, 29);
            this.checkBoxLaunchIfNotOpen.TabIndex = 1;
            this.checkBoxLaunchIfNotOpen.Text = "Launch application if doesn\'t exisit";
            this.checkBoxLaunchIfNotOpen.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonApply.Location = new System.Drawing.Point(3, 73);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(279, 29);
            this.buttonApply.TabIndex = 2;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonEditProfiles
            // 
            this.buttonEditProfiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEditProfiles.Location = new System.Drawing.Point(3, 310);
            this.buttonEditProfiles.Name = "buttonEditProfiles";
            this.buttonEditProfiles.Size = new System.Drawing.Size(279, 19);
            this.buttonEditProfiles.TabIndex = 3;
            this.buttonEditProfiles.Text = "Edit Profiles";
            this.buttonEditProfiles.UseVisualStyleBackColor = true;
            this.buttonEditProfiles.Click += new System.EventHandler(this.buttonEditProfiles_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(3, 108);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(279, 14);
            this.progressBar.TabIndex = 4;
            // 
            // timerCheckWindowsOpened
            // 
            this.timerCheckWindowsOpened.Interval = 500;
            this.timerCheckWindowsOpened.Tick += new System.EventHandler(this.timerCheckWindowsOpened_Tick);
            // 
            // textBoxLog
            // 
            this.textBoxLog.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Location = new System.Drawing.Point(3, 128);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(279, 176);
            this.textBoxLog.TabIndex = 5;
            // 
            // FormApplyProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 332);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormApplyProfile";
            this.Text = "LayoutSnapshot";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.CheckBox checkBoxLaunchIfNotOpen;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonEditProfiles;
        private System.Windows.Forms.Timer timerCheckWindowsOpened;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}