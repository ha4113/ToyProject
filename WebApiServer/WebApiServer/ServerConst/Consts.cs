using System;
using System.Collections.Generic;

public static class ServerConst
{
    public const string HOST = "lsydisk.synology.me";
    public const string PORT = "3306";
    public const string DATABASE = "TestDataBase";
    public const string ID = "ha4113";
    public const string PW = "LSYsang230!";

    private static Dictionary<Type, string> _tableNames = new Dictionary<Type, string>
    {
        { typeof(AccountModel), "money" },
        //{ typeof(ItemRowData), "item" },
    };

    public static bool TryGetTableName<T>(out string data) where T : IDBModel
    {
        return _tableNames.TryGetValue(typeof(T), out data);
    }
}