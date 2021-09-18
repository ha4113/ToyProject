using WebApiServer.DBProtocol;
using WebApiServer.Utility;

namespace WebApiServer.Attribute
{
    public class DBTable : System.Attribute
    {
        public string TableName { get; }
        public DB DBType { get; }
        public Table Table { get; }

        public DBTable(DB dbType, Table table)
        {
            DBType = dbType;
            Table = table;
            TableName = table.GetStringValue();
        }
    }
}