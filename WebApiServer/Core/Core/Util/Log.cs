using System;

namespace Common.Core.Util
{
    public interface ILogger
    {
        /// <summary>
        /// Info
        /// Release : O / Debug : O
        /// </summary>
        /// <param name="log"></param>
        /// <param name="arg"></param>
        void Info(string log, params object[] arg);

        /// <summary>
        /// Debug
        /// Release : X / Debug : O
        /// </summary>
        /// <param name="log"></param>
        /// <param name="arg"></param>
        void Debug(string log, params object[] arg);

        /// <summary>
        /// Warning
        /// Server : not throw / Client : not throw
        /// </summary>
        /// <param name="log"></param>
        /// <param name="arg"></param>
        void Warning(string log, params object[] arg);

        /// <summary>
        /// Error
        /// Server : not throw / Client : throw
        /// </summary>
        /// <param name="log"></param>
        /// <param name="arg"></param>
        void Error(string log, params object[] arg);

        /// <summary>
        /// FatalError
        /// Server : throw / Client : throw
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="log"></param>
        /// <param name="arg"></param>
        void Fatal(Exception exception, string log, params object[] arg);
    }
    public static class Log
    {
        private static ILogger _logger;
        
        public static void InitLog(ILogger logger)
        {
            _logger = logger;
        }

        public static void Info(string log, params object[] arg) { _logger.Info(log, arg); }
        public static void Debug(string log, params object[] arg) { _logger.Debug(log, arg); }
        public static void Warning(string log, params object[] arg) { _logger.Warning(log, arg); }
        public static void Error(string log, params object[] arg) { _logger.Error(log, arg); }
        public static void Fatal(Exception exception, string log, params object[] arg) { _logger.Fatal(exception, log, arg); }
    }
}