using System;

namespace WebApiServer.Attribute
{
    public class DBColumn : System.Attribute
    {
        public int ColumnNameHash { get; }
        public Type ColumnType { get; }

        public DBColumn(string columnName, Type columnType)
        {
            ColumnNameHash = columnName.GetHashCode();
            ColumnType = columnType;
        }
    }
}