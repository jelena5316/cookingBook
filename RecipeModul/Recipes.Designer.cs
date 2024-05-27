
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
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDbEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localizacijaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_insert = new System.Windows.Forms.Button();
            this.lbl_info = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
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
            this.btn_edit = new System.Windows.Forms.Button();
            this.txbRecipe = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCat = new System.Windows.Forms.ComboBox();
            this.lbl_SeeAll = new System.Windows.Forms.Label();
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
            this.listView1.Location = new System.Drawing.Point(458, 154);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(428, 431);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ingredients";
            this.columnHeader1.Width = 139;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amounts (%)";
            this.columnHeader2.Width = 141;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Amounts (g)";
            this.columnHeader3.Width = 172;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem,
            this.printToolStripMenuItem,
            this.openDbEditorToolStripMenuItem,
            this.localizacijaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(950, 31);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(70, 25);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(55, 25);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // openDbEditorToolStripMenuItem
            // 
            this.openDbEditorToolStripMenuItem.Name = "openDbEditorToolStripMenuItem";
            this.openDbEditorToolStripMenuItem.Size = new System.Drawing.Size(127, 25);
            this.openDbEditorToolStripMenuItem.Text = "Open db editor";
            this.openDbEditorToolStripMenuItem.Click += new System.EventHandler(this.openDbEditorToolStripMenuItem_Click);
            // 
            // localizacijaToolStripMenuItem
            // 
            this.localizacijaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSToolStripMenuItem,
            this.lVToolStripMenuItem,
            this.rUToolStripMenuItem});
            this.localizacijaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.localizacijaToolStripMenuItem.Name = "localizacijaToolStripMenuItem";
            this.localizacijaToolStripMenuItem.Size = new System.Drawing.Size(42, 25);
            this.localizacijaToolStripMenuItem.Text = "US";
            // 
            // uSToolStripMenuItem
            // 
            this.uSToolStripMenuItem.Name = "uSToolStripMenuItem";
            this.uSToolStripMenuItem.Size = new System.Drawing.Size(101, 26);
            this.uSToolStripMenuItem.Text = "US";
            this.uSToolStripMenuItem.Click += new System.EventHandler(this.uSToolStripMenuItem_Click);
            // 
            // lVToolStripMenuItem
            // 
            this.lVToolStripMenuItem.Name = "lVToolStripMenuItem";
            this.lVToolStripMenuItem.Size = new System.Drawing.Size(101, 26);
            this.lVToolStripMenuItem.Text = "LV";
            this.lVToolStripMenuItem.Click += new System.EventHandler(this.lVToolStripMenuItem_Click);
            // 
            // rUToolStripMenuItem
            // 
            this.rUToolStripMenuItem.Name = "rUToolStripMenuItem";
            this.rUToolStripMenuItem.Size = new System.Drawing.Size(101, 26);
            this.rUToolStripMenuItem.Text = "RU";
            this.rUToolStripMenuItem.Click += new System.EventHandler(this.rUToolStripMenuItem_Click);
            // 
            // btn_insert
            // 
            this.btn_insert.Location = new System.Drawing.Point(204, 212);
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
            this.lbl_info.Location = new System.Drawing.Point(48, 63);
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
            this.comboBox1.Location = new System.Drawing.Point(458, 88);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(312, 28);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(47, 231);
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
            this.cmbCoeff.Location = new System.Drawing.Point(47, 157);
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
            this.button1.Location = new System.Drawing.Point(299, 154);
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
            this.lbl_koef.Location = new System.Drawing.Point(340, 191);
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
            this.label3.Location = new System.Drawing.Point(52, 132);
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
            this.groupBox1.Controls.Add(this.cmb_option);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txb_new_recipe);
            this.groupBox1.Controls.Add(this.txb_coeff);
            this.groupBox1.Controls.Add(this.btn_insert);
            this.groupBox1.Location = new System.Drawing.Point(47, 313);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(348, 272);
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
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(168, 231);
            this.btn_edit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(100, 35);
            this.btn_edit.TabIndex = 25;
            this.btn_edit.Text = "Edit";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // txbRecipe
            // 
            this.txbRecipe.Location = new System.Drawing.Point(47, 194);
            this.txbRecipe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbRecipe.Name = "txbRecipe";
            this.txbRecipe.Size = new System.Drawing.Size(222, 26);
            this.txbRecipe.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(463, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Receptures\' list (in %)";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // cmbCat
            // 
            this.cmbCat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbCat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCat.FormattingEnabled = true;
            this.cmbCat.Location = new System.Drawing.Point(47, 88);
            this.cmbCat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(134, 28);
            this.cmbCat.TabIndex = 28;
            this.cmbCat.SelectedIndexChanged += new System.EventHandler(this.cmbCat_SelectedIndexChanged);
            // 
            // lbl_SeeAll
            // 
            this.lbl_SeeAll.AutoSize = true;
            this.lbl_SeeAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SeeAll.Location = new System.Drawing.Point(189, 96);
            this.lbl_SeeAll.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_SeeAll.Name = "lbl_SeeAll";
            this.lbl_SeeAll.Size = new System.Drawing.Size(51, 20);
            this.lbl_SeeAll.TabIndex = 29;
            this.lbl_SeeAll.Text = "select";
            this.lbl_SeeAll.Click += new System.EventHandler(this.lbl_SeeAll_Click);
            // 
            // Recipes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 665);
            this.Controls.Add(this.lbl_SeeAll);
            this.Controls.Add(this.cmbCat);
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
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDbEditorToolStripMenuItem;
        private System.Windows.Forms.Label lbl_info;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localizacijaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rUToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbRecipe;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cmbCat;
        private System.Windows.Forms.Label lbl_SeeAll;
    }
}

