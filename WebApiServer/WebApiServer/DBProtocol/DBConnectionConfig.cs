using System.Collections.Generic;
using WebApiServer.Utility;

namespace WebApiServer.DBProtocol
{
    public static class DBConnectionConfig
    {
        private static readonly Dictionary<DB, string> _dbConfigList = new Dictionary<DB, string>();

        public static string GetConfig(this DB dbType)
        {
            return _dbConfigList.TryGetValue(dbType, out var str) ? str : string.Empty;
        }
    
        public static void RegistDBConfig(DB dbType, string dbBase)
        {
            _dbConfigList.Add(dbType, string.Format(dbBase, dbType.GetStringValue()));
        }
    }
}