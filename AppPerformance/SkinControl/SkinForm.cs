using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPerformance
{
    public class SkinForm : Form
    {
        //窗体移动参考控件
        protected Control MoveTarget { get; set; }

        //最小化、最大化、关闭
        protected PictureBox SysButtonMin { get; set; }
        protected PictureBox SysButtonMax { get; set; }
        protected PictureBox SysButtonClose { get; set; }
        protected string CloseInfo { get; set; }

        protected FontFamily mIconFontFamily = null;

        public SkinForm()
        {
            //最大化时不会遮盖任务栏
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);

            //无边框
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        public void InitLoad()
        {
            string AppPath = Application.StartupPath;
            PrivateFontCollection font = new PrivateFontCollection();
            font.AddFontFile(AppPath + @"\Fonts\iconfont.ttf");   //字体的路径及名字
            mIconFontFamily = font.Families[0];

            //窗体移动支持
            InitWindowMove();

            //系统按钮事件绑定
            InitSysButton();
        }

        #region 窗体移动
        /* 首先将窗体的边框样式修改为None，让窗体没有标题栏
         * 实现这个效果使用了三个事件：鼠标按下、鼠标弹起、鼠标移动
         * 鼠标按下时更改变量isMouseDown标记窗体可以随鼠标的移动而移动
         * 鼠标移动时根据鼠标的移动量更改窗体的location属性，实现窗体移动
         * 鼠标弹起时更改变量isMouseDown标记窗体不可以随鼠标的移动而移动
         */

        //窗体移动标识
        protected bool flagWindowMove = false;

        //窗体原始坐标和鼠标按下时的距离
        private Point windowOffset = new Point();

        /// <summary>
        /// 窗体移动支持
        /// </summary>
        private void InitWindowMove()
        {
            if (MoveTarget != null)
            {
                MoveTarget.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form_MouseUp);
                MoveTarget.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form_MouseMove);
                MoveTarget.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            }
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
            if (!MoveTarget.Bounds.Contains(mousePos))
            {
                return;
            }

            //在非系统按钮处按下鼠标
            //             if (pictureBox_min.Bounds.Contains(mousePos) ||
            //                 pictureBox_max.Bounds.Contains(mousePos) ||
            //                 pictureBox_close.Bounds.Contains(mousePos))
            //             {
            //                 return;
            //             }

            mousePos = PointToScreen(mousePos);
            int xOffset = mousePos.X - Location.X;
            int yOffset = mousePos.Y - Location.Y;
            windowOffset = new Point(-xOffset, -yOffset);
            flagWindowMove = true;  //开始移动
        }

        //鼠标移动
        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (flagWindowMove)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(windowOffset);
                Location = mousePos;
            }
        }

        //鼠标松开
        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //停止移动
                flagWindowMove = false;
            }
        }
        #endregion

        #region Sys Buttons

        /// <summary>
        /// 系统按钮事件绑定
        /// </summary>
        private void InitSysButton()
        {
            InitSysButton(SysButtonMin);
            InitSysButton(SysButtonMax);
            InitSysButton(SysButtonClose);
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
            if (picBox == null || picBox.Tag == null)
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
                        //                         var pos = pictureBox_skin.Location;
                        //                         pos = PointToScreen(pos);
                        //                         contextMenuStrip1.Show(Control.MousePosition);
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
            if (picBox == null || picBox.Tag == null)
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
            this.Invalidate();
        }
        #endregion

        //groupBox绘制边框
        protected virtual void groupBox_Paint(object sender, PaintEventArgs e)
        {
            var groupBox = sender as GroupBox;
            if (groupBox != null)
            {
                e.Graphics.Clear(groupBox.Parent.BackColor);
                e.Graphics.DrawString(groupBox.Text, groupBox.Font, Brushes.Black, 10, 1);
                e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
                e.Graphics.DrawLine(Pens.Black, e.Graphics.MeasureString(groupBox.Text, groupBox.Font).Width + 8, 7, groupBox.Width - 2, 7);
                e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox.Height - 2);
                e.Graphics.DrawLine(Pens.Black, 1, groupBox.Height - 2, groupBox.Width - 2, groupBox.Height - 2);
                e.Graphics.DrawLine(Pens.Black, groupBox.Width - 2, 7, groupBox.Width - 2, groupBox.Height - 2);
            }
        }

        //设置iconfont按钮
        protected void InitIconButton(Button button, string text, int fontSize)
        {
            button.Font = new Font(mIconFontFamily, fontSize);
            button.Text = text;
        }

        //设置iconfont标签
        protected void InitIconLabel(Label label, string text, int fontSize)
        {
            label.Font = new Font(mIconFontFamily, fontSize);
            label.Text = text;
        }
    }
}
