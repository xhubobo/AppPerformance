using AppPerformance.Common;
using AppPerformance.SkinControl;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Windows.Forms;

namespace AppPerformance
{
    public partial class FrmMain : FrmSkinMain
    {
        //系统内存
        private SystemInfo mSystemInfo = new SystemInfo();

        //APP信息
        private AppInfo mAppInfo = new AppInfo();

        //Config
        private readonly Config _config = new Config();

        private readonly ChartValues<MeasureModel> mChartValuesCpu = null;
        private readonly ChartValues<MeasureModel> mChartValuesMem = null;
        private Timer mTimer = null;

        public FrmMain()
        {
            //APP启动时间
            CommonPara.AppStartTime = DateTime.Now;

            mChartValuesCpu = new ChartValues<MeasureModel>();
            mChartValuesMem = new ChartValues<MeasureModel>();

            InitializeComponent();

            //初始化按钮
            this.btn_pause.Text = @"暂停";
            this.btn_pause.Tag = true;
            this.btn_pause.Enabled = false;
            this.btn_stop.Enabled = false;

            //获取系统内存信息
            this.label_sys_mem.Text = GetSysMemInfo();

            //this.textBox_app_name.Text = @"";
            this.label_cpu_usage.Text = @"0.0 %";
            this.label_mem_app.Text = @"0.0 MB";
            this.label_mem_workingset.Text = @"0.0 MB";

            //XP系统
            if (Environment.OSVersion.Version.Major < 6)
            {
                this.label_mem_app_show.Text = @"内存使用：";
                this.label_mem_app.Left = this.label_mem_app_show.Left + this.label_mem_app_show.Width;
                label_mem_workingset.Visible = false;
                label_mem_workingset_show.Visible = false;
            }
        }

        #region 窗体消息
        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitChart();

            //The next code simulates data changes every minute
            mTimer = new Timer
            {
                Interval = _config.TimerInterval * 1000
            };
            mTimer.Tick += TimerOnTick;
        }

        private void FrmMain_SizeChanged(object sender, EventArgs e)
        {
            int interval = 5;
            int height = (this.panel_content.Height - this.panel_tool.Height - interval) / 2;

            this.panel_cpu.Width = this.panel_content.Width;
            this.panel_cpu.Height = height;
            this.panel_cpu.Left = 0;
            this.panel_cpu.Top = this.panel_tool.Height;

            this.panel_memory.Width = this.panel_content.Width;
            this.panel_memory.Height = height;
            this.panel_memory.Left = 0;
            this.panel_memory.Top = this.panel_tool.Height + height + interval;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !MessageBoxEx.Question("确定退出软件吗？", this);
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            Environment.Exit(0);
        }
        #endregion

        #region 初始化
        private void InitChart()
        {
            _config.LoadConfig();

            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y

            //lets save the mapper globally.
            Charting.For<MeasureModel>(mapper);

            InitChart(cartesianChart_cpu, mChartValuesCpu, "CPU");
            InitChart(cartesianChart_mem, mChartValuesMem, "内存");

            SetAxisLimits(DateTime.Now);
        }

