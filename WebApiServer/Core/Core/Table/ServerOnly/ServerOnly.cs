using System;
using Common.Core.Table.Util;

namespace Common.Core.Table
{
    public class ServerOnly : ITable
    {
        public string IconName { get; }
        public ServerOnly(string iconName)
        {
            IconName = iconName;
        }

        public void PostProcess()
        {
            Console.WriteLine(IconName);
        }

        public void Valid() {  }
    }

    [TableInfo(TableReadCategory.SERVER_ONLY, nameof(ServerOnly))]
    internal class ServerOnlyCsv : ICsv
    {
        public int Id { get; set; }
        public string IconName { get; set; }

        public void Build()
        {
            Table<ServerOnly>.Add(Id, new ServerOnly(IconName));
        }
    }
}