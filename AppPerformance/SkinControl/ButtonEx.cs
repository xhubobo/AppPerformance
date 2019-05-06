using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace AppPerformance
{
    [ToolboxItem(true)]
    public class ButtonEx : Button
    {
        //用来标示是否鼠标正在悬浮在按钮上  true:悬浮在按钮上 false:鼠标离开了按钮
        private bool m_bMouseHover;

        //用来标示是否鼠标点击了按钮  true：按下了按钮 false：松开了按钮
        private bool m_bMouseDown;

        public Font FontEx;

        public ButtonEx()
        {
            //首先开启双缓冲，防止闪烁
            //双缓冲的一大堆设置 具体参数含义参照msdn的ControlStyles枚举值
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        //重写鼠标悬浮的事件
        protected override void OnMouseEnter(EventArgs e)
        {
            //当鼠标进入控件时，标示变量为进入了控件
            m_bMouseHover = true;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        //重载鼠标离开的事件
        protected override void OnMouseLeave(EventArgs e)
        {
            //当鼠标离开控件时，标示变量为离开了控件
            m_bMouseHover = false;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        //重写鼠标按下的事件
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            //当鼠标按下控件时，标示变量为按下了控件
            m_bMouseDown = true;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseDown(mevent);
        }

        //重写鼠标松开的事件
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            //当鼠标松开时，标示变量为按下并松开了控件
            m_bMouseDown = false;
            //刷新面板触发OnPaint重绘
            this.Invalidate();
            base.OnMouseUp(mevent);
        }

        //重写绘画事件
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            //因为上面调用了base会绘制原生控件 重刷一下背景清掉原生绘制 不然自己绘制的是重叠在原生绘制上
            base.OnPaintBackground(pevent);
            //得到绘画句柄
            Graphics g = pevent.Graphics;
            //定义字体格式
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            //处理热键 当Alt点下时
            sf.HotkeyPrefix = this.ShowKeyboardCues ? HotkeyPrefix.Show : HotkeyPrefix.Hide;
            //判断使用什么资源图
            Bitmap bmpDraw = Properties.Resources.skinButton18_NormlBack;
            //如果禁用了，则使用禁用时的样式图片绘制，否则调用其他满足条件的样式图片绘制
            if (!this.Enabled) bmpDraw = Properties.Resources.skinButton18_NormlBack;
            else if (m_bMouseDown) bmpDraw = Properties.Resources.skinButton18_DownBack;
            else if (m_bMouseHover) bmpDraw = Properties.Resources.skinButton18_MouseBack;
            else if (this.Focused) bmpDraw = Properties.Resources.skinButton18_NormlBack;
            //绘制背景(若不知道这句啥意思 参照九宫切图里面的代码)
            RenderHelper.DrawImageWithNineRect(g, bmpDraw, this.ClientRectangle);
            //如果禁用了
            if (!this.Enabled)
            {
                //则绘制双重阴影文字
                g.DrawString(this.Text, this.Font, Brushes.White, this.ClientRectangle, sf);
                g.TranslateTransform(-1, -1);//左上移动一个单位坐标系
                g.DrawString(this.Text, this.Font, Brushes.DarkGray, this.ClientRectangle, sf);
                g.ResetTransform();
                return;
            }
            //否则，默认绘制正常字体
            using (SolidBrush sb = new SolidBrush(this.ForeColor))
            {
                try
                {
                    g.DrawString(this.Text, this.Font, sb, this.ClientRectangle, sf);
                }
                catch (System.ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
