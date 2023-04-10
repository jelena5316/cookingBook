﻿
namespace MajPAbGr_project
{
    partial class Recipes
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ingredientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.amountsEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tecnologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDbEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localizacijaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_insert = new System.Windows.Forms.Button();
            this.lbl_info = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.button3 = new System.Windows.Forms.Button();
            this.txb_coeff = new System.Windows.Forms.TextBox();
            this.cmbCoeff = new System.Windows.Forms.ComboBox();
            this.txb_new_recipe = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbl_koef = new System.Windows.Forms.Label();
            this.cmb_option = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_edit = new System.Windows.Forms.Button();
            this.txbRecipe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(38, 72);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(428, 526);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Names";
            this.columnHeader1.Width = 123;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amounts (%)";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "New";
            this.columnHeader3.Width = 172;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.openDbEditorToolStripMenuItem,
            this.printToolStripMenuItem,
            this.localizacijaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(950, 31);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // goToToolStripMenuItem
            // 
            this.goToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ingredientsToolStripMenuItem,
            this.recipeToolStripMenuItem,
            this.amountsEditorToolStripMenuItem,
            this.receptureToolStripMenuItem,
            this.tecnologyToolStripMenuItem});
            this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            this.goToToolStripMenuItem.Size = new System.Drawing.Size(49, 19);
            this.goToToolStripMenuItem.Text = "Go To";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem1.Text = "Categorires";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ingredientsToolStripMenuItem
            // 
            this.ingredientsToolStripMenuItem.Name = "ingredientsToolStripMenuItem";
            this.ingredientsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.ingredientsToolStripMenuItem.Text = "Ingredients";
            this.ingredientsToolStripMenuItem.Click += new System.EventHandler(this.ingredientsToolStripMenuItem_Click);
            // 
            // recipeToolStripMenuItem
            // 
            this.recipeToolStripMenuItem.Name = "recipeToolStripMenuItem";
            this.recipeToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.recipeToolStripMenuItem.Text = "Recipe editor";
            this.recipeToolStripMenuItem.Click += new System.EventHandler(this.recipeToolStripMenuItem_Click);
            // 
            // amountsEditorToolStripMenuItem
            // 
            this.amountsEditorToolStripMenuItem.Name = "amountsEditorToolStripMenuItem";
            this.amountsEditorToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.amountsEditorToolStripMenuItem.Text = "Amounts editor";
            this.amountsEditorToolStripMenuItem.Click += new System.EventHandler(this.amountsEditorToolStripMenuItem_Click);
            // 
            // receptureToolStripMenuItem
            // 
            this.receptureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.editToolStripMenuItem});
            this.receptureToolStripMenuItem.Name = "receptureToolStripMenuItem";
            this.receptureToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.receptureToolStripMenuItem.Text = "Recepture";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.addNewToolStripMenuItem.Text = "Add New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // tecnologyToolStripMenuItem
            // 
            this.tecnologyToolStripMenuItem.Name = "tecnologyToolStripMenuItem";
            this.tecnologyToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.tecnologyToolStripMenuItem.Text = "Technology";
            this.tecnologyToolStripMenuItem.Click += new System.EventHandler(this.technologyToolStripMenuItem_Click);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(55, 19);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // openDbEditorToolStripMenuItem
            // 
            this.openDbEditorToolStripMenuItem.Name = "openDbEditorToolStripMenuItem";
            this.openDbEditorToolStripMenuItem.Size = new System.Drawing.Size(99, 19);
            this.openDbEditorToolStripMenuItem.Text = "Open db editor";
            this.openDbEditorToolStripMenuItem.Click += new System.EventHandler(this.openDbEditorToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(44, 19);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // localizacijaToolStripMenuItem
            // 
            this.localizacijaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSToolStripMenuItem,
            this.lVToolStripMenuItem,
            this.rUToolStripMenuItem});
            this.localizacijaToolStripMenuItem.Name = "localizacijaToolStripMenuItem";
            this.localizacijaToolStripMenuItem.Size = new System.Drawing.Size(31, 19);
            this.localizacijaToolStripMenuItem.Text = "LV";
            // 
            // uSToolStripMenuItem
            // 
            this.uSToolStripMenuItem.Name = "uSToolStripMenuItem";
            this.uSToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
            this.uSToolStripMenuItem.Text = "US";
            this.uSToolStripMenuItem.Click += new System.EventHandler(this.uSToolStripMenuItem_Click);
            // 
            // lVToolStripMenuItem
            // 
            this.lVToolStripMenuItem.Name = "lVToolStripMenuItem";
            this.lVToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
            this.lVToolStripMenuItem.Text = "LV";
            this.lVToolStripMenuItem.Click += new System.EventHandler(this.lVToolStripMenuItem_Click);
            // 
            // rUToolStripMenuItem
            // 
            this.rUToolStripMenuItem.Name = "rUToolStripMenuItem";
            this.rUToolStripMenuItem.Size = new System.Drawing.Size(89, 22);
            this.rUToolStripMenuItem.Text = "RU";
            this.rUToolStripMenuItem.Click += new System.EventHandler(this.rUToolStripMenuItem_Click);
            // 
            // btn_insert
            // 
            this.btn_insert.Location = new System.Drawing.Point(204, 258);
            this.btn_insert.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(112, 38);
            this.btn_insert.TabIndex = 16;
            this.btn_insert.Text = "Insert";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(18, 618);
            this.lbl_info.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(116, 20);
            this.lbl_info.TabIndex = 20;
            this.lbl_info.Text = "Recepture Info";
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(543, 97);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(354, 28);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(543, 247);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 35);
            this.button3.TabIndex = 25;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txb_coeff
            // 
            this.txb_coeff.Location = new System.Drawing.Point(9, 29);
            this.txb_coeff.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txb_coeff.Name = "txb_coeff";
            this.txb_coeff.Size = new System.Drawing.Size(108, 26);
            this.txb_coeff.TabIndex = 13;
            // 
            // cmbCoeff
            // 
            this.cmbCoeff.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCoeff.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCoeff.FormattingEnabled = true;
            this.cmbCoeff.Location = new System.Drawing.Point(543, 174);
            this.cmbCoeff.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbCoeff.Name = "cmbCoeff";
            this.cmbCoeff.Size = new System.Drawing.Size(222, 28);
            this.cmbCoeff.TabIndex = 6;
            this.cmbCoeff.Text = "recipes (in g)";
            this.cmbCoeff.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // txb_new_recipe
            // 
            this.txb_new_recipe.Location = new System.Drawing.Point(9, 132);
            this.txb_new_recipe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txb_new_recipe.Name = "txb_new_recipe";
            this.txb_new_recipe.Size = new System.Drawing.Size(240, 26);
            this.txb_new_recipe.TabIndex = 15;
            this.txb_new_recipe.Text = "new recipe name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(795, 171);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 32);
            this.button1.TabIndex = 7;
            this.button1.Text = "Calc";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(204, 25);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 35);
            this.button2.TabIndex = 17;
            this.button2.Text = "Calc New";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbl_koef
            // 
            this.lbl_koef.AutoSize = true;
            this.lbl_koef.Location = new System.Drawing.Point(836, 208);
            this.lbl_koef.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_koef.Name = "lbl_koef";
            this.lbl_koef.Size = new System.Drawing.Size(45, 20);
            this.lbl_koef.TabIndex = 19;
            this.lbl_koef.Text = "koeff";
            // 
            // cmb_option
            // 
            this.cmb_option.FormattingEnabled = true;
            this.cmb_option.Items.AddRange(new object[] {
            "main",
            "total",
            "coefficient"});
            this.cmb_option.Location = new System.Drawing.Point(8, 69);
            this.cmb_option.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_option.Name = "cmb_option";
            this.cmb_option.Size = new System.Drawing.Size(109, 28);
            this.cmb_option.TabIndex = 14;
            this.cmb_option.Text = "main";
            this.cmb_option.SelectedIndexChanged += new System.EventHandler(this.cmb_option_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(547, 149);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "In grams";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.cmb_option);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txb_new_recipe);
            this.groupBox1.Controls.Add(this.txb_coeff);
            this.groupBox1.Controls.Add(this.btn_insert);
            this.groupBox1.Location = new System.Drawing.Point(543, 360);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(348, 323);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Insert recipe";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "clear";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "clear";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(8, 172);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 24);
            this.checkBox1.TabIndex = 26;
            this.checkBox1.Text = "Edit mode";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(664, 247);
            this.btn_edit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(101, 35);
            this.btn_edit.TabIndex = 25;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // txbRecipe
            // 
            this.txbRecipe.Location = new System.Drawing.Point(543, 211);
            this.txbRecipe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbRecipe.Name = "txbRecipe";
            this.txbRecipe.Size = new System.Drawing.Size(222, 26);
            this.txbRecipe.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(547, 72);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Recipes\' list (in %)";
            // 
            // Recipes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 749);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbRecipe);
            this.Controls.Add(this.btn_edit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_koef);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.cmbCoeff);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Recipes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receptures";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ingredientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recipeToolStripMenuItem;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.ToolStripMenuItem receptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDbEditorToolStripMenuItem;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.ToolStripMenuItem tecnologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localizacijaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem amountsEditorToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txb_coeff;
        public System.Windows.Forms.ComboBox cmbCoeff;
        private System.Windows.Forms.TextBox txb_new_recipe;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.Label lbl_koef;
        private System.Windows.Forms.ComboBox cmb_option;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbRecipe;
        private System.Windows.Forms.Label label4;
    }
}
