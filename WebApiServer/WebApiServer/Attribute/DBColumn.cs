using System;

namespace WebApiServer.Attribute
{
    public class DBColumn : System.Attribute
    {
        public int ColumnNameHash { get; }

        public DBColumn(string columnName)
        {
            ColumnNameHash = columnName.GetHashCode();
        }
    }
}