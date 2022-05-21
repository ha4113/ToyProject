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

        [TableInfo(TableCategory.SERVER, typeof(ServerOnly))]
        private class Row : IRow
        {
            public int Id { get; set; }
            public string IconName { get; set; }

            public void Build()
            {
                TableContainer<ServerOnly>.Add(Id, new ServerOnly(IconName));
            }
        }
    }
}