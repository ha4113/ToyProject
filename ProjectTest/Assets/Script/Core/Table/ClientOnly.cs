using Common.Core.Table.Util;

namespace Common.Core.Table
{
    public class DefClientOnly : IDef
    {
        public string IconName { get; }

        public DefClientOnly(string iconName)
        {
            IconName = iconName;
        }
    }
    
    internal class ClientOnly : ITable
    {
        public long Id { get; set; }
        public string IconName { get; set; }
        public void Build()
        {
            DefMgr<DefClientOnly>.Add(Id, new DefClientOnly(IconName));
        }
    }
}