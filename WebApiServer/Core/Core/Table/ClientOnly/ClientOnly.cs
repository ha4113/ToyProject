using Common.Core.Table.Util;

namespace Common.Core.Table
{
    public class ClientOnly : ITable
    {
        public string IconName { get; }

        public ClientOnly(string iconName)
        {
            IconName = iconName;
        }

        public void PostProcess()
        {
        }

        public void Valid() { }
    }
    
    internal class ClientOnlyCsv : ICsv
    {
        public long Id { get; set; }
        public string IconName { get; set; }
        public void Build()
        {
            Table<ClientOnly>.Add(Id, new ClientOnly(IconName));
        }
    }
}