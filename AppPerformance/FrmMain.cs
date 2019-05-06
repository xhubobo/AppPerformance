﻿using AppPerformance.Common;
using AppPerformance.Core;
using AppPerformance.SkinControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AppPerformance
{
    public partial class FrmMain : FrmSkinMain
    {
        //Config
        private readonly Config _config = new Config();

        private AppPerformanceHelper _appPerformanceHelper;

        //UI线程的同步上下文
        private readonly SynchronizationContext _syncContext;

        private readonly List<MeasureModel> _cpuValueList = new List<MeasureModel>();
        private readonly List<MeasureModel> _memValueList = new List<MeasureModel>();

        private double _maxMemValue = 0;
        private double _minMemValue;
        private double _totalMemValue;

        public FrmMain()
        {
            //APP启动时间
            CommonPara.AppStartTime = DateTime.Now;

            InitializeComponent();

            _syncContext = SynchronizationContext.Current;

            _appPerformanceHelper = new AppPerformanceHelper();
            _appPerformanceHelper.ErrorAction = OnErrorAction;
            _appPerformanceHelper.ShowInfoAction = OnShowInfoAction;
        }

        #region 窗体消息
        private void FrmMain_Load(object sender, EventArgs e)
        {
            TopMost = false;
            textBox_app_name.Focus();

            //初始化按钮
            btn_pause.Text = @"暂停";
            btn_pause.Tag = true;
            btn_pause.Enabled = false;
            btn_stop.Enabled = false;

            //获取系统内存信息
            //label_sys_mem.Text = _appPerformanceHelper.GetSysMemInfo();
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

            _config.LoadConfig();

            InitCharts();
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
            (_appPerformanceHelper as IDisposable)?.Dispose();
            Dispose();
            Environment.Exit(0);
        }
        #endregion

        #region 初始化

        private void InitCharts()
        {
            var now = DateTime.Now;

            //CPU
            var cpuSeries = chart_cpu.Series[0];
            cpuSeries.ChartType = SeriesChartType.Area;
            cpuSeries.BorderWidth = 1;
            cpuSeries.BorderColor = Color.Blue;
            cpuSeries.XValueType = ChartValueType.DateTime;
            cpuSeries.YValueType = ChartValueType.Single;
            cpuSeries.LegendText = "CPU";
            cpuSeries.Points.AddXY(-1, 0);

            var cpuArea = chart_cpu.ChartAreas[0];
            cpuArea.AxisX.LabelStyle.Format = "HH:mm";
            cpuArea.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Minutes;
            cpuArea.AxisX.Minimum = now.ToOADate();
            cpuArea.AxisX.Maximum = now.AddMinutes(_config.AxisXSpan).ToOADate();
            cpuArea.AxisY.LabelStyle.Format = "####";
            cpuArea.AxisY.Minimum = 0d;
            cpuArea.AxisY.Maximum = 100d;

            //Memory
            var memSeries = chart_mem.Series[0];
            memSeries.ChartType = SeriesChartType.Area;
            memSeries.BorderWidth = 1;
            memSeries.BorderColor = Color.Blue;
            memSeries.XValueType = ChartValueType.DateTime;
            memSeries.YValueType = ChartValueType.Single;
            memSeries.LegendText = "Memory(MB)";
            memSeries.Points.AddXY(-1, 0);

            var memArea = chart_mem.ChartAreas[0];
            memArea.AxisX.LabelStyle.Format = "HH:mm";
            memArea.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Minutes;
            memArea.AxisX.Minimum = now.ToOADate();
            memArea.AxisX.Maximum = now.AddMinutes(_config.AxisXSpan).ToOADate();
            memArea.AxisY.LabelStyle.Format = "####";
            memArea.AxisY.Minimum = 0d;
            memArea.AxisY.Maximum = _maxMemValue;

            var phyMem = _appPerformanceHelper.GetPhysicalMemory();
            _totalMemValue = Math.Round(1.0 * phyMem / Constants.MB_DIV, 1, MidpointRounding.AwayFromZero);

            _maxMemValue = 0;
            _minMemValue = _totalMemValue;
        }

        #endregion

        #region 工作线程
        private void OnErrorAction(string errMsg)
        {
            _syncContext.Post(DealwithErrorSafePost, errMsg);
        }

        private void OnShowInfoAction(AppPerformanceInfo appPerformanceInfo)
        {
            _syncContext.Post(ShowInfoSafePost, appPerformanceInfo);
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
            ShowCharts(appPerformance.CpuUsage, memValue);
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

            var cpuSeries = chart_cpu.Series[0];
            cpuSeries.Points.AddXY(now.ToOADate(), cpuValue);

            var memArea = chart_mem.ChartAreas[0];
            var vPhyMem = Math.Round(1.0 * memValue / Constants.MB_DIV, 1, MidpointRounding.AwayFromZero);

            var maxValue = vPhyMem + 10;
            if (_maxMemValue <= _totalMemValue && maxValue < _totalMemValue && _maxMemValue < maxValue)
            {
                _maxMemValue = maxValue;                
                memArea.AxisY.Maximum = _maxMemValue;
            }

            var minValue = vPhyMem - 10;
            if (_minMemValue > minValue)
            {
                _minMemValue = minValue;
                memArea.AxisY.Minimum = _minMemValue;
            }

            var memSeries = chart_mem.Series[0];
            memSeries.Points.AddXY(now.ToOADate(), vPhyMem);

            //             _cpuValueList.Add(new MeasureModel
            //             {
            //                 DateTime = now,
            //                 Value = cpuValue
            //             });
            //             _memValueList.Add(new MeasureModel
            //             {
            //                 DateTime = now,
            //                 Value = 1.0 * memValue / Constants.MB_DIV
            //             });
        }
        #endregion

        #region 按钮响应
        private void btn_start_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            if (!_appPerformanceHelper.SetProcessName(this.textBox_app_name.Text.Trim(), ref errMsg))
            {
                MessageBoxEx.Warning(errMsg);
                return;
            }            

            btn_start.Enabled = false;
            btn_pause.Enabled = true;
            btn_stop.Enabled = true;
            btn_pause.Text = "暂停";

            _config.LoadConfig();
            _appPerformanceHelper.StartWork(_config);
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            bool? pause = btn_pause.Tag as bool?;
            if (pause == true)
            {
                //暂停
                btn_pause.Tag = false;
                btn_pause.Text = "继续";
                _appPerformanceHelper.PauseWork(true);
            }
            else
            {
                //继续
                btn_pause.Tag = true;
                btn_pause.Text = "暂停";
                _appPerformanceHelper.PauseWork(false);
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            _appPerformanceHelper.StopWork();

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
                    InitCharts();
                }
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
