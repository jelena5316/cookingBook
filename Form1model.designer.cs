﻿
namespace MajPAbGr_project
{
    partial class Form1
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ingredientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertIgredientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tecnologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDbEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.lbl_koef = new System.Windows.Forms.Label();
            this.lbl_info = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txb_coeff = new System.Windows.Forms.TextBox();
            this.txb_new_recipe = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_insert = new System.Windows.Forms.Button();
            this.cmb_option = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.printToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(27, 73);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(275, 285);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ingridients";
            this.columnHeader1.Width = 149;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amounts (%)";
            this.columnHeader2.Width = 120;
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(27, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(275, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.Text = "recipe (in %)";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.goToToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.openDbEditorToolStripMenuItem,
            this.printToFileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(588, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // goToToolStripMenuItem
            // 
            this.goToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ingredientsToolStripMenuItem,
            this.recipeToolStripMenuItem,
            this.receptureToolStripMenuItem,
            this.tecnologyToolStripMenuItem});
            this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            this.goToToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.goToToolStripMenuItem.Text = "Go To";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem1.Text = "Categorires";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ingredientsToolStripMenuItem
            // 
            this.ingredientsToolStripMenuItem.Name = "ingredientsToolStripMenuItem";
            this.ingredientsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.ingredientsToolStripMenuItem.Text = "Ingredients";
            this.ingredientsToolStripMenuItem.Click += new System.EventHandler(this.ingredientsToolStripMenuItem_Click);
            // 
            // recipeToolStripMenuItem
            // 
            this.recipeToolStripMenuItem.Name = "recipeToolStripMenuItem";
            this.recipeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.recipeToolStripMenuItem.Text = "Recipe";
            // 
            // receptureToolStripMenuItem
            // 
            this.receptureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNewToolStripMenuItem,
            this.insertIgredientsToolStripMenuItem,
            this.editToolStripMenuItem});
            this.receptureToolStripMenuItem.Name = "receptureToolStripMenuItem";
            this.receptureToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.receptureToolStripMenuItem.Text = "Recepture";
            // 
            // addNewToolStripMenuItem
            // 
            this.addNewToolStripMenuItem.Name = "addNewToolStripMenuItem";
            this.addNewToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.addNewToolStripMenuItem.Text = "Add New";
            this.addNewToolStripMenuItem.Click += new System.EventHandler(this.addNewToolStripMenuItem_Click);
            // 
            // insertIgredientsToolStripMenuItem
            // 
            this.insertIgredientsToolStripMenuItem.Name = "insertIgredientsToolStripMenuItem";
            this.insertIgredientsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.insertIgredientsToolStripMenuItem.Text = "Insert Igredients";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click_1);
            // 
            // tecnologyToolStripMenuItem
            // 
            this.tecnologyToolStripMenuItem.Name = "tecnologyToolStripMenuItem";
            this.tecnologyToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.tecnologyToolStripMenuItem.Text = "Technology";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // openDbEditorToolStripMenuItem
            // 
            this.openDbEditorToolStripMenuItem.Name = "openDbEditorToolStripMenuItem";
            this.openDbEditorToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.openDbEditorToolStripMenuItem.Text = "Open db editor";
            // 
            // comboBox2
            // 
            this.comboBox2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBox2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(321, 36);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(126, 21);
            this.comboBox2.TabIndex = 6;
            this.comboBox2.Text = "recipes (in g)";
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // lbl_koef
            // 
            this.lbl_koef.AutoSize = true;
            this.lbl_koef.Location = new System.Drawing.Point(412, 20);
            this.lbl_koef.Name = "lbl_koef";
            this.lbl_koef.Size = new System.Drawing.Size(31, 13);
            this.lbl_koef.TabIndex = 19;
            this.lbl_koef.Text = "koeff";
            // 
            // lbl_info
            // 
            this.lbl_info.AutoSize = true;
            this.lbl_info.Location = new System.Drawing.Point(318, 282);
            this.lbl_info.Name = "lbl_info";
            this.lbl_info.Size = new System.Drawing.Size(57, 13);
            this.lbl_info.TabIndex = 20;
            this.lbl_info.Text = "Recepture";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(478, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "btn_calc";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txb_coeff
            // 
            this.txb_coeff.Location = new System.Drawing.Point(6, 81);
            this.txb_coeff.Name = "txb_coeff";
            this.txb_coeff.Size = new System.Drawing.Size(74, 20);
            this.txb_coeff.TabIndex = 13;
            // 
            // txb_new_recipe
            // 
            this.txb_new_recipe.Location = new System.Drawing.Point(6, 55);
            this.txb_new_recipe.Name = "txb_new_recipe";
            this.txb_new_recipe.Size = new System.Drawing.Size(143, 20);
            this.txb_new_recipe.TabIndex = 15;
            this.txb_new_recipe.Text = "recipe name";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 107);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "calc_new";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btn_insert
            // 
            this.btn_insert.Location = new System.Drawing.Point(122, 158);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(87, 23);
            this.btn_insert.TabIndex = 16;
            this.btn_insert.Text = "btn_insert";
            this.btn_insert.UseVisualStyleBackColor = true;
            // 
            // cmb_option
            // 
            this.cmb_option.FormattingEnabled = true;
            this.cmb_option.Items.AddRange(new object[] {
            "main",
            "total",
            "coefficient"});
            this.cmb_option.Location = new System.Drawing.Point(135, 19);
            this.cmb_option.Name = "cmb_option";
            this.cmb_option.Size = new System.Drawing.Size(74, 21);
            this.cmb_option.TabIndex = 14;
            this.cmb_option.Text = "coefficient";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmb_option);
            this.groupBox1.Controls.Add(this.btn_insert);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txb_new_recipe);
            this.groupBox1.Controls.Add(this.txb_coeff);
            this.groupBox1.Location = new System.Drawing.Point(321, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 206);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New Recipe";
            this.groupBox1.Visible = false;
            // 
            // printToFileToolStripMenuItem
            // 
            this.printToFileToolStripMenuItem.Name = "printToFileToolStripMenuItem";
            this.printToFileToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.printToFileToolStripMenuItem.Text = "Print to file";
            this.printToFileToolStripMenuItem.Click += new System.EventHandler(this.printToFileToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 413);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_info);
            this.Controls.Add(this.lbl_koef);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receptures";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ingredientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recipeToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label lbl_koef;
        private System.Windows.Forms.ToolStripMenuItem receptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertIgredientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDbEditorToolStripMenuItem;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.ToolStripMenuItem tecnologyToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txb_coeff;
        private System.Windows.Forms.TextBox txb_new_recipe;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.ComboBox cmb_option;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem printToFileToolStripMenuItem;
    }
}

