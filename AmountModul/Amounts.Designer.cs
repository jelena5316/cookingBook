﻿
namespace MajPAbGr_project
{
    partial class InsertAmounts
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
            this.btn_submit = new System.Windows.Forms.Button();
            this.txbRecipe = new System.Windows.Forms.TextBox();
            this.txbAmounts = new System.Windows.Forms.TextBox();
            this.lvRecipe = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.cmbIngr = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.localizacijaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblReload = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblMain = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRecipe = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCoef = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(786, 229);
            this.btn_submit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(91, 40);
            this.btn_submit.TabIndex = 2;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // txbRecipe
            // 
            this.txbRecipe.Location = new System.Drawing.Point(463, 236);
            this.txbRecipe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbRecipe.Name = "txbRecipe";
            this.txbRecipe.Size = new System.Drawing.Size(238, 26);
            this.txbRecipe.TabIndex = 0;
            // 
            // txbAmounts
            // 
            this.txbAmounts.Location = new System.Drawing.Point(258, 31);
            this.txbAmounts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbAmounts.Name = "txbAmounts";
            this.txbAmounts.Size = new System.Drawing.Size(55, 26);
            this.txbAmounts.TabIndex = 1;
            this.txbAmounts.Text = "100";
            // 
            // lvRecipe
            // 
            this.lvRecipe.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lvRecipe.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvRecipe.GridLines = true;
            this.lvRecipe.HideSelection = false;
            this.lvRecipe.Location = new System.Drawing.Point(26, 60);
            this.lvRecipe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvRecipe.Name = "lvRecipe";
            this.lvRecipe.Size = new System.Drawing.Size(399, 407);
            this.lvRecipe.TabIndex = 1;
            this.lvRecipe.UseCompatibleStateImageBehavior = false;
            this.lvRecipe.View = System.Windows.Forms.View.Details;
            this.lvRecipe.SelectedIndexChanged += new System.EventHandler(this.lvRecipe_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ingredients";
            this.columnHeader1.Width = 131;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amounts (g)";
            this.columnHeader2.Width = 136;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Amounts (%)";
            this.columnHeader3.Width = 127;
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(9, 67);
            this.btn_remove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(93, 35);
            this.btn_remove.TabIndex = 4;
            this.btn_remove.Text = "remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(330, 27);
            this.btn_edit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(75, 37);
            this.btn_edit.TabIndex = 2;
            this.btn_edit.Text = "add";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // cmbIngr
            // 
            this.cmbIngr.FormattingEnabled = true;
            this.cmbIngr.Location = new System.Drawing.Point(9, 29);
            this.cmbIngr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbIngr.Name = "cmbIngr";
            this.cmbIngr.Size = new System.Drawing.Size(238, 28);
            this.cmbIngr.TabIndex = 0;
            this.cmbIngr.Text = "pick an ingredients";
            this.cmbIngr.SelectedIndexChanged += new System.EventHandler(this.cmbIngr_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localizacijaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(909, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // localizacijaToolStripMenuItem
            // 
            this.localizacijaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.localizacijaToolStripMenuItem.Name = "localizacijaToolStripMenuItem";
            this.localizacijaToolStripMenuItem.Size = new System.Drawing.Size(34, 19);
            this.localizacijaToolStripMenuItem.Text = "RU";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItem1.Text = "US";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItem2.Text = "LV";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItem3.Text = "RU";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblReload);
            this.groupBox1.Controls.Add(this.txbAmounts);
            this.groupBox1.Controls.Add(this.btn_remove);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.cmbIngr);
            this.groupBox1.Location = new System.Drawing.Point(454, 60);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(422, 128);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ingredients";
            // 
            // lblReload
            // 
            this.lblReload.AutoSize = true;
            this.lblReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReload.Location = new System.Drawing.Point(346, 103);
            this.lblReload.Name = "lblReload";
            this.lblReload.Size = new System.Drawing.Size(60, 20);
            this.lblReload.TabIndex = 14;
            this.lblReload.Text = "Reload";
            this.lblReload.Click += new System.EventHandler(this.lblReload_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Save as recipe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(459, 317);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "main";
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Location = new System.Drawing.Point(530, 317);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(70, 20);
            this.lblMain.TabIndex = 8;
            this.lblMain.Text = "name_id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(459, 355);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "recipe";
            // 
            // lblRecipe
            // 
            this.lblRecipe.AutoSize = true;
            this.lblRecipe.Location = new System.Drawing.Point(530, 355);
            this.lblRecipe.Name = "lblRecipe";
            this.lblRecipe.Size = new System.Drawing.Size(18, 20);
            this.lblRecipe.TabIndex = 10;
            this.lblRecipe.Text = "1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(459, 397);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "coef";
            // 
            // lblCoef
            // 
            this.lblCoef.AutoSize = true;
            this.lblCoef.Location = new System.Drawing.Point(530, 397);
            this.lblCoef.Name = "lblCoef";
            this.lblCoef.Size = new System.Drawing.Size(18, 20);
            this.lblCoef.TabIndex = 12;
            this.lblCoef.Text = "1";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(22, 35);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(103, 20);
            this.lblName.TabIndex = 13;
            this.lblName.Text = "Recipe name";
            // 
            // InsertAmounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 487);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCoef);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblRecipe);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.txbRecipe);
            this.Controls.Add(this.lvRecipe);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "InsertAmounts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InsertAmounts";
            this.Load += new System.EventHandler(this.InsertAmounts_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.TextBox txbRecipe;
        private System.Windows.Forms.TextBox txbAmounts;
        private System.Windows.Forms.ListView lvRecipe;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.ComboBox cmbIngr;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem localizacijaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRecipe;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCoef;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblReload;
    }
}