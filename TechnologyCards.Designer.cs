﻿
namespace MajPAbGr_project
{
    partial class TechnologyCards
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
            this.btn_add = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btn_insert = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTest = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.cmbData = new System.Windows.Forms.ComboBox();
            this.btn_remove = new System.Windows.Forms.Button();
            this.lblCardsOfTech = new System.Windows.Forms.Label();
            this.btn_new = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCards = new System.Windows.Forms.ComboBox();
            this.txbCards = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(9, 113);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(77, 23);
            this.btn_add.TabIndex = 20;
            this.btn_add.Text = "Apply";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Cards description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Cards name";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(24, 162);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(237, 56);
            this.textBox2.TabIndex = 17;
            // 
            // btn_insert
            // 
            this.btn_insert.Location = new System.Drawing.Point(24, 426);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(75, 23);
            this.btn_insert.TabIndex = 16;
            this.btn_insert.Text = "Insert";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(24, 240);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(237, 180);
            this.textBox3.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Technologie`s card";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblTest);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.lblInfo);
            this.groupBox1.Controls.Add(this.cmbData);
            this.groupBox1.Location = new System.Drawing.Point(284, 267);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 153);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chain`s editor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "used_in";
            // 
            // lblTest
            // 
            this.lblTest.AutoSize = true;
            this.lblTest.Location = new System.Drawing.Point(170, 53);
            this.lblTest.Name = "lblTest";
            this.lblTest.Size = new System.Drawing.Size(34, 13);
            this.lblTest.TabIndex = 18;
            this.lblTest.Text = "count";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(6, 53);
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
            this.cmbData.Location = new System.Drawing.Point(6, 22);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(198, 21);
            this.cmbData.TabIndex = 16;
            this.cmbData.Text = "Удалить";
            this.cmbData.SelectedIndexChanged += new System.EventHandler(this.cmbData_SelectedIndexChanged);
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(194, 50);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(67, 23);
            this.btn_remove.TabIndex = 18;
            this.btn_remove.Text = "Delete";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // lblCardsOfTech
            // 
            this.lblCardsOfTech.AutoSize = true;
            this.lblCardsOfTech.Location = new System.Drawing.Point(281, 60);
            this.lblCardsOfTech.Name = "lblCardsOfTech";
            this.lblCardsOfTech.Size = new System.Drawing.Size(111, 13);
            this.lblCardsOfTech.TabIndex = 25;
            this.lblCardsOfTech.Text = "Technology comtains:";
            // 
            // btn_new
            // 
            this.btn_new.Location = new System.Drawing.Point(105, 426);
            this.btn_new.Name = "btn_new";
            this.btn_new.Size = new System.Drawing.Size(75, 23);
            this.btn_new.TabIndex = 27;
            this.btn_new.Text = "Clear";
            this.btn_new.UseVisualStyleBackColor = true;
            this.btn_new.Click += new System.EventHandler(this.btn_new_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(191, 436);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Print Info";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // cmbCards
            // 
            this.cmbCards.FormattingEnabled = true;
            this.cmbCards.Location = new System.Drawing.Point(24, 50);
            this.cmbCards.Name = "cmbCards";
            this.cmbCards.Size = new System.Drawing.Size(156, 21);
            this.cmbCards.TabIndex = 29;
            this.cmbCards.SelectedIndexChanged += new System.EventHandler(this.cmbCards_SelectedIndexChanged);
            // 
            // txbCards
            // 
            this.txbCards.Location = new System.Drawing.Point(24, 123);
            this.txbCards.Name = "txbCards";
            this.txbCards.Size = new System.Drawing.Size(237, 20);
            this.txbCards.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Name";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(339, 76);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 32;
            this.comboBox1.Text = "Technologies";
            // 
            // TechnologyCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 494);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txbCards);
            this.Controls.Add(this.cmbCards);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_new);
            this.Controls.Add(this.lblCardsOfTech);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btn_insert);
            this.Name = "TechnologyCards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Technology Cards: create, delete and add to technology";
            this.Load += new System.EventHandler(this.TechnologyCards_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTest;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.ComboBox cmbData;
        private System.Windows.Forms.Label lblCardsOfTech;
        private System.Windows.Forms.Button btn_new;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCards;
        private System.Windows.Forms.TextBox txbCards;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}