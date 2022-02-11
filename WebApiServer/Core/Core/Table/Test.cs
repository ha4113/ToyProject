using Common.Core.Table.Util;

namespace Common.Core.Table
{
    public class DefTest : IDef
    {
        public string Name { get; }
        public DefTest(string name)
        {
            Name = name;
        }
    }

    internal class Test : ITable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public void Build()
        {
            DefMgr<DefTest>.Add(Id, new DefTest(Name));
        }
    }
}