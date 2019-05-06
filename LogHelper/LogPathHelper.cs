using System;
using System.IO;

namespace LogHelper
{
    internal class LogPathHelper
    {
        //日志文件保存的路径
        private string _logPath = @"./Log/";

        //日志记录的类型
        private LogType _logType = LogType.Daily;

        //日志文件生命周期的时间标记
        private DateTime _timeSign;

        private readonly object _logPathLockHelper = new object();
        private readonly object _logTypeLockHelper = new object();
        private readonly object _timeSignLockHelper = new object();

        public string LogPath
        {
            get
            {
                string logPath;
                lock (_logPathLockHelper)
                {
                    logPath = _logPath;
                }

                return logPath;
            }
            set
            {
                lock (_logPathLockHelper)
                {
                    _logPath = value;
                }

                //重置日志文件生命周期
                TimeSign = DateTime.Now;
            }
        }

        public LogType LogType
        {
            get
            {
                LogType logType;
                lock (_logTypeLockHelper)
                {
                    logType = _logType;
                }

                return logType;
            }
            set
            {
                lock (_logTypeLockHelper)
                {
                    _logType = value;
                }

                //重置日志文件生命周期
                TimeSign = DateTime.Now;
            }
        }

        public DateTime TimeSign
        {
            get
            {
                DateTime timeSign;
                lock (_timeSignLockHelper)
                {
                    timeSign = _timeSign;
                }

                return timeSign;
            }
            private set
            {
                lock (_timeSignLockHelper)
                {
                    _timeSign = value;
                }
            }
        }

        public bool TimeSignFlag => DateTime.Now >= TimeSign;

        /// <summary>
        /// 根据日志类型获取日志文件名
        /// 创建文件到期的时间标记，通过判断文件的到期时间标记将决定是否创建新文件。
        /// </summary>
        /// <returns>日志文件名</returns>
        public string GetFilename()
        {
            //创建文件夹
            if (!Directory.Exists(LogPath))
            {
                Directory.CreateDirectory(LogPath);
            }

            var now = DateTime.Now;
            DateTime timeSign;
            string format;

            switch (LogType)
            {
                case LogType.Daily:
                    timeSign = new DateTime(now.Year, now.Month, now.Day);
                    timeSign = timeSign.AddDays(1);
                    format = "yyyyMMdd'.log'";
                    break;
                case LogType.Weekly:
                    timeSign = new DateTime(now.Year, now.Month, now.Day);
                    timeSign = timeSign.AddDays(7);
                    format = "yyyyMMdd'.log'";
                    break;
                case LogType.Monthly:
                    timeSign = new DateTime(now.Year, now.Month, 1);
                    timeSign = timeSign.AddMonths(1);
                    format = "yyyyMM'.log'";
                    break;
                case LogType.Annually:
                    timeSign = new DateTime(now.Year, 1, 1);
                    timeSign = timeSign.AddYears(1);
                    format = "yyyy'.log'";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            TimeSign = timeSign;
            return LogPath + now.ToString(format);
        }
    }
}
