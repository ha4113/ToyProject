using Server.Enums;

namespace Server.Attribute
{
    public class DBColumnAttribute : System.Attribute
    {
        public ColumnType ColumnType { get; }
        public int ColumnNameHash { get; }

        public DBColumnAttribute(string columnName, ColumnType columnType = ColumnType.NONE)
        {
            ColumnType = columnType;
            ColumnNameHash = columnName.GetHashCode();
        }
    }
}