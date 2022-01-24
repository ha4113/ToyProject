using Common.Core.Util;
using Server.Enums;

namespace Server.Attribute
{
    public class DBTableAttribute : System.Attribute
    {
        public DB DBType { get; }
        public string TableName { get; }

        public DBTableAttribute(DB dbType, Table table)
        {
            DBType = dbType;
            TableName = table.ToStringValue();
        }
    }
}