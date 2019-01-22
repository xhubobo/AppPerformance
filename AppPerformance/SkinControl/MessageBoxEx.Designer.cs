namespace AppPerformance.SkinControl
{
    partial class MessageBoxEx
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
            this.panel_title = new System.Windows.Forms.Panel();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.label_title = new System.Windows.Forms.Label();
            this.label_icon = new System.Windows.Forms.Label();
            this.label_image = new System.Windows.Forms.Label();
            this.label_message = new System.Windows.Forms.Label();
            this.btn_ok = new AppPerformance.ButtonEx();
            this.btn_cancel = new AppPerformance.ButtonEx();
            this.panel_title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_title
            // 
            this.panel_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(83)))));
            this.panel_title.Controls.Add(this.pictureBox_close);
            this.panel_title.Controls.Add(this.label_title);
            this.panel_title.Controls.Add(this.label_icon);
            this.panel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_title.Location = new System.Drawing.Point(0, 0);
            this.panel_title.Name = "panel_title";
            this.panel_title.Size = new System.Drawing.Size(450, 30);
            this.panel_title.TabIndex = 0;
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Image = global::AppPerformance.Properties.Resources.CloseNormlBack;
            this.pictureBox_close.Location = new System.Drawing.Point(418, 4);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_close.TabIndex = 2;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Tag = "title_close";
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(27, 9);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(101, 12);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "自定义MessageBox";
            // 
            // label_icon
            // 
            this.label_icon.ForeColor = System.Drawing.Color.White;
            this.label_icon.Location = new System.Drawing.Point(5, 5);
            this.label_icon.Name = "label_icon";
            this.label_icon.Size = new System.Drawing.Size(20, 20);
            this.label_icon.TabIndex = 0;
            this.label_icon.Text = "icon";
            // 
            // label_image
            // 
            this.label_image.BackColor = System.Drawing.Color.Transparent;
            this.label_image.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(83)))));
            this.label_image.Location = new System.Drawing.Point(40, 55);
            this.label_image.Name = "label_image";
            this.label_image.Size = new System.Drawing.Size(100, 100);
            this.label_image.TabIndex = 0;
            this.label_image.Text = "icon";
            // 
            // label_message
            // 
            this.label_message.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_message.Location = new System.Drawing.Point(165, 60);
            this.label_message.Name = "label_message";
            this.label_message.Size = new System.Drawing.Size(240, 100);
            this.label_message.TabIndex = 1;
            this.label_message.Text = "新建一个WPF程序在Windows8下面就会出现左边的窗口边框，颜色取决于Windows主题。我想在想创建一个右边那样的窗口，要么是窄边，要么没有边。";
            this.label_message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.Transparent;
            this.btn_ok.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ok.Location = new System.Drawing.Point(100, 180);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(100, 30);
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.Location = new System.Drawing.Point(250, 180);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(100, 30);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // MessageBoxEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(450, 230);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.label_message);
            this.Controls.Add(this.panel_title);
            this.Controls.Add(this.label_image);
            this.Name = "MessageBoxEx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageBoxEx";
            this.Load += new System.EventHandler(this.MessageBoxEx_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MessageBoxEx_Paint);
            this.panel_title.ResumeLayout(false);
            this.panel_title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_title;
        private System.Windows.Forms.Label label_icon;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.Label label_image;
        private System.Windows.Forms.Label label_message;
        private ButtonEx btn_ok;
        private ButtonEx btn_cancel;
    }
}