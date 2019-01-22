namespace AppPerformance.SkinControl
{
    partial class FrmSkinMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSkinMain));
            this.contextMenuStrip_skin = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.喜庆红ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.青草地ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.护眼绿ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_content = new System.Windows.Forms.Panel();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_title = new System.Windows.Forms.Panel();
            this.pictureBox_skin = new System.Windows.Forms.PictureBox();
            this.pictureBox_min = new System.Windows.Forms.PictureBox();
            this.pictureBox_max = new System.Windows.Forms.PictureBox();
            this.pictureBox_close = new System.Windows.Forms.PictureBox();
            this.label_title = new System.Windows.Forms.Label();
            this.pictureBox_icon = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip_skin.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.panel_title.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_skin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip_skin
            // 
            this.contextMenuStrip_skin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.喜庆红ToolStripMenuItem,
            this.青草地ToolStripMenuItem,
            this.护眼绿ToolStripMenuItem});
            this.contextMenuStrip_skin.Name = "contextMenuStrip_skin";
            this.contextMenuStrip_skin.Size = new System.Drawing.Size(153, 92);
            // 
            // 喜庆红ToolStripMenuItem
            // 
            this.喜庆红ToolStripMenuItem.Name = "喜庆红ToolStripMenuItem";
            this.喜庆红ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.喜庆红ToolStripMenuItem.Tag = "festival_red";
            this.喜庆红ToolStripMenuItem.Text = "喜庆红";
            this.喜庆红ToolStripMenuItem.Click += new System.EventHandler(this.SkinToolStripMenuItem_Click);
            // 
            // 青草地ToolStripMenuItem
            // 
            this.青草地ToolStripMenuItem.Name = "青草地ToolStripMenuItem";
            this.青草地ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.青草地ToolStripMenuItem.Tag = "grass";
            this.青草地ToolStripMenuItem.Text = "青草地";
            this.青草地ToolStripMenuItem.Click += new System.EventHandler(this.SkinToolStripMenuItem_Click);
            // 
            // 护眼绿ToolStripMenuItem
            // 
            this.护眼绿ToolStripMenuItem.Name = "护眼绿ToolStripMenuItem";
            this.护眼绿ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.护眼绿ToolStripMenuItem.Tag = "eye_green";
            this.护眼绿ToolStripMenuItem.Text = "护眼绿";
            this.护眼绿ToolStripMenuItem.Click += new System.EventHandler(this.SkinToolStripMenuItem_Click);
            // 
            // panel_content
            // 
            this.panel_content.Location = new System.Drawing.Point(2, 60);
            this.panel_content.Name = "panel_content";
            this.panel_content.Size = new System.Drawing.Size(476, 100);
            this.panel_content.TabIndex = 2;
            // 
            // panel_bottom
            // 
            this.panel_bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(153)))), ((int)(((byte)(83)))));
            this.panel_bottom.Controls.Add(this.label1);
            this.panel_bottom.Location = new System.Drawing.Point(2, 248);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(476, 20);
            this.panel_bottom.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = " V1.0";
            // 
            // panel_title
            // 
            this.panel_title.BackgroundImage = global::AppPerformance.Properties.Resources.skin06;
            this.panel_title.Controls.Add(this.pictureBox_skin);
            this.panel_title.Controls.Add(this.pictureBox_min);
            this.panel_title.Controls.Add(this.pictureBox_max);
            this.panel_title.Controls.Add(this.pictureBox_close);
            this.panel_title.Controls.Add(this.label_title);
            this.panel_title.Controls.Add(this.pictureBox_icon);
            this.panel_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_title.Location = new System.Drawing.Point(0, 0);
            this.panel_title.Name = "panel_title";
            this.panel_title.Size = new System.Drawing.Size(480, 30);
            this.panel_title.TabIndex = 0;
            // 
            // pictureBox_skin
            // 
            this.pictureBox_skin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_skin.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_skin.Image = global::AppPerformance.Properties.Resources.SkinNormalBack;
            this.pictureBox_skin.Location = new System.Drawing.Point(352, 4);
            this.pictureBox_skin.Name = "pictureBox_skin";
            this.pictureBox_skin.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_skin.TabIndex = 2;
            this.pictureBox_skin.TabStop = false;
            this.pictureBox_skin.Tag = "title_skin";
            // 
            // pictureBox_min
            // 
            this.pictureBox_min.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_min.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_min.Image = global::AppPerformance.Properties.Resources.MiniNormlBack;
            this.pictureBox_min.Location = new System.Drawing.Point(384, 4);
            this.pictureBox_min.Name = "pictureBox_min";
            this.pictureBox_min.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_min.TabIndex = 2;
            this.pictureBox_min.TabStop = false;
            this.pictureBox_min.Tag = "title_min";
            // 
            // pictureBox_max
            // 
            this.pictureBox_max.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_max.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_max.Image = global::AppPerformance.Properties.Resources.MaxNormlBack;
            this.pictureBox_max.Location = new System.Drawing.Point(416, 4);
            this.pictureBox_max.Name = "pictureBox_max";
            this.pictureBox_max.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_max.TabIndex = 2;
            this.pictureBox_max.TabStop = false;
            this.pictureBox_max.Tag = "title_max";
            // 
            // pictureBox_close
            // 
            this.pictureBox_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox_close.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_close.Image = global::AppPerformance.Properties.Resources.CloseNormlBack;
            this.pictureBox_close.Location = new System.Drawing.Point(448, 4);
            this.pictureBox_close.Name = "pictureBox_close";
            this.pictureBox_close.Size = new System.Drawing.Size(27, 22);
            this.pictureBox_close.TabIndex = 2;
            this.pictureBox_close.TabStop = false;
            this.pictureBox_close.Tag = "title_close";
            // 
            // label_title
            // 
            this.label_title.AutoSize = true;
            this.label_title.BackColor = System.Drawing.Color.Transparent;
            this.label_title.ForeColor = System.Drawing.Color.White;
            this.label_title.Location = new System.Drawing.Point(23, 9);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(131, 12);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "软件CPU、内存监测工具";
            // 
            // pictureBox_icon
            // 
            this.pictureBox_icon.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_icon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_icon.Image")));
            this.pictureBox_icon.Location = new System.Drawing.Point(5, 7);
            this.pictureBox_icon.Name = "pictureBox_icon";
            this.pictureBox_icon.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_icon.TabIndex = 0;
            this.pictureBox_icon.TabStop = false;
            // 
            // FrmSkinMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 270);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_content);
            this.Controls.Add(this.panel_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSkinMain";
            this.Text = "软件CPU、内存监测工具";
            this.SizeChanged += new System.EventHandler(this.FrmSkinMain_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmSkinMain_Paint);
            this.Resize += new System.EventHandler(this.FrmSkinMain_Resize);
            this.contextMenuStrip_skin.ResumeLayout(false);
            this.panel_bottom.ResumeLayout(false);
            this.panel_bottom.PerformLayout();
            this.panel_title.ResumeLayout(false);
            this.panel_title.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_skin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_title;
        private System.Windows.Forms.PictureBox pictureBox_icon;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.PictureBox pictureBox_close;
        private System.Windows.Forms.PictureBox pictureBox_skin;
        private System.Windows.Forms.PictureBox pictureBox_min;
        private System.Windows.Forms.PictureBox pictureBox_max;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_skin;
        private System.Windows.Forms.ToolStripMenuItem 喜庆红ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 青草地ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 护眼绿ToolStripMenuItem;
        protected System.Windows.Forms.Panel panel_content;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Label label1;
    }
}