        private void InitChart(LiveCharts.WinForms.CartesianChart cartesianChart, ChartValues<MeasureModel> chartValues, string title)
        {
            //the ChartValues property will store our values array
            chartValues.Clear();
            cartesianChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = chartValues,
                    PointGeometrySize = 2,
                    StrokeThickness = 2
                }
            };
            cartesianChart.AxisX.Clear();
            cartesianChart.AxisX.Add(new Axis
            {
                DisableAnimations = true,
                LabelFormatter = value => new DateTime((long)value).ToString(_config.LabelFormatter),
                Separator = new Separator
                {
                    Step = TimeSpan.FromMinutes(_config.AxisXStep).Ticks
                }
            });
            cartesianChart.AxisY.Clear();
            cartesianChart.AxisY.Add(new Axis
            {
                LabelFormatter = value => value.ToString("f1"),
                Title = title
            });
        }
        #endregion

        //设置Chart坐标轴范围
        private void SetAxisLimits(DateTime now)
        {
            cartesianChart_cpu.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(0).Ticks; // lets force the axis to be 100ms ahead
            cartesianChart_cpu.AxisX[0].MinValue = now.Ticks - TimeSpan.FromMinutes(_config.AxisXSpan).Ticks; //we only care about the last 8 seconds

            cartesianChart_mem.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(0).Ticks; // lets force the axis to be 100ms ahead
            cartesianChart_mem.AxisX[0].MinValue = now.Ticks - TimeSpan.FromMinutes(_config.AxisXSpan).Ticks; //we only care about the last 8 seconds
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
//             if (this.btn_start.Enabled || (this.btn_pause.Tag as bool?) == false)
//             {
//                 //停止或者暂停状态不执行定时器回调
//                 return;
//             }

            mTimer.Stop();

            double cpuValue = 0;
            double memValue = 0;
            ShowLabels(ref cpuValue, ref memValue);

            var now = System.DateTime.Now;
            mChartValuesCpu.Add(new MeasureModel
            {
                DateTime = now,
                Value = cpuValue
            });
            mChartValuesMem.Add(new MeasureModel
            {
                DateTime = now,
                Value = 1.0 * memValue / Constants.MB_DIV
            });

            SetAxisLimits(now);

            //X坐标轴点个数
            if (mChartValuesCpu.Count > _config.AxisXCount)
            {
                mChartValuesCpu.RemoveAt(0);
            }
            if (mChartValuesMem.Count > _config.AxisXCount)
            {
                mChartValuesMem.RemoveAt(0);
            }

            //截图
            AutoCapture.Instance().Capture(this);

            mTimer.Start();
        }

        private void ShowLabels(ref double cpuValue, ref double memValue)
        {
            //CPU使用率
            double cpuUsage = 0;
            string errMsg = string.Empty;
            if (!mAppInfo.GetCpuPerformance(ref cpuUsage, ref errMsg))
            {
                MessageBoxEx.Warning(errMsg);
                btn_stop_Click(null, null);
                return;
            }

            cpuValue = cpuUsage;

            //系统内存信息
            var memTotalPhys = GetSysMemInfo();

            //APP内存
            long memAppPrivate = 0;
            long memAppWorkingSet = 0;
            if (!mAppInfo.GetMemInfo(ref memAppPrivate, ref memAppWorkingSet, ref errMsg))
            {
                MessageBoxEx.Warning(errMsg);
                btn_stop_Click(null, null);
                return;
            }

            //Labels
            if (Environment.OSVersion.Version.Major >= 6)
            {
                this.label_cpu_usage.Text = string.Format("{0:F1} %", cpuUsage);
                this.label_sys_mem.Text = memTotalPhys;
                this.label_mem_app.Text = string.Format("{0:F1}MB", 1.0 * memAppPrivate / Constants.MB_DIV);
                this.label_mem_workingset.Text = string.Format("{0:F1}MB", 1.0 * memAppWorkingSet / Constants.MB_DIV);
                memValue = memAppPrivate;
            }
            else
            {
                this.label_cpu_usage.Text = string.Format("{0:F1} %", cpuUsage);
                this.label_sys_mem.Text = memTotalPhys;
                this.label_mem_app.Text = string.Format("{0:F1}MB", 1.0 * memAppWorkingSet / Constants.MB_DIV);
                memValue = memAppWorkingSet;
            }
        }

        private string GetSysMemInfo()
        {
            //系统内存：6.0/7.9 GB (76%)
            long phyMem = mSystemInfo.PhysicalMemory;
            long sysMem = mSystemInfo.PhysicalMemory - mSystemInfo.MemoryAvailable;
            var vPhyMem = Math.Round(1.0 * phyMem / Constants.GB_DIV, 1, MidpointRounding.AwayFromZero);
            var vSysMem = Math.Round(1.0 * sysMem / Constants.GB_DIV, 1, MidpointRounding.AwayFromZero);
            var vSysPercent = Math.Round(100.0 * sysMem / phyMem, 0);
            string memTotalPhys = string.Format("{0:F1}/{1:F1} GB ({2:N0}%)", vSysMem, vPhyMem, vSysPercent);
            return memTotalPhys;
        }

        #region 按钮响应
        private void btn_start_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            if (!mAppInfo.SetProcessName(this.textBox_app_name.Text.Trim(), ref errMsg))
            {
                MessageBoxEx.Warning(errMsg);
                return;
            }

            InitChart();

            //显示Label信息
            double cpuValue = 0;
            double memValue = 0;
            ShowLabels(ref cpuValue, ref memValue);

            this.btn_start.Enabled = false;
            this.btn_pause.Enabled = true;
            this.btn_stop.Enabled = true;
            this.btn_pause.Text = "暂停";
            mTimer.Start();

            //重置截图时间
            AutoCapture.Instance().ResetTime();
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            bool? pause = this.btn_pause.Tag as bool?;
            if (pause == true)
            {
                //暂停
                this.btn_pause.Tag = false;
                this.btn_pause.Text = "继续";
                mTimer.Stop();
            }
            else
            {
                //继续
                this.btn_pause.Tag = true;
                this.btn_pause.Text = "暂停";
                mTimer.Start();
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            mTimer.Stop();
            this.btn_start.Enabled = true;
            this.btn_pause.Enabled = false;
            this.btn_stop.Enabled = false;
            this.btn_pause.Text = "暂停";
        }

        private void btn_setup_Click(object sender, EventArgs e)
        {
            var setupFrm = new FrmSetup();
            if(setupFrm.ShowDialog() == DialogResult.OK)
            {
                if(btn_start.Enabled)
                {
                    InitChart();
                }
            }
        }
        #endregion
    }
}
