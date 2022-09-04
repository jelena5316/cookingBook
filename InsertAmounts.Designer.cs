
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
            this.btn_recipe = new System.Windows.Forms.Button();
            this.txbRecipe = new System.Windows.Forms.TextBox();
            this.txbAmounts = new System.Windows.Forms.TextBox();
            this.btn_calc = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_remove = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_select = new System.Windows.Forms.Button();
            this.btn_edit = new System.Windows.Forms.Button();
            this.cmbIngr = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(220, 268);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(79, 31);
            this.btn_submit.TabIndex = 40;
            this.btn_submit.Text = "Submit";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_recipe
            // 
            this.btn_recipe.Location = new System.Drawing.Point(220, 175);
            this.btn_recipe.Name = "btn_recipe";
            this.btn_recipe.Size = new System.Drawing.Size(62, 20);
            this.btn_recipe.TabIndex = 37;
            this.btn_recipe.Text = "Insert";
            this.btn_recipe.UseVisualStyleBackColor = true;
            this.btn_recipe.Click += new System.EventHandler(this.button1_Click);
            // 
            // txbRecipe
            // 
            this.txbRecipe.Location = new System.Drawing.Point(18, 175);
            this.txbRecipe.Name = "txbRecipe";
            this.txbRecipe.Size = new System.Drawing.Size(190, 20);
            this.txbRecipe.TabIndex = 34;
            this.txbRecipe.Text = "recipe\'s name";
            // 
            // txbAmounts
            // 
            this.txbAmounts.Location = new System.Drawing.Point(171, 20);
            this.txbAmounts.Name = "txbAmounts";
            this.txbAmounts.Size = new System.Drawing.Size(54, 20);
            this.txbAmounts.TabIndex = 24;
            this.txbAmounts.Text = "amounts";
            // 
            // btn_calc
            // 
            this.btn_calc.Location = new System.Drawing.Point(231, 86);
            this.btn_calc.Name = "btn_calc";
            this.btn_calc.Size = new System.Drawing.Size(50, 24);
            this.btn_calc.TabIndex = 39;
            this.btn_calc.Text = "Calc";
            this.btn_calc.UseVisualStyleBackColor = true;
            this.btn_calc.Click += new System.EventHandler(this.button3_Click);
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
            this.listView1.Location = new System.Drawing.Point(321, 15);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(316, 285);
            this.listView1.TabIndex = 32;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Ingredients";
            this.columnHeader1.Width = 131;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Amounts (g)";
            this.columnHeader2.Width = 87;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Amounts (%)";
            this.columnHeader3.Width = 89;
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(6, 75);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(62, 23);
            this.btn_remove.TabIndex = 18;
            this.btn_remove.Text = "remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_select);
            this.groupBox1.Controls.Add(this.txbAmounts);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.btn_remove);
            this.groupBox1.Controls.Add(this.btn_calc);
            this.groupBox1.Controls.Add(this.cmbIngr);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(287, 126);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ingredients";
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(6, 46);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(62, 23);
            this.btn_select.TabIndex = 40;
            this.btn_select.Text = "select";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_edit
            // 
            this.btn_edit.Location = new System.Drawing.Point(231, 20);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(50, 20);
            this.btn_edit.TabIndex = 20;
            this.btn_edit.Text = "add";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // cmbIngr
            // 
            this.cmbIngr.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbIngr.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbIngr.FormattingEnabled = true;
            this.cmbIngr.Location = new System.Drawing.Point(6, 19);
            this.cmbIngr.Name = "cmbIngr";
            this.cmbIngr.Size = new System.Drawing.Size(159, 21);
            this.cmbIngr.TabIndex = 16;
            this.cmbIngr.Text = "pick an ingredients";
            this.cmbIngr.SelectedIndexChanged += new System.EventHandler(this.cmbIngr_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Save as recipe too";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(18, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 68);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edit mode`s options";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(110, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "add/remove items";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(95, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "change values";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // InsertAmounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 311);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_submit);
            this.Controls.Add(this.btn_recipe);
            this.Controls.Add(this.txbRecipe);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "InsertAmounts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InsertAmounts";
            this.Load += new System.EventHandler(this.InsertAmounts_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_submit;
        private System.Windows.Forms.Button btn_recipe;
        private System.Windows.Forms.TextBox txbRecipe;
        private System.Windows.Forms.TextBox txbAmounts;
        private System.Windows.Forms.Button btn_calc;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.ComboBox cmbIngr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}