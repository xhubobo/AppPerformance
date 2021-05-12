using AppPerformance.SkinControl;
using System;
using System.Drawing;
using System.Windows.Forms;
using AppPerformance.Common;

namespace AppPerformance
{
    public partial class FrmSetup : SkinForm
    {
        private const string NotifyIconTitle = "\ue600";

        private readonly Config.Config _config;

        public FrmSetup()
        {
            InitializeComponent();
            CommonInit();

            _config = new Config.Config();
            _config.LoadConfig();
        }

        private void CommonInit()
        {
            MoveTarget = panel_title;
            SysButtonClose = pictureBox_close;

            panel_title.BackColor = CommonPara.SkinColor;
            //label_image.ForeColor = CommonPara.SkinColor;
        }

        private void FrmSetup_Load(object sender, EventArgs e)
        {
            //将标题与label_title绑定
            Text = label_title.Text;

            InitLoad();

            InitIconLabel(label_icon, NotifyIconTitle, 13);

            text_timer_interval.Text = _config.TimerInterval.ToString();
            text_update_interval.Text = _config.UpdateInterval.ToString();
            text_axis_span.Text = _config.AxisXSpan.ToString();
            text_axis_step_number.Text = _config.AxisXStepNumber.ToString();
            text_label_formatter.Text = _config.LabelFormatter;
        }

        private void FrmSetup_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var p = new Pen(CommonPara.SkinColor, 2);
            g.DrawRectangle(p, panel_title.Left, panel_title.Top, Width, Height);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            int interval;
            int updateInterval;
            int stepNumber;
            int span;
            string formatter;

            if (!GetData(out interval, out updateInterval,
                out stepNumber, out span, out formatter))
            {
                return;
            }

            if (interval * stepNumber * span == 0)
            {
                MessageBoxEx.Warning("参数不能为0");
                return;
            }

            _config.TimerInterval = interval;
            _config.UpdateInterval = updateInterval;
            _config.AxisXStepNumber = stepNumber;
            _config.AxisXSpan = span;
            _config.LabelFormatter = formatter;

            _config.SaveConfig();

            DialogResult = DialogResult.OK;

            Close();
        }

        private bool GetData(out int interval, out int updateInterval,
            out int stepNumber, out int span, out string formatter)
        {
            interval = 0;
            updateInterval = 0;
            stepNumber = 0;
            span = 0;
            formatter = string.Empty;

            if (!int.TryParse(text_timer_interval.Text, out interval))
            {
                MessageBoxEx.Warning("监测定时器间隔无效");
                return false;
            }

            if (!int.TryParse(text_update_interval.Text, out updateInterval))
            {
                MessageBoxEx.Warning("图表定时器间隔无效");
                return false;
            }

            if (!int.TryParse(text_axis_step_number.Text, out stepNumber))
            {
                MessageBoxEx.Warning("X坐标轴时间长度无效");
                return false;
            }

            if (!int.TryParse(text_axis_span.Text, out span))
            {
                MessageBoxEx.Warning("X坐标轴显示步进个数无效");
                return false;
            }

            formatter = text_label_formatter.Text;
            if (string.IsNullOrEmpty(formatter))
            {
                MessageBoxEx.Warning("请输入X坐标轴时间显示格式");
                return false;
            }

            return true;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
