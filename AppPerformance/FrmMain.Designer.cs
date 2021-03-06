﻿namespace AppPerformance
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.panel_tool = new System.Windows.Forms.Panel();
            this.btn_refresh = new AppPerformance.ButtonEx();
            this.btn_setup = new AppPerformance.ButtonEx();
            this.btn_stop = new AppPerformance.ButtonEx();
            this.btn_pause = new AppPerformance.ButtonEx();
            this.btn_start = new AppPerformance.ButtonEx();
            this.textBox_app_name = new System.Windows.Forms.TextBox();
            this.label_thread_count = new System.Windows.Forms.Label();
            this.label_mem_workingset = new System.Windows.Forms.Label();
            this.label_mem_app = new System.Windows.Forms.Label();
            this.label_sys_mem = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_cpu_usage = new System.Windows.Forms.Label();
            this.label_mem_workingset_show = new System.Windows.Forms.Label();
            this.label_mem_app_show = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_cpu = new System.Windows.Forms.Panel();
            this.panel_memory = new System.Windows.Forms.Panel();
            this.chart_cpu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart_mem = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel_content.SuspendLayout();
            this.panel_tool.SuspendLayout();
            this.panel_cpu.SuspendLayout();
            this.panel_memory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_cpu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_mem)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_content
            // 
            this.panel_content.BackColor = System.Drawing.Color.White;
            this.panel_content.Controls.Add(this.panel_memory);
            this.panel_content.Controls.Add(this.panel_cpu);
            this.panel_content.Controls.Add(this.panel_tool);
            this.panel_content.Location = new System.Drawing.Point(2, 30);
            this.panel_content.Size = new System.Drawing.Size(956, 488);
            // 
            // panel_tool
            // 
            this.panel_tool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.panel_tool.Controls.Add(this.btn_refresh);
            this.panel_tool.Controls.Add(this.btn_setup);
            this.panel_tool.Controls.Add(this.btn_stop);
            this.panel_tool.Controls.Add(this.btn_pause);
            this.panel_tool.Controls.Add(this.btn_start);
            this.panel_tool.Controls.Add(this.textBox_app_name);
            this.panel_tool.Controls.Add(this.label_thread_count);
            this.panel_tool.Controls.Add(this.label_mem_workingset);
            this.panel_tool.Controls.Add(this.label_mem_app);
            this.panel_tool.Controls.Add(this.label_sys_mem);
            this.panel_tool.Controls.Add(this.label4);
            this.panel_tool.Controls.Add(this.label_cpu_usage);
            this.panel_tool.Controls.Add(this.label_mem_workingset_show);
            this.panel_tool.Controls.Add(this.label_mem_app_show);
            this.panel_tool.Controls.Add(this.label5);
            this.panel_tool.Controls.Add(this.label3);
            this.panel_tool.Controls.Add(this.label2);
            this.panel_tool.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_tool.Location = new System.Drawing.Point(0, 0);
            this.panel_tool.Name = "panel_tool";
            this.panel_tool.Size = new System.Drawing.Size(956, 65);
            this.panel_tool.TabIndex = 0;
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh.Location = new System.Drawing.Point(639, 10);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_refresh.TabIndex = 3;
            this.btn_refresh.Text = "刷新";
            this.btn_refresh.UseVisualStyleBackColor = false;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btn_setup
            // 
            this.btn_setup.BackColor = System.Drawing.Color.Transparent;
            this.btn_setup.Location = new System.Drawing.Point(554, 10);
            this.btn_setup.Name = "btn_setup";
            this.btn_setup.Size = new System.Drawing.Size(75, 23);
            this.btn_setup.TabIndex = 2;
            this.btn_setup.Text = "设置";
            this.btn_setup.UseVisualStyleBackColor = false;
            this.btn_setup.Click += new System.EventHandler(this.btn_setup_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.BackColor = System.Drawing.Color.Transparent;
            this.btn_stop.Location = new System.Drawing.Point(469, 10);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 2;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.BackColor = System.Drawing.Color.Transparent;
            this.btn_pause.Location = new System.Drawing.Point(384, 9);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(75, 23);
            this.btn_pause.TabIndex = 2;
            this.btn_pause.Text = "暂停";
            this.btn_pause.UseVisualStyleBackColor = false;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // btn_start
            // 
            this.btn_start.BackColor = System.Drawing.Color.Transparent;
            this.btn_start.Location = new System.Drawing.Point(299, 9);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 2;
            this.btn_start.Text = "开始";
            this.btn_start.UseVisualStyleBackColor = false;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // textBox_app_name
            // 
            this.textBox_app_name.Location = new System.Drawing.Point(85, 10);
            this.textBox_app_name.Name = "textBox_app_name";
            this.textBox_app_name.Size = new System.Drawing.Size(194, 21);
            this.textBox_app_name.TabIndex = 1;
            // 
            // label_thread_count
            // 
            this.label_thread_count.AutoSize = true;
            this.label_thread_count.Location = new System.Drawing.Point(853, 44);
            this.label_thread_count.Name = "label_thread_count";
            this.label_thread_count.Size = new System.Drawing.Size(17, 12);
            this.label_thread_count.TabIndex = 0;
            this.label_thread_count.Text = "12";
            // 
            // label_mem_workingset
            // 
            this.label_mem_workingset.AutoSize = true;
            this.label_mem_workingset.Location = new System.Drawing.Point(697, 44);
            this.label_mem_workingset.Name = "label_mem_workingset";
            this.label_mem_workingset.Size = new System.Drawing.Size(53, 12);
            this.label_mem_workingset.TabIndex = 0;
            this.label_mem_workingset.Text = "128.6 MB";
            // 
            // label_mem_app
            // 
            this.label_mem_app.AutoSize = true;
            this.label_mem_app.Location = new System.Drawing.Point(505, 44);
            this.label_mem_app.Name = "label_mem_app";
            this.label_mem_app.Size = new System.Drawing.Size(53, 12);
            this.label_mem_app.TabIndex = 0;
            this.label_mem_app.Text = "128.6 MB";
            // 
            // label_sys_mem
            // 
            this.label_sys_mem.AutoSize = true;
            this.label_sys_mem.Location = new System.Drawing.Point(241, 44);
            this.label_sys_mem.Name = "label_sys_mem";
            this.label_sys_mem.Size = new System.Drawing.Size(101, 12);
            this.label_sys_mem.TabIndex = 0;
            this.label_sys_mem.Text = "6.0/7.9 GB (76%)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(800, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "线程数：";
            // 
            // label_cpu_usage
            // 
            this.label_cpu_usage.AutoSize = true;
            this.label_cpu_usage.Location = new System.Drawing.Point(91, 44);
            this.label_cpu_usage.Name = "label_cpu_usage";
            this.label_cpu_usage.Size = new System.Drawing.Size(35, 12);
            this.label_cpu_usage.TabIndex = 0;
            this.label_cpu_usage.Text = "90.8%";
            // 
            // label_mem_workingset_show
            // 
            this.label_mem_workingset_show.AutoSize = true;
            this.label_mem_workingset_show.Location = new System.Drawing.Point(608, 44);
            this.label_mem_workingset_show.Name = "label_mem_workingset_show";
            this.label_mem_workingset_show.Size = new System.Drawing.Size(89, 12);
            this.label_mem_workingset_show.TabIndex = 0;
            this.label_mem_workingset_show.Text = "工作集(内存)：";
            // 
            // label_mem_app_show
            // 
            this.label_mem_app_show.AutoSize = true;
            this.label_mem_app_show.Location = new System.Drawing.Point(392, 44);
            this.label_mem_app_show.Name = "label_mem_app_show";
            this.label_mem_app_show.Size = new System.Drawing.Size(113, 12);
            this.label_mem_app_show.TabIndex = 0;
            this.label_mem_app_show.Text = "内存(专用工作集)：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "系统内存：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "CPU占用率：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "进程名称：";
            // 
            // panel_cpu
            // 
            this.panel_cpu.Controls.Add(this.chart_cpu);
            this.panel_cpu.Location = new System.Drawing.Point(10, 100);
            this.panel_cpu.Name = "panel_cpu";
            this.panel_cpu.Size = new System.Drawing.Size(307, 266);
            this.panel_cpu.TabIndex = 1;
            // 
            // panel_memory
            // 
            this.panel_memory.Controls.Add(this.chart_mem);
            this.panel_memory.Location = new System.Drawing.Point(447, 100);
            this.panel_memory.Name = "panel_memory";
            this.panel_memory.Size = new System.Drawing.Size(267, 249);
            this.panel_memory.TabIndex = 2;
            // 
            // chart_cpu
            // 
            chartArea2.Name = "ChartArea1";
            this.chart_cpu.ChartAreas.Add(chartArea2);
            this.chart_cpu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart_cpu.Legends.Add(legend2);
            this.chart_cpu.Location = new System.Drawing.Point(0, 0);
            this.chart_cpu.Name = "chart_cpu";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart_cpu.Series.Add(series2);
            this.chart_cpu.Size = new System.Drawing.Size(307, 266);
            this.chart_cpu.TabIndex = 3;
            this.chart_cpu.Text = "chart1";
            // 
            // chart_mem
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_mem.ChartAreas.Add(chartArea1);
            this.chart_mem.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart_mem.Legends.Add(legend1);
            this.chart_mem.Location = new System.Drawing.Point(0, 0);
            this.chart_mem.Name = "chart_mem";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_mem.Series.Add(series1);
            this.chart_mem.Size = new System.Drawing.Size(267, 249);
            this.chart_mem.TabIndex = 4;
            this.chart_mem.Text = "chart1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 540);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
            this.panel_content.ResumeLayout(false);
            this.panel_tool.ResumeLayout(false);
            this.panel_tool.PerformLayout();
            this.panel_cpu.ResumeLayout(false);
            this.panel_memory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_cpu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart_mem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_tool;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_app_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_cpu_usage;
        private System.Windows.Forms.Label label_sys_mem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_mem_workingset;
        private System.Windows.Forms.Label label_mem_app;
        private System.Windows.Forms.Label label_mem_workingset_show;
        private System.Windows.Forms.Label label_mem_app_show;
        private ButtonEx btn_start;
        private ButtonEx btn_stop;
        private ButtonEx btn_pause;
        private System.Windows.Forms.Panel panel_memory;
        private System.Windows.Forms.Panel panel_cpu;
        private ButtonEx btn_setup;
        private ButtonEx btn_refresh;
        private System.Windows.Forms.Label label_thread_count;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_cpu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_mem;
    }
}