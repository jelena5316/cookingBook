
namespace MajPAbGr_project
{
    partial class Chains
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
            this.btn_remove = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTest = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmbData = new System.Windows.Forms.ComboBox();
            this.cmbTechn = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmbUsedIn = new System.Windows.Forms.ComboBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(95, 142);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(60, 23);
            this.btn_remove.TabIndex = 23;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "used_in";
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(216, 42);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(33, 13);
            this.lblTest.TabIndex = 18;
            this.lblTest.Text = "cards";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(12, 142);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(77, 23);
            this.btn_add.TabIndex = 20;
            this.btn_add.Text = "Apply";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(333, 42);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(48, 13);
            this.lblInfo.TabIndex = 19;
            this.lblInfo.Text = "record(s)";
            // 
            // cmbData
            // 
            this.cmbData.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbData.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbData.FormattingEnabled = true;
            this.cmbData.Location = new System.Drawing.Point(12, 34);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(198, 21);
            this.cmbData.TabIndex = 16;
            this.cmbData.SelectedIndexChanged += new System.EventHandler(this.cmbData_SelectedIndexChanged);
            // 
            // cmbTechn
            // 
            this.cmbTechn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbTechn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTechn.FormattingEnabled = true;
            this.cmbTechn.Location = new System.Drawing.Point(12, 90);
            this.cmbTechn.Name = "cmbTechn";
            this.cmbTechn.Size = new System.Drawing.Size(198, 21);
            this.cmbTechn.TabIndex = 25;
            this.cmbTechn.SelectedIndexChanged += new System.EventHandler(this.cmbTechn_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "techn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "has";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(313, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "card(s)";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 209);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(611, 22);
            this.statusStrip1.TabIndex = 30;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel1.Text = "cards id";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel2.Text = "techns id";
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // cmbUsedIn
            // 
            this.cmbUsedIn.FormattingEnabled = true;
            this.cmbUsedIn.Location = new System.Drawing.Point(418, 39);
            this.cmbUsedIn.Name = "cmbUsedIn";
            this.cmbUsedIn.Size = new System.Drawing.Size(149, 21);
            this.cmbUsedIn.TabIndex = 31;
            this.cmbUsedIn.SelectedIndexChanged += new System.EventHandler(this.cmbUsedIn_SelectedIndexChanged);
            // 
            // Chains
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 231);
            this.Controls.Add(this.cmbUsedIn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbTechn);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.cmbData);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblTest);
            this.Name = "Chains";
            this.Text = "Chains";
            this.Load += new System.EventHandler(this.Chains_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.ComboBox cmbData;
        private System.Windows.Forms.ComboBox cmbTechn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ComboBox cmbUsedIn;
    }
}