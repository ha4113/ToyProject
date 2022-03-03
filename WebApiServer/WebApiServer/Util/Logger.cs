using System;
using Serilog;

namespace Server.Util
{

    public class Logger : Common.Core.Util.ILogger
    {
        //Log 파일에 기록
        public void Init()
        {
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Information()
                         .WriteTo.Console()
                         .WriteTo.File(@"log.txt",
                                       rollingInterval: RollingInterval.Day,
                                       rollOnFileSizeLimit: true)
                         .CreateLogger();
        }

        public void Info(string log, params object[] arg)
        {
            Log.Information(log, arg);
        }

        public void Debug(string log, params object[] arg)
        {
            Log.Debug(log, arg);
        }

        public void Warning(string log, params object[] arg)
        {
            Log.Warning(log, arg);
        }

        public void Error(string log, params object[] arg)
        {
            Log.Error(log, arg);
        }

        public void Fatal(Exception exception, string log, params object[] arg)
        {
            Log.Fatal(exception, log, arg);
        }
    }
}