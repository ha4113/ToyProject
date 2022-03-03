using System;
using ILogger = Common.Core.Util.ILogger;

public class Logger : ILogger
{
    public void Info(string log, params object[] arg)
    {
        UnityEngine.Debug.Log($"Log : {string.Format(log, arg)}");
    }

    public void Debug(string log, params object[] arg)
    {
        UnityEngine.Debug.Log($"Debug : {string.Format(log, arg)}");
    }

    public void Warning(string log, params object[] arg)
    {
        UnityEngine.Debug.LogWarning($"Warning : {string.Format(log, arg)}");
    }

    public void Error(string log, params object[] arg)
    {
        UnityEngine.Debug.LogError($"Error : {string.Format(log, arg)}");
    }
    
    public void Fatal(Exception exception, string log, params object[] arg)
    {
        UnityEngine.Debug.LogError($"FatalError : {string.Format(log, arg)}");
        throw exception;
    }
}