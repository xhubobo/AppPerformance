using AppPerformance.Common;
using AppPerformance.SkinControl;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Threading;
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

        #region 工作线程
        private Thread _workThread;
        private bool _isWorking;
        private readonly object _isWorkingLockHelper = new object();

        private bool _isWorkPause;
        private readonly object _isWorkPauseLockHelper = new object();

        private bool _isShowingUi;
        private readonly object _isShowingUiLockHelper = new object();

        private bool IsWorking
        {
            get
            {
                bool ret;
                lock (_isWorkingLockHelper)
                {
                    ret = _isWorking;
                }
                return ret;
            }
            set
            {
                lock (_isWorkingLockHelper)
                {
                    _isWorking = value;
                }
            }
        }

        private bool IsWorkPause
        {
            get
            {
                bool ret;
                lock (_isWorkPauseLockHelper)
                {
                    ret = _isWorkPause;
                }
                return ret;
            }
            set
            {
                lock (_isWorkPauseLockHelper)
                {
                    _isWorkPause = value;
                }
            }
        }

        private bool IsShowingUi
        {
            get
            {
                bool ret;
                lock (_isShowingUiLockHelper)
                {
                    ret = _isShowingUi;
                }
                return ret;
            }
            set
            {
                lock (_isShowingUiLockHelper)
                {
                    _isShowingUi = value;
                }
            }
        }
        #endregion

        //UI线程的同步上下文
        private SynchronizationContext _syncContext;

        public FrmMain()
        {
            _syncContext = SynchronizationContext.Current;

            //APP启动时间
            CommonPara.AppStartTime = DateTime.Now;

            mChartValuesCpu = new ChartValues<MeasureModel>();
            mChartValuesMem = new ChartValues<MeasureModel>();

            InitializeComponent();

            //初始化按钮
            btn_pause.Text = @"暂停";
            btn_pause.Tag = true;
            btn_pause.Enabled = false;
            btn_stop.Enabled = false;

            //获取系统内存信息
            label_sys_mem.Text = GetSysMemInfo();

            //this.textBox_app_name.Text = @"";
            label_cpu_usage.Text = @"0.0 %";
            label_mem_app.Text = @"0.0 MB";
            label_mem_workingset.Text = @"0.0 MB";
            label_thread_count.Text = "0";

            //XP系统
            if (Environment.OSVersion.Version.Major < 6)
            {
                label_mem_app_show.Text = @"内存使用：";
                label_mem_app.Left = label_mem_app_show.Left + label_mem_app_show.Width;
                label_mem_workingset.Visible = false;
                label_mem_workingset_show.Visible = false;
            }
        }

        #region 窗体消息
        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitChart();
            textBox_app_name.Focus();
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
            StopWork();
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
            InitChart(cartesianChart_mem, mChartValuesMem, "Memory");

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
            cartesianChart.Refresh();
        }

        //设置Chart坐标轴范围
        private void SetAxisLimits(DateTime now)
        {
            //             cartesianChart_cpu.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(0).Ticks; // lets force the axis to be 100ms ahead
            //             cartesianChart_cpu.AxisX[0].MinValue = now.Ticks - TimeSpan.FromMinutes(_config.AxisXSpan).Ticks; //we only care about the last 8 seconds
            // 
            //             cartesianChart_mem.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(0).Ticks; // lets force the axis to be 100ms ahead
            //             cartesianChart_mem.AxisX[0].MinValue = now.Ticks - TimeSpan.FromMinutes(_config.AxisXSpan).Ticks; //we only care about the last 8 seconds

            cartesianChart_cpu.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(_config.AxisXSpan).Ticks;
            cartesianChart_cpu.AxisX[0].MinValue = now.Ticks;

            cartesianChart_mem.AxisX[0].MaxValue = now.Ticks + TimeSpan.FromMinutes(_config.AxisXSpan).Ticks;
            cartesianChart_mem.AxisX[0].MinValue = now.Ticks;
        }
        #endregion

        #region 工作线程
        private void StartWork()
        {
            IsWorking = true;
            IsWorkPause = false;
            IsShowingUi = false;
            _workThread = new Thread(DoWork)
            {
                IsBackground = true
            };
            _workThread.Start();
        }

        private void PauseWork(bool pause)
        {
            IsWorkPause = pause;
        }

        private void StopWork()
        {
            IsWorking = false;
            IsWorkPause = false;
            IsShowingUi = false;
            _workThread?.Join();
            _workThread = null;
        }

        private void DoWork()
        {
            double cpuUsage = 0;
            var errMsg = string.Empty;

            while (IsWorking)
            {
                //暂停
                if (IsWorkPause || IsShowingUi)
                {
                    Thread.Sleep(1);
                    continue;
                }

                //CPU使用率
                if (!mAppInfo.GetCpuPerformance(ref cpuUsage, ref errMsg))
                {
                    _syncContext.Post(DealwithErrorSafePost, errMsg);
                    IsWorking = false;
                    continue;
                }

                //APP内存
                long memAppPrivate = 0;
                long memAppWorkingSet = 0;
                if (!mAppInfo.GetMemInfo(ref memAppPrivate, ref memAppWorkingSet, ref errMsg))
                {
                    _syncContext.Post(DealwithErrorSafePost, errMsg);
                    IsWorking = false;
                    continue;
                }

                //线程数
                var threadCount = 0;
                if (!mAppInfo.GetThreadCount(ref threadCount, ref errMsg))
                {
                    _syncContext.Post(DealwithErrorSafePost, errMsg);
                    IsWorking = false;
                    continue;
                }

                var appPerformance = new AppPerformanceInfo()
                {
                    SystemMemoryInfo = GetSysMemInfo(),
                    CpuUsage = cpuUsage,
                    AppPrivateMemory = memAppPrivate,
                    AppWorkingSetMemory = memAppWorkingSet,
                    ThreadCount = threadCount
                };
                IsShowingUi = true;
                _syncContext.Post(ShowInfoSafePost, appPerformance);

                //等待计时
                var interval = _config.TimerInterval * 1000;
                var count = interval / 100;
                for (var i = 0; i < count; i++)
                {
                    if (!IsWorking)
                    {
                        return;
                    }

                    Thread.Sleep(100);
                }
                if (interval % 100 > 0)
                {
                    Thread.Sleep(interval % 100);
                }
                Thread.Sleep(1);
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

        private void DealwithErrorSafePost(object state)
        {
            var errMsg = state as string;
            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBoxEx.Warning(errMsg);
            }
        }

        private void ShowInfoSafePost(object state)
        {
            var appPerformance = state as AppPerformanceInfo;
            if (appPerformance == null)
            {
                return;
            }

            double memValue = 0;
            ShowLabels(appPerformance, ref memValue);
            //ShowCharts(appPerformance.CpuUsage, memValue);

            IsShowingUi = false;
        }

        private void ShowLabels(AppPerformanceInfo appPerformance, ref double memValue)
        {
            //Labels
            label_cpu_usage.Text = $"{appPerformance.CpuUsage:F1} %";
            label_sys_mem.Text = appPerformance.SystemMemoryInfo;
            label_thread_count.Text = appPerformance.ThreadCount.ToString();

            if (Environment.OSVersion.Version.Major >= 6)
            {
                label_mem_app.Text = string.Format("{0:F1}MB", 1.0 * appPerformance.AppPrivateMemory / Constants.MB_DIV);
                label_mem_workingset.Text = string.Format("{0:F1}MB", 1.0 * appPerformance.AppWorkingSetMemory / Constants.MB_DIV);
                memValue = appPerformance.AppPrivateMemory;
            }
            else
            {
                label_mem_app.Text = string.Format("{0:F1}MB", 1.0 * appPerformance.CpuUsage / Constants.MB_DIV);
                memValue = appPerformance.AppWorkingSetMemory;
            }
        }

        private void ShowCharts(double cpuValue, double memValue)
        {
            var now = DateTime.Now;
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

            //X坐标轴点个数
            if (mChartValuesCpu.Count > _config.AxisXCount)
            {
                mChartValuesCpu.RemoveAt(0);
            }
            if (mChartValuesMem.Count > _config.AxisXCount)
            {
                mChartValuesMem.RemoveAt(0);

                SetAxisLimits(now);
            }
        } 
        #endregion

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

            btn_start.Enabled = false;
            btn_pause.Enabled = true;
            btn_stop.Enabled = true;
            btn_pause.Text = "暂停";

            StartWork();
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            bool? pause = btn_pause.Tag as bool?;
            if (pause == true)
            {
                //暂停
                btn_pause.Tag = false;
                btn_pause.Text = "继续";
                PauseWork(true);
            }
            else
            {
                //继续
                btn_pause.Tag = true;
                btn_pause.Text = "暂停";
                PauseWork(false);
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            StopWork();
            btn_start.Enabled = true;
            btn_pause.Enabled = false;
            btn_stop.Enabled = false;
            btn_pause.Text = "暂停";
        }

        private void btn_setup_Click(object sender, EventArgs e)
        {
            var setupFrm = new FrmSetup();
            if (setupFrm.ShowDialog() == DialogResult.OK)
            {
                if (btn_start.Enabled)
                {
                    InitChart();
                }
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private class AppPerformanceInfo
        {
            //系统内存
            public string SystemMemoryInfo { get; set; }

            //CPU使用率
            public double CpuUsage { get; set; }

            public long AppPrivateMemory { get; set; }

            public long AppWorkingSetMemory { get; set; }

            //线程数
            public int ThreadCount { get; set; }
        }
    }
}
