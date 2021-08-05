using System;

public class DBColumn : Attribute
{
    public int ColumnNameHash { get; }
    public Type ColumnType { get; }

    public DBColumn(string columnName, Type columnType)
    {
        ColumnNameHash = columnName.GetHashCode();
        ColumnType = columnType;
    }
}