namespace AppPerformance
{
    partial class FrmSetup
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
            this.btn_cancel = new AppPerformance.ButtonEx();
            this.btn_ok = new AppPerformance.ButtonEx();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.text_timer_interval = new System.Windows.Forms.TextBox();
            this.text_axis_span = new System.Windows.Forms.TextBox();
            this.text_axis_step_number = new System.Windows.Forms.TextBox();
            this.text_label_formatter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.text_update_interval = new System.Windows.Forms.TextBox();
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
            this.panel_title.TabIndex = 3;
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
            this.label_title.Size = new System.Drawing.Size(29, 12);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "设置";
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
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.Location = new System.Drawing.Point(250, 277);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(100, 30);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.Transparent;
            this.btn_ok.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ok.Location = new System.Drawing.Point(100, 277);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(100, 30);
            this.btn_ok.TabIndex = 7;
            this.btn_ok.Text = "应用";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "监测定时器间隔(s)：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "X坐标轴时间长度(min)：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "X坐标轴显示步进个数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 218);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "X坐标轴时间显示格式：";
            // 
            // text_timer_interval
            // 
            this.text_timer_interval.Location = new System.Drawing.Point(250, 50);
            this.text_timer_interval.MaxLength = 4;
            this.text_timer_interval.Name = "text_timer_interval";
            this.text_timer_interval.Size = new System.Drawing.Size(110, 21);
            this.text_timer_interval.TabIndex = 9;
            // 
            // text_axis_span
            // 
            this.text_axis_span.Location = new System.Drawing.Point(250, 132);
            this.text_axis_span.MaxLength = 4;
            this.text_axis_span.Name = "text_axis_span";
            this.text_axis_span.Size = new System.Drawing.Size(110, 21);
            this.text_axis_span.TabIndex = 9;
            // 
            // text_axis_step_number
            // 
            this.text_axis_step_number.Location = new System.Drawing.Point(250, 173);
            this.text_axis_step_number.MaxLength = 3;
            this.text_axis_step_number.Name = "text_axis_step_number";
            this.text_axis_step_number.Size = new System.Drawing.Size(110, 21);
            this.text_axis_step_number.TabIndex = 9;
            // 
            // text_label_formatter
            // 
            this.text_label_formatter.Location = new System.Drawing.Point(250, 214);
            this.text_label_formatter.MaxLength = 20;
            this.text_label_formatter.Name = "text_label_formatter";
            this.text_label_formatter.Size = new System.Drawing.Size(110, 21);
            this.text_label_formatter.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(250, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "yyyy-MM-dd HH:mm:ss";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(90, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "图表定时器间隔(s)：";
            // 
            // text_update_interval
            // 
            this.text_update_interval.Location = new System.Drawing.Point(250, 91);
            this.text_update_interval.MaxLength = 4;
            this.text_update_interval.Name = "text_update_interval";
            this.text_update_interval.Size = new System.Drawing.Size(110, 21);
            this.text_update_interval.TabIndex = 9;
            // 
            // FrmSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(450, 337);
            this.Controls.Add(this.text_label_formatter);
            this.Controls.Add(this.text_axis_step_number);
            this.Controls.Add(this.text_axis_span);
            this.Controls.Add(this.text_update_interval);
            this.Controls.Add(this.text_timer_interval);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel_title);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Name = "FrmSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmSetup";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmSetup_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmSetup_Paint);
            this.panel_title.ResumeLayout(false);
            this.panel_title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_title;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_icon;
        private ButtonEx btn_cancel;
        private ButtonEx btn_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox text_timer_interval;
        private System.Windows.Forms.TextBox text_axis_span;
        private System.Windows.Forms.TextBox text_axis_step_number;
        private System.Windows.Forms.TextBox text_label_formatter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox text_update_interval;
    }
}