using System;
using System.Collections.Generic;
using TestServer;

public static class ServerConst
{
    public const string HOST = "lsydisk.synology.me";
    public const string PORT = "3306";
    public const string DATABASE = "TestDataBase";

    private static Dictionary<Type, string> _tableNames = new Dictionary<Type, string>
    {
        { typeof(MoneyRowData), "money" },
        //{ typeof(ItemRowData), "item" },
    };

    public static bool TryGetTableName<T>(out string data) where T : IRowData
    {
        return _tableNames.TryGetValue(typeof(T), out data);
    }
}