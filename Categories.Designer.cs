
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
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmb_categories
            // 
            this.cmb_categories.FormattingEnabled = true;
            this.cmb_categories.Location = new System.Drawing.Point(12, 30);
            this.cmb_categories.Name = "cmb_categories";
            this.cmb_categories.Size = new System.Drawing.Size(111, 21);
            this.cmb_categories.TabIndex = 0;
            this.cmb_categories.SelectedIndexChanged += new System.EventHandler(this.cmb_categories_SelectedIndexChanged);
            // 
            // lv_recepture
            // 
            this.lv_recepture.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lv_recepture.GridLines = true;
            this.lv_recepture.HideSelection = false;
            this.lv_recepture.Location = new System.Drawing.Point(12, 87);
            this.lv_recepture.Name = "lv_recepture";
            this.lv_recepture.Size = new System.Drawing.Size(337, 206);
            this.lv_recepture.TabIndex = 5;
            this.lv_recepture.UseCompatibleStateImageBehavior = false;
            this.lv_recepture.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 104;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Width = 106;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Width = 116;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "see all";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 326);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lv_recepture);
            this.Controls.Add(this.cmb_categories);
            this.Name = "Categories";
            this.Text = "Categories";
            this.Load += new System.EventHandler(this.Categories_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_categories;
        private System.Windows.Forms.ListView lv_recepture;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label label1;
    }
}