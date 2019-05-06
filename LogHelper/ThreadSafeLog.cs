using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace LogHelper
{
    /// <summary>
    /// 企业应用框架的日志类
    /// </summary>
    /// <remarks>
    /// 此日志类提供高性能的日志记录实现.
    /// 当调用Write方法时不会造成线程阻塞,而是立即完成方法调用,因此调用线程不用等待日志写入文件之后才返回。
    /// </remarks>
    internal class ThreadSafeLog : IDisposable
    {
        public LogPathHelper LogPathHelper { get; } = new LogPathHelper();

        private readonly Queue<Msg> _msgQueue = new Queue<Msg>();
        private readonly Semaphore _msgSemaphore = new Semaphore(0, int.MaxValue);

        //日志文件写入流对象
        private StreamWriter _writer;
        private readonly object _writerLockHelper = new object();

        private Thread _threadWorker; //工作线程
        private bool _threadWorking; //日志写入线程的控制标记
        private readonly object _threadWorkingLockHelper = new object();

        private bool IsThreadWorking
        {
            get
            {
                bool ret;
                lock (_threadWorkingLockHelper)
                {
                    ret = _threadWorking;
                }

                return ret;
            }
            set
            {
                lock (_threadWorkingLockHelper)
                {
                    _threadWorking = value;
                }
            }
        }

        /// <summary>
        /// 创建日志对象的新实例，
        /// 采用默认当前程序位置作为日志路径和默认的每日日志文件类型记录日志
        /// </summary>
        public ThreadSafeLog()
        {
            Start();
        }

        private void Start()
        {
            IsThreadWorking = true;
            _threadWorker = new Thread(DoWork)
            {
                IsBackground = true
            };
            _threadWorker.Start();
        }

        private void Stop()
        {
            IsThreadWorking = false;
            if (_threadWorker != null)
            {
                _threadWorker.Join();
                _threadWorker = null;
            }

            ClearMessage();
        }

        #region 消息队列操作

        public void AddMessage(Msg msg)
        {
            lock (_msgQueue)
            {
                _msgQueue.Enqueue(msg);
                _msgSemaphore.Release();
            }
        }

        private bool HasMessage()
        {
            bool ret;
            lock (_msgQueue)
            {
                ret = _msgQueue.Any();
            }

            return ret;
        }

        private Msg PeekMessage()
        {
            Msg msg = null;
            lock (_msgQueue)
            {
                if (_msgQueue.Count > 0)
                {
                    msg = _msgQueue.Peek();
                    _msgQueue.Dequeue();
                }
            }

            return msg;
        }

        private void ClearMessage()
        {
            lock (_msgQueue)
            {
                _msgQueue.Clear();
            }
        }

        #endregion

        /// <summary>
        /// 日志文件写入线程执行的方法
        /// </summary>
        private void DoWork()
        {
            while (IsThreadWorking || HasMessage())
            {
                if (!_msgSemaphore.WaitOne(1))
                {
                    continue;
                }

                DoWriteFile(PeekMessage());
            }

            CloseFile();
        }

        /// <summary>
        /// 写入日志文本到文件的方法
        /// </summary>
        /// <param name="msg">消息对象</param>
        private void DoWriteFile(Msg msg)
        {
            try
            {
                OpenFile();

                //判断文件到期标志，如果当前文件到期则关闭当前文件创建新的日志文件
                if (LogPathHelper.TimeSignFlag)
                {
                    CloseFile();
                    OpenFile();
                }

                WriteFile(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #region 文件操作

        /// <summary>
        /// 打开文件准备写入
        /// </summary>
        private void OpenFile()
        {
            lock (_writerLockHelper)
            {
                if (_writer == null)
                {
                    _writer = new StreamWriter(LogPathHelper.GetFilename(), true, Encoding.UTF8);
                }
            }
        }

        /// <summary>
        /// 关闭打开的日志文件
        /// </summary>
        private void CloseFile()
        {
            lock (_writerLockHelper)
            {
                if (_writer == null)
                {
                    return;
                }

                _writer.Flush();
                _writer.Close();
                _writer.Dispose();
                _writer = null;
            }
        }

        private void WriteFile(Msg msg)
        {
            lock (_writerLockHelper)
            {
                if (_writer == null)
                {
                    return;
                }

#if DEBUG
                Console.WriteLine(msg.ToString());
#endif

                _writer.WriteLine(msg.ToString());
                _writer.Flush();
            }
        }

        #endregion

        #region IDisposable Support

        //检测冗余调用
        private bool _disposedValue;

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
                Stop();
                CloseFile();

                //关闭线程后释放信号量
                _msgSemaphore.Dispose();

                 _disposedValue = true;
            }
        }

        ~ThreadSafeLog()
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
