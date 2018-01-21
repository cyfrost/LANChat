namespace LAN_Chat
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.author = new System.Windows.Forms.Label();
            this.build = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.appname = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // author
            // 
            this.author.AutoSize = true;
            this.author.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.author.Location = new System.Drawing.Point(71, 44);
            this.author.Name = "author";
            this.author.Size = new System.Drawing.Size(58, 15);
            this.author.TabIndex = 17;
            this.author.Text = "<author>";
            // 
            // build
            // 
            this.build.AutoSize = true;
            this.build.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.build.Location = new System.Drawing.Point(71, 28);
            this.build.Name = "build";
            this.build.Size = new System.Drawing.Size(50, 15);
            this.build.TabIndex = 16;
            this.build.Text = "<build>";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(179, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // appname
            // 
            this.appname.AutoSize = true;
            this.appname.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.appname.Location = new System.Drawing.Point(71, 12);
            this.appname.Name = "appname";
            this.appname.Size = new System.Drawing.Size(38, 15);
            this.appname.TabIndex = 14;
            this.appname.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 102);
            this.Controls.Add(this.author);
            this.Controls.Add(this.build);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.appname);
            this.Controls.Add(this.pictureBox1);
            this.Name = "About";
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label author;
        private System.Windows.Forms.Label build;
        private System.Windows.Forms.Button button2;
        protected internal System.Windows.Forms.Label appname;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}