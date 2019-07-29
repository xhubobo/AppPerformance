using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AppPerformance.Common;

namespace AppPerformance.SkinControl
{
    public partial class MessageBoxEx : SkinForm
    {
        private const string NotifyIconError = "\ue634";
        private const string NotifyIconInfo = "\ue653";
        private const string NotifyIconQuestion = "\ue621";
        private const string NotifyIconWarning = "\ue6aa";
        private const string NotifyIconTitle = "\ue682";

        private int LabelImgFont = 45;

        /// <summary>
        /// 结果，用户点击确定Result=true
        /// </summary>
        public bool Result { get; private set; }

        private void CommonInit()
        {
            MoveTarget = this.panel_title;
            SysButtonClose = this.pictureBox_close;
            this.Result = false;

            this.panel_title.BackColor = CommonPara.SkinColor;
            //this.label_image.ForeColor = CommonPara.SkinColor;
        }

        public MessageBoxEx()
        {
            InitializeComponent();
            CommonInit();

            this.label_title.Text = string.Empty;       //标题
            this.label_message.Text = string.Empty;     //内容
            this.label_image.Text = NotifyIconWarning;  //图片
            this.btn_cancel.Visible = false;            //隐藏取消按钮
            this.Text = this.label_title.Text;          //将标题与label_title绑定
        }

        public MessageBoxEx(EnumNotifyType type, string message)
        {
            InitializeComponent();
            CommonInit();

            this.label_title.Text = type.GetDescription();  //标题
            this.label_message.Text = message;              //内容
            this.btn_cancel.Visible = false;                //隐藏取消按钮
            this.Text = this.label_title.Text;              //将标题与label_title绑定

            //type
            switch (type)
            {
                case EnumNotifyType.Error:
                    this.label_image.Text = NotifyIconError;
                    LabelImgFont = 60;
                    break;
                case EnumNotifyType.Warning:
                    this.label_image.Text = NotifyIconWarning;
                    LabelImgFont = 58;
                    break;
                case EnumNotifyType.Info:
                    this.label_image.Text = NotifyIconInfo;
                    LabelImgFont = 65;
                    break;
                case EnumNotifyType.Question:
                    this.label_image.Text = NotifyIconQuestion;
                    LabelImgFont = 65;
                    this.btn_cancel.Visible = true;
                    break;
            }
        }

        #region 窗体消息
        private void MessageBoxEx_Load(object sender, EventArgs e)
        {
            this.InitLoad();

            InitIconLabel(label_icon, NotifyIconTitle, 15);
            InitIconLabel(label_image, label_image.Text, LabelImgFont);

            if (!btn_ok.Visible)
            {
                btn_cancel.Left = (this.Width - btn_cancel.Width) / 2;
            }
            else if (!btn_cancel.Visible)
            {
                btn_ok.Left = (this.Width - btn_ok.Width) / 2;
            }
        }

        private void MessageBoxEx_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen p = new Pen(CommonPara.SkinColor, 2);
            g.DrawRectangle(p, this.panel_title.Left, this.panel_title.Top, Width, Height);
        }
        #endregion

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Result = true;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Result = false;
            this.Close();
        }

        #region public static methods
        /// <summary>
        /// 提示错误消息
        /// </summary>
        public static void Error(string mes, Form owner = null)
        {
            Show(EnumNotifyType.Error, mes, owner);
        }

        /// <summary>
        /// 提示普通消息
        /// </summary>
        public static void Info(string mes, Form owner = null)
        {
            Show(EnumNotifyType.Info, mes, owner);
        }

        /// <summary>
        /// 提示警告消息
        /// </summary>
        public static void Warning(string mes, Form owner = null)
        {
            Show(EnumNotifyType.Warning, mes, owner);
        }

        /// <summary>
        /// 提示询问消息
        /// </summary>
        public static bool Question(string mes, Form owner = null)
        {
            return Show(EnumNotifyType.Question, mes, owner);
        }

        private static bool Show(EnumNotifyType type, string mes, Form owner = null)
        {
            var res = true;

            MessageBoxEx mb = new MessageBoxEx(type, mes);
            mb.Owner = owner;
            mb.TopMost = owner == null ? true : owner.TopMost;
            mb.ShowDialog();

            res = mb.Result;
            return res;
        }
        #endregion

        /// <summary>
        /// 通知消息类型
        /// </summary>
        public enum EnumNotifyType
        {
            [Description("错误")]
            Error,
            [Description("警告")]
            Warning,
            [Description("提示信息")]
            Info,
            [Description("询问信息")]
            Question,
        }
    }

    public static class TypeExtension
    {
        private static NameValueCollection nvc = GetEnumDescription(typeof(MessageBoxEx.EnumNotifyType));
        public static string GetDescription(this MessageBoxEx.EnumNotifyType type)
        {
            return nvc.Get(type.ToString());
        }

        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static NameValueCollection GetEnumDescription(Type enumType)
        {
            NameValueCollection nvc = new NameValueCollection();
            Type type = typeof(DescriptionAttribute);

            foreach (FieldInfo fi in enumType.GetFields())
            {
                object[] array = fi.GetCustomAttributes(type, true);
                if (array.Length > 0)
                {
                    nvc.Add(fi.Name, ((DescriptionAttribute)array[0]).Description);
                }
            }

            return nvc;
        }
    }
}
