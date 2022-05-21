using Common.Core.Table.Util;
using Common.Core.Util;

namespace Common.Core.Table
{
    public class Test : ITable
    {
        public readonly string Name;
        public Test(string name)
        {
            Name = name;
        }

        public void PostProcess()
        {
            Log.Debug($"PostProcess {GetType().Name} : {Name}");
        }

        public void Valid() 
        {
            Log.Debug($"Valid {GetType().Name} : {Name}");
        }
        
        [TableInfo(TableCategory.COMMON, typeof(Test))]
        private class Row : IRow
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public void Build()
            {
                TableContainer<Test>.Add(Id, new Test(Name));
            }
        }
    }
}