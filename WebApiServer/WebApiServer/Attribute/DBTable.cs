using WebApiServer.DBProtocol;

namespace WebApiServer.Attribute
{
    public class DBTable : System.Attribute
    {
        public string TableName { get; }
        public DB DBType { get; }

        public DBTable(DB dbType, string tableName)
        {
            DBType = dbType;
            TableName = tableName;
        }
    }
}