
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.checkBoxLaunchIfNotOpen = new System.Windows.Forms.CheckBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonEditProfiles = new System.Windows.Forms.Button();
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
            this.tableLayoutPanel1.Controls.Add(this.buttonEditProfiles, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(285, 162);
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
            this.buttonEditProfiles.Location = new System.Drawing.Point(3, 140);
            this.buttonEditProfiles.Name = "buttonEditProfiles";
            this.buttonEditProfiles.Size = new System.Drawing.Size(279, 19);
            this.buttonEditProfiles.TabIndex = 3;
            this.buttonEditProfiles.Text = "Edit Profiles";
            this.buttonEditProfiles.UseVisualStyleBackColor = true;
            this.buttonEditProfiles.Click += new System.EventHandler(this.buttonEditProfiles_Click);
            // 
            // FormApplyProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 162);
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
    }
}