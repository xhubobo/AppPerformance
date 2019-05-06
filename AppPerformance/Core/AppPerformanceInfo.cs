namespace AppPerformance.Core
{
    internal class AppPerformanceInfo
    {
        //系统内存
        public string SystemMemoryInfo { get; set; }

        //CPU使用率
        public double CpuUsage { get; set; }

        //内存(专用工作集)
        public long AppPrivateMemory { get; set; }

        //工作集(内存)
        public long AppWorkingSetMemory { get; set; }

        //线程数
        public int ThreadCount { get; set; }
    }
}
