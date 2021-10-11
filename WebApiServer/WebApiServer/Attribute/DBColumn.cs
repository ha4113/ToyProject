using System;

namespace WebApiServer.Attribute
{
    public enum ColumnType
    {
        NONE,
        KEY,
        PRIMARY_KEY,
    }
    public class DBColumn : System.Attribute
    {
        public ColumnType ColumnType { get; }
        public int ColumnNameHash { get; }

        public DBColumn(string columnName, ColumnType columnType = ColumnType.NONE)
        {
            ColumnType = columnType;
            ColumnNameHash = columnName.GetHashCode();
        }
    }
}