
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
            this.lblInfo2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmbUsedIn = new System.Windows.Forms.ComboBox();
            this.cmbHasCards = new System.Windows.Forms.ComboBox();
            this.lblCards = new System.Windows.Forms.Label();
            this.lblTechn = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(466, 157);
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
            this.label4.Location = new System.Drawing.Point(261, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "used_in";
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(215, 100);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(33, 13);
            this.lblTest.TabIndex = 18;
            this.lblTest.Text = "cards";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(383, 157);
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
            this.lblInfo.Location = new System.Drawing.Point(306, 100);
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
            this.cmbData.Location = new System.Drawing.Point(11, 92);
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
            this.cmbTechn.Location = new System.Drawing.Point(12, 28);
            this.cmbTechn.Name = "cmbTechn";
            this.cmbTechn.Size = new System.Drawing.Size(198, 21);
            this.cmbTechn.TabIndex = 25;
            this.cmbTechn.SelectedIndexChanged += new System.EventHandler(this.cmbTechn_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 26;
            this.label1.Text = "techn";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "has";
            // 
            // lblInfo2
            // 
            this.lblInfo2.AutoSize = true;
            this.lblInfo2.Location = new System.Drawing.Point(292, 36);
            this.lblInfo2.Name = "lblInfo2";
            this.lblInfo2.Size = new System.Drawing.Size(39, 13);
            this.lblInfo2.TabIndex = 27;
            this.lblInfo2.Text = "card(s)";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 252);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(543, 22);
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
            // 
            // cmbUsedIn
            // 
            this.cmbUsedIn.FormattingEnabled = true;
            this.cmbUsedIn.Location = new System.Drawing.Point(360, 97);
            this.cmbUsedIn.Name = "cmbUsedIn";
            this.cmbUsedIn.Size = new System.Drawing.Size(165, 21);
            this.cmbUsedIn.TabIndex = 31;
            // 
            // cmbHasCards
            // 
            this.cmbHasCards.FormattingEnabled = true;
            this.cmbHasCards.Location = new System.Drawing.Point(361, 36);
            this.cmbHasCards.Name = "cmbHasCards";
            this.cmbHasCards.Size = new System.Drawing.Size(165, 21);
            this.cmbHasCards.TabIndex = 32;
            // 
            // lblCards
            // 
            this.lblCards.AutoSize = true;
            this.lblCards.Location = new System.Drawing.Point(11, 126);
            this.lblCards.Name = "lblCards";
            this.lblCards.Size = new System.Drawing.Size(55, 13);
            this.lblCards.TabIndex = 33;
            this.lblCards.Text = "cards (full)";
            // 
            // lblTechn
            // 
            this.lblTechn.AutoSize = true;
            this.lblTechn.Location = new System.Drawing.Point(11, 63);
            this.lblTechn.Name = "lblTechn";
            this.lblTechn.Size = new System.Drawing.Size(58, 13);
            this.lblTechn.TabIndex = 34;
            this.lblTechn.Text = "description";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(543, 25);
            this.toolStrip1.TabIndex = 35;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 22);
            this.toolStripLabel1.Text = "Technology";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(37, 22);
            this.toolStripLabel2.Text = "Cards";
            this.toolStripLabel2.Click += new System.EventHandler(this.toolStripLabel2_Click);
            // 
            // Chains
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 274);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lblTechn);
            this.Controls.Add(this.lblCards);
            this.Controls.Add(this.cmbHasCards);
            this.Controls.Add(this.cmbUsedIn);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblInfo2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbTechn);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.cmbData);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblTest);
            this.Name = "Chains";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chains";
            this.Load += new System.EventHandler(this.Chains_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.Label lblInfo2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ComboBox cmbUsedIn;
        private System.Windows.Forms.ComboBox cmbHasCards;
        private System.Windows.Forms.Label lblCards;
        private System.Windows.Forms.Label lblTechn;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    }
}