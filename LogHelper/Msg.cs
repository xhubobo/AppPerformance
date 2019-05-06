using System;

namespace LogHelper
{
    /// <summary>
    /// 日志类型的枚举
    /// </summary>
    /// <remarks>
    /// 日志类型枚举指示日志文件创建的方式，如果日志比较多可考虑每天创建一个日志文件，
    /// 如果日志量比较小可考虑每周、每月或每年创建一个日志文件。
    /// </remarks>
    public enum LogType
    {
        /// <summary>
        /// 此枚举指示每天创建一个新的日志文件
        /// </summary>
        Daily,

        /// <summary>
        /// 此枚举指示每周创建一个新的日志文件
        /// </summary>
        Weekly,

        /// <summary>
        /// 此枚举指示每月创建一个新的日志文件
        /// </summary>
        Monthly,

        /// <summary>
        /// 此枚举指示每年创建一个新的日志文件
        /// </summary>
        Annually
    }

    /// <summary>
    /// 表示一个日志记录的对象
    /// </summary>
    public class Msg
    {
        /// <summary>
        /// 创建新的日志记录实例;日志记录的内容为空,消息类型为MsgType.Unknown,日志时间为当前时间
        /// </summary>
        public Msg()
            : this("", MsgType.Unknown)
        {
        }

        /// <summary>
        /// 创建新的日志记录实例;日志事件为当前时间
        /// </summary>
        /// <param name="text">日志记录的文本内容</param>
        public Msg(string text)
            : this(text, MsgType.Information)
        {
        }

        /// <summary>
        /// 创建新的日志记录实例;日志事件为当前时间
        /// </summary>
        /// <param name="text">日志记录的文本内容</param>
        /// <param name="type">日志记录的消息类型</param>
        public Msg(string text, MsgType type)
            : this(DateTime.Now, text, type)
        {
        }

        /// <summary>
        /// 创建新的日志记录实例;
        /// </summary>
        /// <param name="dt">日志记录的时间</param>
        /// <param name="text">日志记录的文本内容</param>
        /// <param name="type">日志记录的消息类型</param>
        public Msg(DateTime dt, string text, MsgType type)
        {
            Datetime = dt;
            Type = type;
            Text = text;
        }

        /// <summary>
        /// 日志记录的时间
        /// </summary>
        public DateTime Datetime { get; set; }

        /// <summary>
        /// 日志记录的文本内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 日志记录的消息类型
        /// </summary>
        public MsgType Type { get; set; }

        public new string ToString()
        {
            return $"{Datetime}{'\t'}{Type}{'\t'}{Text}{'\n'}";
        }
    }

    /// <summary>
    /// 日志消息类型的枚举
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 指示未知信息类型的日志记录
        /// </summary>
        Unknown,

        /// <summary>
        /// 指示普通信息类型的日志记录
        /// </summary>
        Information,

        /// <summary>
        /// 指示警告信息类型的日志记录
        /// </summary>
        Warning,

        /// <summary>
        /// 指示错误信息类型的日志记录
        /// </summary>
        Error,

        /// <summary>
        /// 指示成功信息类型的日志记录
        /// </summary>
        Success
    }
}
