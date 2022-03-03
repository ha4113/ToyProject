using Common.Core.Table.Util;

namespace Common.Core.Table
{
    public class Test : ITable
    {
        public string Name { get; }
        public Test(string name)
        {
            Name = name;
        }

        public void PostProcess()
        {
        }

        public void Valid() 
        {
        }
    }

    [TableInfo(TableReadCategory.COMMON, nameof(Test))]
    internal class TestCsv : ICsv
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Build()
        {
            Table<Test>.Add(Id, new Test(Name));
        }
    }
}