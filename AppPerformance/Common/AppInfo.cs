using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AppPerformance
{
    public class AppInfo
    {
        //定义CPU的信息结构  
        [StructLayout(LayoutKind.Sequential)]
        public struct CPU_INFO
        {
            public uint dwOemId;
            public uint dwPageSize;
            public uint lpMinimumApplicationAddress;
            public uint lpMaximumApplicationAddress;
            public uint dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public uint dwProcessorLevel;
            public uint dwProcessorRevision;
        }
        //定义内存的信息结构  
        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_INFO
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }
        //定义系统时间的信息结构  
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME_INFO
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }
        [DllImport("kernel32")]
        public static extern void GetSystemDirectory(StringBuilder SysDir, int count);
        [DllImport("kernel32")]
        public static extern void GetSystemInfo(ref  CPU_INFO cpuinfo);
        [DllImport("kernel32")]
        public static extern void GlobalMemoryStatus(ref  MEMORY_INFO meminfo);
        [DllImport("kernel32")]
        public static extern void GetSystemTime(ref  SYSTEMTIME_INFO stinfo);

        //CPU使用信息
        private PerformanceCounter mTotalPcCpu = null;

        //进程名称
        private string mProcessName = string.Empty;

        /// <summary>
        /// 设置进程名称
        /// </summary>
        /// <param name="processName">进程名称</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>true成功，false失败</returns>
        public bool SetProcessName(string processName, ref string errMsg)
        {
            bool ret = false;
            try
            {
                //获取性能计数器
                mTotalPcCpu = new PerformanceCounter("Process", "% Processor Time", processName);

                //获取进程
                var processes = Process.GetProcessesByName(processName);
                if (processes == null || processes.GetLength(0) == 0)
                {
                    errMsg = @"进程名称无效";
                }
                else
                {
                    mProcessName = processName;
                    ret = true;
                }
            }
            catch (System.Exception ex)
            {
                errMsg = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        /// <param name="value">CPU使用率</param>
        /// <param name="errMsg">错误信息</param>
        /// <returns>true成功，false失败</returns>
        public bool GetCpuPerformance(ref double value, ref string errMsg)
        {
            bool ret = false;
            if (mTotalPcCpu != null)
            {
                try
                {
                    value = Math.Round(mTotalPcCpu.NextValue() / Environment.ProcessorCount, 1, MidpointRounding.AwayFromZero);
                    ret = true;
                }
                catch (System.Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            else
            {
                errMsg = @"进程名称无效";
            }
            return ret;
        }

        public bool GetMemInfo(ref long memAppPrivate, ref long memAppWorkingSet, ref string errMsg)
        {
            bool ret = false;

            //获取进程
            var processes = Process.GetProcessesByName(mProcessName);
            if (processes != null && processes.GetLength(0) > 0)
            {
                try
                {
                    if (Environment.OSVersion.Version.Major >= 6)
                    {
                        using (var process = processes[0])
                        using (var p1 = new PerformanceCounter("Process", "Working Set - Private", mProcessName))
                        using (var p2 = new PerformanceCounter("Process", "Working Set", mProcessName))
                        {
                            //                         //注意除以CPU数量
                            //                         Console.WriteLine("{0}{1:N} KB", "工作集（进程类）", process.WorkingSet64 / 1024);
                            //                         Console.WriteLine("{0}{1:N} KB", "工作集 ", process.WorkingSet64 / 1024);
                            //                         Console.WriteLine("{0}{1:N} KB", "私有工作集 ", p1.NextValue() / 1024);
                            //                         Console.WriteLine("{0}；内存（专用工作集）{1:N}；PID:{2}；程序名：{3}", DateTime.Now, p1.NextValue() / 1024, process.Id.ToString(), mProcessName);

                            //APP
                            memAppPrivate = (long)p1.NextValue();
                            memAppWorkingSet = process.WorkingSet64;
                            ret = true;
                        }
                    }
                    else
                    {
                        using (var process = processes[0])
                        {
                            memAppPrivate = process.PrivateMemorySize64;
                            memAppWorkingSet = process.WorkingSet64;
                            ret = true;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            else
            {
                errMsg = @"进程名称无效";
            }

            return ret;
        }

        public bool GetThreadCount(ref int threadCount, ref string errMsg)
        {
            bool ret = false;

            //获取进程
            var processes = Process.GetProcessesByName(mProcessName);
            if (processes != null && processes.GetLength(0) > 0)
            {
                try
                {
                    using (var process = processes[0])
                    {
                        threadCount = process.Threads.Count;
                        ret = true;
                    }
                }
                catch (System.Exception ex)
                {
                    errMsg = ex.Message;
                }
            }
            else
            {
                errMsg = @"进程名称无效";
            }

            return ret;
        }
    }
}
