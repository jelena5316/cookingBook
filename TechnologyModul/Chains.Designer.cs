
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
            this.lblCards = new System.Windows.Forms.Label();
            this.lblTechn = new System.Windows.Forms.Label();
            this.listBox_cards = new System.Windows.Forms.ListBox();
            this.listBox_tech = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(342, 149);
            this.btn_remove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(116, 40);
            this.btn_remove.TabIndex = 23;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(492, 160);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "used in {}";
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(507, 48);
            this.lblTest.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(51, 20);
            this.lblTest.TabIndex = 18;
            this.lblTest.Text = "Steps";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(342, 72);
            this.btn_add.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(116, 40);
            this.btn_add.TabIndex = 20;
            this.btn_add.Text = "Apply";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(572, 160);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(99, 20);
            this.lblInfo.TabIndex = 19;
            this.lblInfo.Text = "technologies";
            // 
            // cmbData
            // 
            this.cmbData.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbData.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbData.FormattingEnabled = true;
            this.cmbData.Location = new System.Drawing.Point(483, 72);
            this.cmbData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(295, 28);
            this.cmbData.TabIndex = 16;
            this.cmbData.SelectedIndexChanged += new System.EventHandler(this.cmbData_SelectedIndexChanged);
            // 
            // cmbTechn
            // 
            this.cmbTechn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbTechn.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbTechn.FormattingEnabled = true;
            this.cmbTechn.Location = new System.Drawing.Point(18, 72);
            this.cmbTechn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTechn.Name = "cmbTechn";
            this.cmbTechn.Size = new System.Drawing.Size(295, 28);
            this.cmbTechn.TabIndex = 25;
            this.cmbTechn.SelectedIndexChanged += new System.EventHandler(this.cmbTechn_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Technologies or chain of steps";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 28;
            this.label2.Text = "has {}";
            // 
            // lblInfo2
            // 
            this.lblInfo2.AutoSize = true;
            this.lblInfo2.Location = new System.Drawing.Point(81, 160);
            this.lblInfo2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo2.Name = "lblInfo2";
            this.lblInfo2.Size = new System.Drawing.Size(58, 20);
            this.lblInfo2.TabIndex = 27;
            this.lblInfo2.Text = "card(s)";
            // 
            // lblCards
            // 
            this.lblCards.AutoSize = true;
            this.lblCards.Location = new System.Drawing.Point(498, 109);
            this.lblCards.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCards.Name = "lblCards";
            this.lblCards.Size = new System.Drawing.Size(134, 20);
            this.lblCards.TabIndex = 33;
            this.lblCards.Text = "description_cards";
            // 
            // lblTechn
            // 
            this.lblTechn.AutoSize = true;
            this.lblTechn.Location = new System.Drawing.Point(38, 109);
            this.lblTechn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTechn.Name = "lblTechn";
            this.lblTechn.Size = new System.Drawing.Size(126, 20);
            this.lblTechn.TabIndex = 34;
            this.lblTechn.Text = "description_tech";
            // 
            // listBox_cards
            // 
            this.listBox_cards.FormattingEnabled = true;
            this.listBox_cards.ItemHeight = 20;
            this.listBox_cards.Location = new System.Drawing.Point(18, 191);
            this.listBox_cards.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_cards.Name = "listBox_cards";
            this.listBox_cards.Size = new System.Drawing.Size(295, 264);
            this.listBox_cards.TabIndex = 37;
            // 
            // listBox_tech
            // 
            this.listBox_tech.FormattingEnabled = true;
            this.listBox_tech.ItemHeight = 20;
            this.listBox_tech.Location = new System.Drawing.Point(483, 191);
            this.listBox_tech.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBox_tech.Name = "listBox_tech";
            this.listBox_tech.Size = new System.Drawing.Size(295, 264);
            this.listBox_tech.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(338, 437);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 39;
            this.label3.Text = "To steps editor >>";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Chains
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 486);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listBox_tech);
            this.Controls.Add(this.listBox_cards);
            this.Controls.Add(this.lblTechn);
            this.Controls.Add(this.lblCards);
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
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Chains";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chains";
            this.Load += new System.EventHandler(this.Chains_Load);
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
        private System.Windows.Forms.Label lblCards;
        private System.Windows.Forms.Label lblTechn;
        private System.Windows.Forms.ListBox listBox_cards;
        private System.Windows.Forms.ListBox listBox_tech;
        private System.Windows.Forms.Label label3;
    }
}