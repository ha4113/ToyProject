using System.Collections.Generic;

public static class ServerConst
{
    private static readonly Dictionary<DB, string> _dbConfigList = new Dictionary<DB, string>();

    public static string GetConfig(this DB dbType)
    {
        _dbConfigList.TryGetValue(dbType, out var str);
        return str;
    }
    
    public static void RegistDBConfig(DB dbType, string dbConfig)
    {
        _dbConfigList.Add(dbType, dbConfig);
    }
}