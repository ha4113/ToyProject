using Common.Core.Util;
using WebApiServer.DBProtocol;

namespace WebApiServer.Attribute
{
    public class DBTableAttribute : System.Attribute
    {
        public string TableName { get; }
        public DB DBType { get; }
        public Table Table { get; }

        public DBTableAttribute(DB dbType, Table table)
        {
            DBType = dbType;
            Table = table;
            TableName = table.GetStringValue();
        }
    }
}