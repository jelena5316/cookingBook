namespace MajPAbGr_project
{
    partial class EditDB
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.box = new System.Windows.Forms.ComboBox();
            this.lbl_db = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backupDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTablescsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmb_tables = new System.Windows.Forms.ToolStripComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 63);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(528, 146);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 219);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 2;
            this.button1.Text = "create table";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(429, 219);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 3;
            this.button2.Text = "execute view";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // box
            // 
            this.box.Location = new System.Drawing.Point(271, 223);
            this.box.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.box.Name = "box";
            this.box.Size = new System.Drawing.Size(150, 28);
            this.box.TabIndex = 4;
            this.box.SelectedIndexChanged += new System.EventHandler(this.box_ChangeIndex);
            // 
            // lbl_db
            // 
            this.lbl_db.AutoSize = true;
            this.lbl_db.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.lbl_db.Location = new System.Drawing.Point(12, 259);
            this.lbl_db.Name = "lbl_db";
            this.lbl_db.Size = new System.Drawing.Size(91, 17);
            this.lbl_db.TabIndex = 5;
            this.lbl_db.Text = "Sqlite_Studio";
            this.lbl_db.Click += new System.EventHandler(this.lbl_db_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupDbToolStripMenuItem,
            this.exportTablescsvToolStripMenuItem,
            this.cmb_tables});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(558, 27);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // backupDbToolStripMenuItem
            // 
            this.backupDbToolStripMenuItem.Name = "backupDbToolStripMenuItem";
            this.backupDbToolStripMenuItem.Size = new System.Drawing.Size(75, 23);
            this.backupDbToolStripMenuItem.Text = "Backup db";
            this.backupDbToolStripMenuItem.Click += new System.EventHandler(this.backupDbToolStripMenuItem_Click);
            // 
            // exportTablescsvToolStripMenuItem
            // 
            this.exportTablescsvToolStripMenuItem.Name = "exportTablescsvToolStripMenuItem";
            this.exportTablescsvToolStripMenuItem.Size = new System.Drawing.Size(110, 23);
            this.exportTablescsvToolStripMenuItem.Text = "Export table (csv)";
            this.exportTablescsvToolStripMenuItem.Click += new System.EventHandler(this.exportTablescsvToolStripMenuItem_Click);
            // 
            // cmb_tables
            // 
            this.cmb_tables.Name = "cmb_tables";
            this.cmb_tables.Size = new System.Drawing.Size(121, 23);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Input code and output";
            // 
            // EditDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 288);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_db);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.box);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "EditDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditDB";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditDB_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox box;
        private System.Windows.Forms.Label lbl_db;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem backupDbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTablescsvToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox cmb_tables;
        private System.Windows.Forms.Label label1;
    }
}