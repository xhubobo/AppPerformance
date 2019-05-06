using AppPerformance.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPerformance.SkinControl
{
    public partial class FrmSkinMain : Form
    {
        public FrmSkinMain()
        {
            InitializeComponent();

            InitWindowMove();       //窗体移动支持
            InitSysButton();        //系统按钮设置
            InitDoubleClick();      //双击最大最小化

            SkinToolStripMenuItem_Click(护眼绿ToolStripMenuItem, null);

            //版本号
            label_version.Text = $"{Constants.MajorVersion}.{Constants.MinorVersion}";

            //最大化时不会遮盖任务栏
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
        }

        #region 窗体移动
        /* 首先将窗体的边框样式修改为None，让窗体没有标题栏
         * 实现这个效果使用了三个事件：鼠标按下、鼠标弹起、鼠标移动
         * 鼠标按下时更改变量isMouseDown标记窗体可以随鼠标的移动而移动
         * 鼠标移动时根据鼠标的移动量更改窗体的location属性，实现窗体移动
         * 鼠标弹起时更改变量isMouseDown标记窗体不可以随鼠标的移动而移动
         */

        //窗体移动标识
        private bool mFlagWindowMove = false;

        //窗体原始坐标和鼠标按下时的距离
        private Point mWindowOffset = new Point();

        /// <summary>
        /// 窗体移动支持
        /// </summary>
        private void InitWindowMove()
        {
            this.panel_title.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
            this.panel_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
            this.panel_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
        }

        //鼠标按下
        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            //鼠标左键按下
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            //鼠标在标题栏
            Point mousePos = e.Location;
            if (!this.panel_title.Bounds.Contains(mousePos))
            {
                return;
            }

            mousePos = PointToScreen(mousePos);
            int xOffset = mousePos.X - this.Location.X;
            int yOffset = mousePos.Y - this.Location.Y;
            mWindowOffset = new Point(-xOffset, -yOffset);
            mFlagWindowMove = true;  //开始移动
        }

        //鼠标移动
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (mFlagWindowMove)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mWindowOffset);
                this.Location = mousePos;
            }
        }

        //鼠标松开
        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //停止移动
                mFlagWindowMove = false;
            }
        }
        #endregion

        #region Sys Buttons
        /// <summary>
        /// 系统按钮事件绑定
        /// </summary>
        private void InitSysButton()
        {
            InitSysButton(pictureBox_min);
            InitSysButton(pictureBox_max);
            InitSysButton(pictureBox_close);
            InitSysButton(pictureBox_skin);
        }

        private void InitSysButton(PictureBox sysButton)
        {
            if (sysButton != null)
            {
                sysButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
                sysButton.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
                sysButton.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
                sysButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            }
        }

        /// <summary>
        /// 设置窗体的最大化、最小化和关闭按钮的单击事件
        /// </summary>
        /// <param n="int">标识</param>
        private void SysButtonClick(object sender)
        {
            PictureBox picBox = sender as PictureBox;
            if (picBox == null)
            {
                return;
            }

            Point mousePos = Control.MousePosition;
            mousePos = PointToClient(mousePos);
            if (!picBox.Bounds.Contains(mousePos))
            {
                return;
            }

            string tag = picBox.Tag.ToString();
            switch (tag)
            {
                case "title_skin":
                    {
                        var pos = pictureBox_skin.Location;
                        pos = PointToScreen(pos);
                        contextMenuStrip_skin.Show(Control.MousePosition);
                    }
                    break;
                case "title_min":
                    this.WindowState = FormWindowState.Minimized;
                    break;
                case "title_max":
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        this.WindowState = FormWindowState.Normal;
                    }
                    else if (this.WindowState == FormWindowState.Normal)
                    {
                        this.WindowState = FormWindowState.Maximized;
                    }
                    ImageSwitch(pictureBox_max, 1);
                    break;
                case "title_close":
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 控制图片的切换状态
        /// </summary>
        /// <param Frm_Tem="Form">要改变图片的对象</param>
        /// <param n="int">标识</param>
        /// <param ns="int">移出移入标识</param>
        protected void ImageSwitch(object sender, int type)
        {
            PictureBox picBox = sender as PictureBox;
            if (picBox == null)
            {
                return;
            }

            string tag = picBox.Tag.ToString();
            switch (tag)
            {
                case "title_skin":
                    switch (type)
                    {
                        case 0:     //MouseEnter
                            picBox.Image = Properties.Resources.SkinMouseBack;
                            break;
                        case 1:     //MouseLeave
                            picBox.Image = Properties.Resources.SkinNormalBack;
                            break;
                        case 2:     //MouseDown
                            picBox.Image = Properties.Resources.SkinDownBack;
                            break;
                        default:
                            break;
                    }
                    break;
                case "title_min":
                    switch (type)
                    {
                        case 0:     //MouseEnter
                            picBox.Image = Properties.Resources.MiniMouseBack;
                            break;
                        case 1:     //MouseLeave
                            picBox.Image = Properties.Resources.MiniNormlBack;
                            break;
                        case 2:     //MouseDown
                            picBox.Image = Properties.Resources.MiniDownBack;
                            break;
                        default:
                            break;
                    }
                    break;
                case "title_max":
                    if (this.WindowState == FormWindowState.Maximized)
                    {
                        switch (type)
                        {
                            case 0:     //MouseEnter
                                picBox.Image = Properties.Resources.RestoreMouseBack;
                                break;
                            case 1:     //MouseLeave
                                picBox.Image = Properties.Resources.RestoreNormlBack;
                                break;
                            case 2:     //MouseDown
                                picBox.Image = Properties.Resources.RestoreDownBack;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (this.WindowState == FormWindowState.Normal)
                    {
                        switch (type)
                        {
                            case 0:     //MouseEnter
                                picBox.Image = Properties.Resources.MaxMouseBack;
                                break;
                            case 1:     //MouseLeave
                                picBox.Image = Properties.Resources.MaxNormlBack;
                                break;
                            case 2:     //MouseDown
                                picBox.Image = Properties.Resources.MaxDownBack;
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case "title_close":
                    switch (type)
                    {
                        case 0:     //MouseEnter
                            picBox.Image = Properties.Resources.CloseMouseBack;
                            break;
                        case 1:     //MouseLeave
                            picBox.Image = Properties.Resources.CloseNormlBack;
                            break;
                        case 2:     //MouseDown
                            picBox.Image = Properties.Resources.CloseDownBack;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            ImageSwitch(sender, 0);
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            ImageSwitch(sender, 1);
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            ImageSwitch(sender, 2);
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            SysButtonClick(sender);
            //this.Invalidate();
        }
        #endregion

        #region 双击最大最小化
        private void InitDoubleClick()
        {
            panel_title.DoubleClick += new System.EventHandler(this.Form_DoubleClick);
        }

        private void Form_DoubleClick(object sender, EventArgs e)
        {
            //重置移动标记，防止双击还原后马上移动窗体
            mFlagWindowMove = false;

            this.WindowState = (this.WindowState == FormWindowState.Normal) ? FormWindowState.Maximized : FormWindowState.Normal;
        }
        #endregion

        #region 系统消息响应
        private void FrmSkinMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(mSkinAvgColor, 2);
            g.DrawRectangle(p, 0, 0, Width, Height);
        }

        private void FrmSkinMain_Resize(object sender, EventArgs e)
        {
            ImageSwitch(pictureBox_max, 1);
            this.Invalidate();
        }

        private void FrmSkinMain_SizeChanged(object sender, EventArgs e)
        {
            //relocation bottom frame
            this.panel_bottom.Width = this.Width - 2;
            this.panel_bottom.Height = 20;
            this.panel_bottom.Left = 1;
            this.panel_bottom.Top = this.Height - this.panel_bottom.Height;

            //relocation content frame
            this.panel_content.Width = this.Width - 4;
            this.panel_content.Height = this.Height - this.panel_title.Height - this.panel_bottom.Height - 2;
            this.panel_content.Left = 2;
            this.panel_content.Top = this.panel_title.Height;
        }
        #endregion

        #region 换肤
        private Color mSkinAvgColor = Color.Transparent;
        private string mSkinName = string.Empty;

        //换肤菜单点击响应
        private void SkinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //uncheck all menu items
            foreach (var item in contextMenuStrip_skin.Items)
            {
                var menuItem = item as ToolStripMenuItem;
                if (menuItem != null)
                {
                    menuItem.Checked = false;
                }
            }

            var toolStripMenuItem = sender as ToolStripMenuItem;
            if (toolStripMenuItem == null)
            {
                return;
            }

            //check current menu item
            toolStripMenuItem.Checked = true;

            if (mSkinName != toolStripMenuItem.Tag.ToString())
            {
                mSkinName = toolStripMenuItem.Tag.ToString();

                //get skin
                Bitmap bmp = null;
                switch (toolStripMenuItem.Tag.ToString())
                {
                    case "festival_red":    //喜庆红
                        bmp = Properties.Resources.skin03;
                        break;
                    case "eye_green":       //护眼绿
                        bmp = Properties.Resources.skin06;
                        break;
                    case "grass":           //青草地
                        bmp = Properties.Resources.skin13;
                        break;
                    case "state_grid":      //国网绿
                        bmp = Properties.Resources.skin25;
                        break;
                    default:
                        return;
                }

                //setup skin
                panel_title.BackgroundImage = bmp;

                //bottom
                mSkinAvgColor = ImageHelper.GetAvgColor(bmp, panel_title.Width, 100);
                panel_bottom.BackColor = mSkinAvgColor;
                CommonPara.SkinColor = mSkinAvgColor;

                //刷新边框线
                this.Invalidate(false);
            }
        }
        #endregion
    }
}
