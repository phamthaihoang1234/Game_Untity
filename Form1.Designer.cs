
namespace WindowsFormsApp1
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbLevel = new System.Windows.Forms.Label();
            this.lbEnemy = new System.Windows.Forms.Label();
            this.lbCount = new System.Windows.Forms.Label();
            this.lbEnemyLevel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 728);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // lbLevel
            // 
            this.lbLevel.AutoSize = true;
            this.lbLevel.Location = new System.Drawing.Point(297, 653);
            this.lbLevel.Name = "lbLevel";
            this.lbLevel.Size = new System.Drawing.Size(81, 17);
            this.lbLevel.TabIndex = 1;
            this.lbLevel.Text = "Level: Easy";
            // 
            // lbEnemy
            // 
            this.lbEnemy.AutoSize = true;
            this.lbEnemy.Location = new System.Drawing.Point(557, 652);
            this.lbEnemy.Name = "lbEnemy";
            this.lbEnemy.Size = new System.Drawing.Size(89, 17);
            this.lbEnemy.TabIndex = 2;
            this.lbEnemy.Text = "Enemy Kill: 0";
            this.lbEnemy.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(451, 13);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(88, 17);
            this.lbCount.TabIndex = 3;
            this.lbCount.Text = "Count Down:";
            // 
            // lbEnemyLevel
            // 
            this.lbEnemyLevel.AutoSize = true;
            this.lbEnemyLevel.Location = new System.Drawing.Point(560, 13);
            this.lbEnemyLevel.Name = "lbEnemyLevel";
            this.lbEnemyLevel.Size = new System.Drawing.Size(123, 17);
            this.lbEnemyLevel.TabIndex = 4;
            this.lbEnemyLevel.Text = "Enemy\'s Level: 01";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 728);
            this.Controls.Add(this.lbEnemyLevel);
            this.Controls.Add(this.lbCount);
            this.Controls.Add(this.lbEnemy);
            this.Controls.Add(this.lbLevel);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbLevel;
        private System.Windows.Forms.Label lbEnemy;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.Label lbEnemyLevel;
    }
}

