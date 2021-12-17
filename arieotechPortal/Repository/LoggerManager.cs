using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILogger = NLog.ILogger;

namespace ArieotechLive.Repository
{
    public class LoggerManager : ILoggerManager
    {
        #region Private Variables
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region  LogDebug
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
        #endregion LogError

        #region
        public void LogError(string message)
        {
            logger.Error(message);
        }
        #endregion

        #region LogInfo
        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        #endregion

        #region LogWarn
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
        #endregion

    }
}
