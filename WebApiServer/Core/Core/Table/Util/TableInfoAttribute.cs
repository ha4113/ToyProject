using System;

namespace Common.Core.Table.Util
{
    [Flags]
    public enum TableReadCategory
    {
        NONE,
        COMMON =  1 << 1,
        CLIENT_ONLY = 2 << 1,
        SERVER_ONLY = 3 << 1,
        CLIENT = COMMON | CLIENT_ONLY,
        SERVER = COMMON | SERVER_ONLY
    }
    public class TableInfoAttribute : Attribute
    {
        public readonly TableReadCategory TableReadCategory;
        public readonly string TableName;
        public TableInfoAttribute(TableReadCategory tableReadCategory, string tableName)
        {
            TableReadCategory = tableReadCategory;
            TableName = tableName;
        }
    }
}