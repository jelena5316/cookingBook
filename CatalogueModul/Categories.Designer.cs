﻿
namespace MajPAbGr_project
{
    partial class Categories
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
            this.cmb_categories = new System.Windows.Forms.ComboBox();
            this.lv_recepture = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tecnologyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ingredientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.amountsEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpOnlineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripCmbPrint = new System.Windows.Forms.ToolStripComboBox();
            this.seeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutReceptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineCalculatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_add = new System.Windows.Forms.Label();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_categories
            // 
            this.cmb_categories.FormattingEnabled = true;
            this.cmb_categories.Location = new System.Drawing.Point(305, 114);
            this.cmb_categories.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_categories.Name = "cmb_categories";
            this.cmb_categories.Size = new System.Drawing.Size(190, 28);
            this.cmb_categories.TabIndex = 0;
            this.cmb_categories.SelectedIndexChanged += new System.EventHandler(this.cmb_categories_SelectedIndexChanged);
            // 
            // lv_recepture
            // 
            this.lv_recepture.GridLines = true;
            this.lv_recepture.HideSelection = false;
            this.lv_recepture.Location = new System.Drawing.Point(18, 192);
            this.lv_recepture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lv_recepture.Name = "lv_recepture";
            this.lv_recepture.Size = new System.Drawing.Size(900, 342);
            this.lv_recepture.TabIndex = 5;
            this.lv_recepture.UseCompatibleStateImageBehavior = false;
            this.lv_recepture.View = System.Windows.Forms.View.Details;
            this.lv_recepture.SelectedIndexChanged += new System.EventHandler(this.lv_recepture_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 114);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(250, 26);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(700, 106);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 45);
            this.button1.TabIndex = 8;
            this.button1.Text = "Add new recipe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.goToToolStripMenuItem,
            this.printToolStripMenuItem,
            this.toolStripCmbPrint,
            this.seeAllToolStripMenuItem,
            this.aboutReceptureToolStripMenuItem,
            this.onlineCalculatorToolStripMenuItem,
            this.reloadToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(938, 29);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // goToToolStripMenuItem
            // 
            this.goToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recipeToolStripMenuItem,
            this.tecnologyToolStripMenuItem,
            this.ingredientsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.amountsEditorToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.helpOnlineToolStripMenuItem});
            this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            this.goToToolStripMenuItem.Size = new System.Drawing.Size(49, 23);
            this.goToToolStripMenuItem.Text = "Go To";
            // 
            // recipeToolStripMenuItem
            // 
            this.recipeToolStripMenuItem.Name = "recipeToolStripMenuItem";
            this.recipeToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.recipeToolStripMenuItem.Text = "Recipes";
            this.recipeToolStripMenuItem.Click += new System.EventHandler(this.recipeToolStripMenuItem_Click);
            // 
            // tecnologyToolStripMenuItem
            // 
            this.tecnologyToolStripMenuItem.Name = "tecnologyToolStripMenuItem";
            this.tecnologyToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.tecnologyToolStripMenuItem.Text = "Technology";
            this.tecnologyToolStripMenuItem.Click += new System.EventHandler(this.tecnologyToolStripMenuItem_Click);
            // 
            // ingredientsToolStripMenuItem
            // 
            this.ingredientsToolStripMenuItem.Name = "ingredientsToolStripMenuItem";
            this.ingredientsToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.ingredientsToolStripMenuItem.Text = "Ingredients";
            this.ingredientsToolStripMenuItem.Click += new System.EventHandler(this.ingredientsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuItem1.Text = "Categorires";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // amountsEditorToolStripMenuItem
            // 
            this.amountsEditorToolStripMenuItem.Name = "amountsEditorToolStripMenuItem";
            this.amountsEditorToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.amountsEditorToolStripMenuItem.Text = "Amounts";
            this.amountsEditorToolStripMenuItem.Click += new System.EventHandler(this.amountsEditorToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // helpOnlineToolStripMenuItem
            // 
            this.helpOnlineToolStripMenuItem.Name = "helpOnlineToolStripMenuItem";
            this.helpOnlineToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.helpOnlineToolStripMenuItem.Text = "Help online";
            this.helpOnlineToolStripMenuItem.Click += new System.EventHandler(this.helpOnlineToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(44, 23);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolStripCmbPrint
            // 
            this.toolStripCmbPrint.Items.AddRange(new object[] {
            "selected recipe",
            "statistic"});
            this.toolStripCmbPrint.Name = "toolStripCmbPrint";
            this.toolStripCmbPrint.Size = new System.Drawing.Size(180, 23);
            this.toolStripCmbPrint.Text = "print_option1";
            // 
            // seeAllToolStripMenuItem
            // 
            this.seeAllToolStripMenuItem.Name = "seeAllToolStripMenuItem";
            this.seeAllToolStripMenuItem.Size = new System.Drawing.Size(52, 23);
            this.seeAllToolStripMenuItem.Text = "See all";
            this.seeAllToolStripMenuItem.Click += new System.EventHandler(this.seeAllToolStripMenuItem_Click);
            // 
            // aboutReceptureToolStripMenuItem
            // 
            this.aboutReceptureToolStripMenuItem.Name = "aboutReceptureToolStripMenuItem";
            this.aboutReceptureToolStripMenuItem.Size = new System.Drawing.Size(108, 23);
            this.aboutReceptureToolStripMenuItem.Text = "About Recepture";
            this.aboutReceptureToolStripMenuItem.Click += new System.EventHandler(this.aboutReceptureToolStripMenuItem_Click);
            // 
            // onlineCalculatorToolStripMenuItem
            // 
            this.onlineCalculatorToolStripMenuItem.Name = "onlineCalculatorToolStripMenuItem";
            this.onlineCalculatorToolStripMenuItem.Size = new System.Drawing.Size(109, 23);
            this.onlineCalculatorToolStripMenuItem.Text = "Online calculator";
            this.onlineCalculatorToolStripMenuItem.Click += new System.EventHandler(this.onlineCalculatorToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Search by name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Categories` list";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 168);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Receptures";
            // 
            // lbl_add
            // 
            this.lbl_add.AutoSize = true;
            this.lbl_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_add.Location = new System.Drawing.Point(502, 118);
            this.lbl_add.Name = "lbl_add";
            this.lbl_add.Size = new System.Drawing.Size(69, 20);
            this.lbl_add.TabIndex = 13;
            this.lbl_add.Text = "add new";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(55, 23);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(938, 555);
            this.Controls.Add(this.lbl_add);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lv_recepture);
            this.Controls.Add(this.cmb_categories);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Categories";
            this.Text = "Categories";
            this.Load += new System.EventHandler(this.Categories_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_categories;
        private System.Windows.Forms.ListView lv_recepture;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ingredientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem amountsEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tecnologyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seeAllToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripComboBox toolStripCmbPrint;
        private System.Windows.Forms.ToolStripMenuItem aboutReceptureToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label lbl_add;
        private System.Windows.Forms.ToolStripMenuItem helpOnlineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onlineCalculatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
    }
}