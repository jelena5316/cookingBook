
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
            this.txbRecepture.Location = new System.Drawing.Point(19, 22);
            this.txbRecepture.Name = "txbRecepture";
            this.txbRecepture.Size = new System.Drawing.Size(263, 20);
            this.txbRecepture.TabIndex = 23;
            // 
            // cmbCat
            // 
            this.cmbCat.FormattingEnabled = true;
            this.cmbCat.Location = new System.Drawing.Point(19, 62);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(159, 21);
            this.cmbCat.TabIndex = 25;
            this.cmbCat.SelectedIndexChanged += new System.EventHandler(this.cmbCat_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(297, 410);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 27);
            this.button2.TabIndex = 25;
            this.button2.Text = "Submit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txbSource
            // 
            this.txbSource.Location = new System.Drawing.Point(16, 201);
            this.txbSource.Name = "txbSource";
            this.txbSource.Size = new System.Drawing.Size(263, 20);
            this.txbSource.TabIndex = 28;
            // 
            // txbAuthor
            // 
            this.txbAuthor.Location = new System.Drawing.Point(19, 166);
            this.txbAuthor.Name = "txbAuthor";
            this.txbAuthor.Size = new System.Drawing.Size(263, 20);
            this.txbAuthor.TabIndex = 29;
            // 
            // txbURL
            // 
            this.txbURL.Location = new System.Drawing.Point(16, 238);
            this.txbURL.Name = "txbURL";
            this.txbURL.Size = new System.Drawing.Size(263, 20);
            this.txbURL.TabIndex = 30;
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(16, 293);
            this.txbDescription.Multiline = true;
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(346, 91);
            this.txbDescription.TabIndex = 31;
            // 
            // buttton1
            // 
            this.buttton1.Location = new System.Drawing.Point(216, 410);
            this.buttton1.Name = "buttton1";
            this.buttton1.Size = new System.Drawing.Size(75, 27);
            this.buttton1.TabIndex = 33;
            this.buttton1.Text = "Delete";
            this.buttton1.UseVisualStyleBackColor = true;
            this.buttton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbTech
            // 
            this.cmbTech.FormattingEnabled = true;
            this.cmbTech.Location = new System.Drawing.Point(19, 103);
            this.cmbTech.Name = "cmbTech";
            this.cmbTech.Size = new System.Drawing.Size(159, 21);
            this.cmbTech.TabIndex = 34;
            this.cmbTech.SelectedIndexChanged += new System.EventHandler(this.cmbTech_SelectedIndexChanged);
            // 
            // chBox_technology
            // 
            this.chBox_technology.AutoSize = true;
            this.chBox_technology.Location = new System.Drawing.Point(19, 130);
            this.chBox_technology.Name = "chBox_technology";
            this.chBox_technology.Size = new System.Drawing.Size(93, 17);
            this.chBox_technology.TabIndex = 35;
            this.chBox_technology.Text = "no technology";
            this.chBox_technology.UseVisualStyleBackColor = true;
            this.chBox_technology.CheckedChanged += new System.EventHandler(this.chBox_technology_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(291, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Author";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(291, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "URL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 39;
            this.label4.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(291, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 40;
            this.label5.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(193, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 13);
            this.label6.TabIndex = 41;
            this.label6.Text = "Choose category";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(193, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "Choose technology";
            // 
            // NewRecepture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 500);
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