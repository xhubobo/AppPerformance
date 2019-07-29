using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppPerformance.Common
{
    public class AutoCapture
    {
        //截图文件夹
        private string mCapPath = string.Empty;

        //相对时间
        private DateTime mTheTime = DateTime.Now;

        public void ResetTime()
        {
            mTheTime = DateTime.Now;
        }

        public void Capture(Form form)
        {
            if (form != null)
            {
                var tmSpan = DateTime.Now - mTheTime;
                if (tmSpan.TotalHours >= 3.0)
                {
                    //3个小时记录一次
                    ResetTime();

                    //保留最小化状态
                    var oldState = form.WindowState;
                    if (oldState == FormWindowState.Minimized)
                    {
                        form.WindowState = FormWindowState.Maximized;
                    }

                    Bitmap bmp = new Bitmap(form.Width, form.Height);
                    var rect = new Rectangle(0, 0, form.Width, form.Height);
                    form.DrawToBitmap(bmp, rect);

                    PrepareFolder();
                    var imgPath = string.Format("{0}\\{1}.jpg", mCapPath, DateTime.Now.ToString("yyyyMMdd_HHmmss"));
                    bmp.Save(imgPath);

                    //恢复最小化状态
                    if (oldState == FormWindowState.Minimized)
                    {
                        form.WindowState = oldState;
                    }
                }
            }
        }

        private void PrepareFolder()
        {
            if (string.IsNullOrEmpty(mCapPath))
            {
                mCapPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Capture");
            }

            //创建文件夹
            if (!Directory.Exists(mCapPath))
            {
                Directory.CreateDirectory(mCapPath);
            }
        }

        #region 单例模式
        private AutoCapture() { }
        private volatile static AutoCapture _instance = null;
        private static readonly object _lockHelper = new object();

        public static AutoCapture Instance()
        {
            if (null == _instance)
            {
                lock (_lockHelper)
                {
                    if (null == _instance)
                    {
                        _instance = new AutoCapture();
                    }
                }
            }
            return _instance;
        }
        #endregion
    }
}
