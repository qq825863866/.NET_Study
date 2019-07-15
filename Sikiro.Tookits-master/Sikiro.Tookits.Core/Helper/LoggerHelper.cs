﻿using System;
using System.Linq;
using log4net;
using log4net.Appender;
using Sikiro.Tookits.Core.Extension;

namespace Sikiro.Tookits.Core.Helper
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LoggerHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoggerHelper));

        #region 文本日志
        /// <summary>
        /// 文本日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dir"></param>
        public static void WriteToFile(string message, string dir = "")
        {
            WriteToFileSetting(() => Log.Info(message), "info", dir);
        }

        /// <summary>
        /// 文本日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="dir"></param>
        public static void WriteToFile(string message, Exception ex, string dir = "")
        {
            WriteToFileSetting(() => Log.Error(message, ex), "error", dir);
        }

        private static void WriteToFileSetting(Action action, string appendersName, string dir = "")
        {
            var appenders = LogManager.GetRepository("").GetAppenders();
            if (appenders.FirstOrDefault(i => i.Name == appendersName) is RollingFileAppender appender)
            {
                appender.File = (dir.IsNullOrEmpty()
                    ? "log4net/{0:yyyyMMdd}/"
                    : "log4net/{2}/{1}/").Format(DateTime.Now, dir, appendersName);
                appender.ActivateOptions();
                action();
            }
        }
        #endregion
    }
}