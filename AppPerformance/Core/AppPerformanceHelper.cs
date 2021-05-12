using AppPerformance.Common;
using System;
using System.Threading;

namespace AppPerformance.Core
{
    internal class AppPerformanceHelper : IDisposable
    {
        public Action<string> ErrorAction { get; set; }
        public Action<AppPerformanceInfo> ShowInfoAction { get; set; }

        //Config
        private Config.Config _config;

        //系统内存
        private readonly SystemInfo _systemInfo = new SystemInfo();

        //APP信息
        private readonly AppInfo _appInfo = new AppInfo();

        #region 工作线程

        private Thread _workThread;

        private bool _isWorking;
        private readonly object _isWorkingLockHelper = new object();

        private bool _isWorkPause;
        private readonly object _isWorkPauseLockHelper = new object();

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

        #endregion

        public bool SetProcessName(string processName, ref string errMsg)
        {
            return _appInfo.SetProcessName(processName, ref errMsg);
        }

        public string GetSysMemInfo()
        {
            //系统内存：6.0/7.9 GB (76%)
            var phyMem = _systemInfo.PhysicalMemory;
            var sysMem = _systemInfo.PhysicalMemory - _systemInfo.MemoryAvailable;
            var vPhyMem = Math.Round(1.0 * phyMem / Constants.GB_DIV, 1, MidpointRounding.AwayFromZero);
            var vSysMem = Math.Round(1.0 * sysMem / Constants.MB_DIV, 1, MidpointRounding.AwayFromZero);
            var vSysPercent = Math.Round(100.0 * sysMem / phyMem, 0);
            var memTotalPhys = $@"{vSysMem:F1}/{vPhyMem:F1} GB ({vSysPercent:N0}%)";
            return memTotalPhys;
        }

        public long GetPhysicalMemory()
        {
            return _systemInfo.PhysicalMemory;
        }

        #region 工作线程

        public void StartWork(Config.Config config)
        {
            _config = config;

            IsWorking = true;
            IsWorkPause = false;
            _workThread = new Thread(DoWork)
            {
                IsBackground = true
            };
            _workThread.Start();
        }

        public void PauseWork(bool pause)
        {
            IsWorkPause = pause;
        }

        public void StopWork()
        {
            IsWorking = false;
            IsWorkPause = false;
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
                if (IsWorkPause)
                {
                    Thread.Sleep(1);
                    continue;
                }

                //CPU使用率
                if (!_appInfo.GetCpuPerformance(ref cpuUsage, ref errMsg))
                {
                    ErrorAction?.Invoke(errMsg);
                    IsWorking = false;
                    continue;
                }

                //APP内存
                long memAppPrivate = 0;
                long memAppWorkingSet = 0;
                if (!_appInfo.GetMemInfo(ref memAppPrivate, ref memAppWorkingSet, ref errMsg))
                {
                    ErrorAction?.Invoke(errMsg);
                    IsWorking = false;
                    continue;
                }

                //线程数
                var threadCount = 0;
                if (!_appInfo.GetThreadCount(ref threadCount, ref errMsg))
                {
                    ErrorAction?.Invoke(errMsg);
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
                ShowInfoAction?.Invoke(appPerformance);

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

        #endregion

        #region IDisposable Support

        private bool _disposedValue; //检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    //释放托管状态(托管对象)。
                }

                //释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                //将大型字段设置为 null。
                StopWork();

                _disposedValue = true;
            }
        }

        ~AppPerformanceHelper()
        {
            Dispose(false);
        }

        // 添加此代码以正确实现可处置模式。
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
