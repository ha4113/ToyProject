using System;

namespace Common.Core.Table.Util
{
    [Flags]
    public enum TableCategory
    {
        COMMON = 1 << 1,
        CLIENT = 2 << 1,
        SERVER = 3 << 1,
    }
    public class TableInfoAttribute : Attribute
    {
        public readonly TableCategory TableCategory;
        public readonly Type Table;
        public TableInfoAttribute(TableCategory tableCategory, Type table)
        {
            TableCategory = tableCategory;
            Table = table;
        }
    }
}