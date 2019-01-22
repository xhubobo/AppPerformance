using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace AppPerformance
{
    public class SystemInfo
    {
        //windows NT性能计数器组件
        private PerformanceCounter mPcCpuLoad = null;

        //private ComputerInfo m

        #region 构造函数
        /// <summary>
        /// 构造函数，初始化计数器等
        /// </summary>
        public SystemInfo()
        {
            //初始化CPU计数器
            mPcCpuLoad = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            mPcCpuLoad.MachineName = ".";
            mPcCpuLoad.NextValue();

            //CPU个数
            ProcessorCount = Environment.ProcessorCount;

            //获得物理内存
            PhysicalMemory = 0;
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    PhysicalMemory += long.Parse(mo["TotalPhysicalMemory"].ToString());
                }
            }
        }
        #endregion

        /// <summary>
        /// CPU个数
        /// </summary>
        public int ProcessorCount { get; private set; }

        /// <summary>
        /// 物理内存
        /// </summary>
        public long PhysicalMemory { get; private set; }

        /// <summary>
        /// 获取CPU占用率
        /// </summary>
        public double CpuPercent
        {
            get
            {
                var percentage = this.mPcCpuLoad.NextValue();
                return Math.Round(percentage, 2, MidpointRounding.AwayFromZero);
            }
        }

        #region 可用内存
        /// <summary>
        /// 获取可用内存
        /// </summary>
        public long MemoryAvailable
        {
            get
            {
                long availablebytes = 0;
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_PerfRawData_PerfOS_Memory");
                foreach (ManagementObject mo in mos.Get())
                {
                    availablebytes += long.Parse(mo["Availablebytes"].ToString());
                }
//                 ManagementClass mos = new ManagementClass("Win32_OperatingSystem");
//                 foreach (ManagementObject mo in mos.GetInstances())
//                 {
//                     if (mo["FreePhysicalMemory"] != null)
//                     {
//                         availablebytes = 1024 * long.Parse(mo["FreePhysicalMemory"].ToString());
//                     }
//                 }
                return availablebytes;
            }
        }
        #endregion

        #region 结束指定进程
        /// <summary>
        /// 结束指定进程
        /// </summary>
        /// <param name="pid">进程的 Process ID</param>
        public static void EndProcess(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                process.Kill();
            }
            catch {}
        }
        #endregion
    }
}
