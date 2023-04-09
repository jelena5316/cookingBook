
namespace MajPAbGr_project
{
    partial class NewRecepture
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
            this.txbRecepture = new System.Windows.Forms.TextBox();
            this.cmbCat = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txbSource = new System.Windows.Forms.TextBox();
            this.txbAuthor = new System.Windows.Forms.TextBox();
            this.txbURL = new System.Windows.Forms.TextBox();
            this.txbDescription = new System.Windows.Forms.TextBox();
            this.buttton1 = new System.Windows.Forms.Button();
            this.cmbTech = new System.Windows.Forms.ComboBox();
            this.chBox_technology = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbRecepture
            // 
            this.txbRecepture.Location = new System.Drawing.Point(28, 34);
            this.txbRecepture.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbRecepture.Name = "txbRecepture";
            this.txbRecepture.Size = new System.Drawing.Size(392, 26);
            this.txbRecepture.TabIndex = 23;
            // 
            // cmbCat
            // 
            this.cmbCat.FormattingEnabled = true;
            this.cmbCat.Location = new System.Drawing.Point(28, 95);
            this.cmbCat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(236, 28);
            this.cmbCat.TabIndex = 25;
            this.cmbCat.SelectedIndexChanged += new System.EventHandler(this.cmbCat_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(446, 631);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 42);
            this.button2.TabIndex = 25;
            this.button2.Text = "Submit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txbSource
            // 
            this.txbSource.Location = new System.Drawing.Point(24, 309);
            this.txbSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbSource.Name = "txbSource";
            this.txbSource.Size = new System.Drawing.Size(392, 26);
            this.txbSource.TabIndex = 28;
            // 
            // txbAuthor
            // 
            this.txbAuthor.Location = new System.Drawing.Point(28, 255);
            this.txbAuthor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbAuthor.Name = "txbAuthor";
            this.txbAuthor.Size = new System.Drawing.Size(392, 26);
            this.txbAuthor.TabIndex = 29;
            // 
            // txbURL
            // 
            this.txbURL.Location = new System.Drawing.Point(24, 366);
            this.txbURL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbURL.Name = "txbURL";
            this.txbURL.Size = new System.Drawing.Size(392, 26);
            this.txbURL.TabIndex = 30;
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(24, 451);
            this.txbDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txbDescription.Multiline = true;
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(517, 138);
            this.txbDescription.TabIndex = 31;
            // 
            // buttton1
            // 
            this.buttton1.Location = new System.Drawing.Point(324, 631);
            this.buttton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttton1.Name = "buttton1";
            this.buttton1.Size = new System.Drawing.Size(112, 42);
            this.buttton1.TabIndex = 33;
            this.buttton1.Text = "Delete";
            this.buttton1.UseVisualStyleBackColor = true;
            this.buttton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbTech
            // 
            this.cmbTech.FormattingEnabled = true;
            this.cmbTech.Location = new System.Drawing.Point(28, 158);
            this.cmbTech.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbTech.Name = "cmbTech";
            this.cmbTech.Size = new System.Drawing.Size(236, 28);
            this.cmbTech.TabIndex = 34;
            this.cmbTech.SelectedIndexChanged += new System.EventHandler(this.cmbTech_SelectedIndexChanged);
            // 
            // chBox_technology
            // 
            this.chBox_technology.AutoSize = true;
            this.chBox_technology.Location = new System.Drawing.Point(28, 200);
            this.chBox_technology.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chBox_technology.Name = "chBox_technology";
            this.chBox_technology.Size = new System.Drawing.Size(127, 24);
            this.chBox_technology.TabIndex = 35;
            this.chBox_technology.Text = "no technology";
            this.chBox_technology.UseVisualStyleBackColor = true;
            this.chBox_technology.CheckedChanged += new System.EventHandler(this.chBox_technology_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(436, 260);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Author";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(436, 314);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 37;
            this.label2.Text = "Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 371);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 20);
            this.label3.TabIndex = 38;
            this.label3.Text = "URL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 423);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 39;
            this.label4.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(436, 45);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 40;
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 100);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 41;
            this.label6.Text = "Choose category";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(290, 163);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 20);
            this.label7.TabIndex = 42;
            this.label7.Text = "Choose technology";
            // 
            // NewRecepture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 749);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chBox_technology);
            this.Controls.Add(this.cmbTech);
            this.Controls.Add(this.buttton1);
            this.Controls.Add(this.txbDescription);
            this.Controls.Add(this.txbURL);
            this.Controls.Add(this.txbAuthor);
            this.Controls.Add(this.txbSource);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmbCat);
            this.Controls.Add(this.txbRecepture);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "NewRecepture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About Recepture";
            this.Load += new System.EventHandler(this.NewRecepture_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txbRecepture;
        private System.Windows.Forms.ComboBox cmbCat;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txbSource;
        private System.Windows.Forms.TextBox txbAuthor;
        private System.Windows.Forms.TextBox txbURL;
        private System.Windows.Forms.TextBox txbDescription;
        private System.Windows.Forms.Button buttton1;
        private System.Windows.Forms.ComboBox cmbTech;
        private System.Windows.Forms.CheckBox chBox_technology;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}