using System;

public class DBTable : Attribute
{
    public string TableName { get; }
    public DB DBType { get; }

    public DBTable(DB tableType, string tableName)
    {
        DBType = tableType;
        TableName = tableName;
    }
}