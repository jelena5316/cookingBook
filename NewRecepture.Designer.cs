
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbTech = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txbRecepture
            // 
            this.txbRecepture.Location = new System.Drawing.Point(12, 26);
            this.txbRecepture.Name = "txbRecepture";
            this.txbRecepture.Size = new System.Drawing.Size(263, 20);
            this.txbRecepture.TabIndex = 23;
            this.txbRecepture.Text = "recepture\'s name";
            // 
            // cmbCat
            // 
            this.cmbCat.FormattingEnabled = true;
            this.cmbCat.Location = new System.Drawing.Point(12, 66);
            this.cmbCat.Name = "cmbCat";
            this.cmbCat.Size = new System.Drawing.Size(159, 21);
            this.cmbCat.TabIndex = 25;
            this.cmbCat.Text = "choose category";
            this.cmbCat.SelectedIndexChanged += new System.EventHandler(this.cmbCat_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(448, 370);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 27);
            this.button2.TabIndex = 25;
            this.button2.Text = "submit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txbSource
            // 
            this.txbSource.Location = new System.Drawing.Point(12, 150);
            this.txbSource.Name = "txbSource";
            this.txbSource.Size = new System.Drawing.Size(263, 20);
            this.txbSource.TabIndex = 28;
            this.txbSource.Text = "source";
            // 
            // txbAuthor
            // 
            this.txbAuthor.Location = new System.Drawing.Point(12, 188);
            this.txbAuthor.Name = "txbAuthor";
            this.txbAuthor.Size = new System.Drawing.Size(263, 20);
            this.txbAuthor.TabIndex = 29;
            this.txbAuthor.Text = "author";
            // 
            // txbURL
            // 
            this.txbURL.Location = new System.Drawing.Point(12, 224);
            this.txbURL.Name = "txbURL";
            this.txbURL.Size = new System.Drawing.Size(263, 20);
            this.txbURL.TabIndex = 30;
            this.txbURL.Text = "URL";
            // 
            // txbDescription
            // 
            this.txbDescription.Location = new System.Drawing.Point(12, 262);
            this.txbDescription.Multiline = true;
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(502, 83);
            this.txbDescription.TabIndex = 31;
            this.txbDescription.Text = "Description";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(427, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Insert ingredients >>";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(367, 370);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 27);
            this.button1.TabIndex = 33;
            this.button1.Text = "delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbTech
            // 
            this.cmbTech.FormattingEnabled = true;
            this.cmbTech.Location = new System.Drawing.Point(12, 107);
            this.cmbTech.Name = "cmbTech";
            this.cmbTech.Size = new System.Drawing.Size(159, 21);
            this.cmbTech.TabIndex = 34;
            this.cmbTech.Text = "choose technology";
            // 
            // NewRecepture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 419);
            this.Controls.Add(this.cmbTech);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbDescription);
            this.Controls.Add(this.txbURL);
            this.Controls.Add(this.txbAuthor);
            this.Controls.Add(this.txbSource);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmbCat);
            this.Controls.Add(this.txbRecepture);
            this.Name = "NewRecepture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewRecepture";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbTech;
    }
}