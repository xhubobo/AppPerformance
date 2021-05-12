using AppPerformance.Common;
using System;
using System.Xml;

namespace AppPerformance.Config
{
    public class Config
    {
        private const int DefaultTimerInterval = 1;
        private const int DefaultUpdateInterval = 10;
        private const int DefaultAxisXSpan = 10;
        private const int DefaultAxisXStepNumber = 10;
        private const string DefaultLabelFormatter = "mm:ss";

        //监测定时器间隔(s)
        public int TimerInterval { get; set; } = DefaultTimerInterval;

        //刷新定时器间隔(s)
        public int UpdateInterval { get; set; } = DefaultUpdateInterval;

        //X坐标轴时间片(minute)
        public int AxisXSpan { get; set; } = DefaultAxisXSpan;

        //X坐标轴显示步进(minute)
        public int AxisXStepNumber { get; set; } = DefaultAxisXStepNumber;

        //X坐标轴显示步进(minute)
        public double AxisXStep { get; private set; }

        //X坐标轴显示格式
        public string LabelFormatter { get; set; } = DefaultLabelFormatter;

        //X坐标轴点个数
        public int AxisXCount { get; private set; }

        private readonly string _xmlFilePath;

        public Config()
        {
            _xmlFilePath = Environment.CurrentDirectory + @"\Config\Config.xml";
        }

        public void LoadConfig()
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(_xmlFilePath);

                var setting = xmlDoc.SelectSingleNode("/Config");
                if (setting == null)
                {
                    return;
                }

                //监测定时器间隔(s)
                var text = setting.SelectSingleNode("TimerInterval")?.InnerText;
                if (!string.IsNullOrEmpty(text))
                {
                    int interval;
                    if (int.TryParse(text, out interval))
                    {
                        TimerInterval = interval;
                    }
                }

                //刷新定时器间隔(s)
                text = setting.SelectSingleNode("UpdateInterval")?.InnerText;
                if (!string.IsNullOrEmpty(text))
                {
                    int interval;
                    if (int.TryParse(text, out interval))
                    {
                        UpdateInterval = interval;
                    }
                }

                //X坐标轴时间片(minute)
                text = setting.SelectSingleNode("AxisXSpan")?.InnerText;
                if (!string.IsNullOrEmpty(text))
                {
                    int span;
                    if (int.TryParse(text, out span))
                    {
                        AxisXSpan = span;
                    }
                }

                //X坐标轴显示步进(minute)
                text = setting.SelectSingleNode("AxisXStepNumber")?.InnerText;
                if (!string.IsNullOrEmpty(text))
                {
                    int stepNumber;
                    if (int.TryParse(text, out stepNumber))
                    {
                        AxisXStepNumber = stepNumber;
                    }
                }

                //X坐标轴显示格式
                text = setting.SelectSingleNode("LabelFormatter")?.InnerText;
                if (!string.IsNullOrEmpty(text))
                {
                    LabelFormatter = text;
                }

                AxisXStep = 1.0 * AxisXSpan / AxisXStepNumber;
                AxisXCount = (int) (1.0f * AxisXSpan * Constants.MINUTE_PER_SECOND / TimerInterval);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SaveConfig()
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(_xmlFilePath);

                var setting = xmlDoc.SelectSingleNode("/Config");

                //监测定时器间隔(s)
                var node = setting?.SelectSingleNode("TimerInterval");
                if (node != null)
                {
                    node.InnerText = TimerInterval.ToString();
                }

                //刷新定时器间隔(s)
                node = setting?.SelectSingleNode("UpdateInterval");
                if (node != null)
                {
                    node.InnerText = UpdateInterval.ToString();
                }

                //X坐标轴时间片(minute)
                node = setting?.SelectSingleNode("AxisXSpan");
                if (node != null)
                {
                    node.InnerText = AxisXSpan.ToString();
                }

                //X坐标轴显示步进(minute)
                node = setting?.SelectSingleNode("AxisXStepNumber");
                if (node != null)
                {
                    node.InnerText = AxisXStepNumber.ToString();
                }

                //X坐标轴显示格式
                node = setting?.SelectSingleNode("LabelFormatter");
                if (node != null)
                {
                    node.InnerText = LabelFormatter;
                }

                xmlDoc.Save(_xmlFilePath);

                AxisXStep = 1.0 * AxisXSpan / AxisXStepNumber;
                AxisXCount = (int) (1.0f * AxisXSpan * Constants.MINUTE_PER_SECOND / TimerInterval);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
