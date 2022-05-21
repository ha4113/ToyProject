using Common.Core.Table.Util;
using Common.Core.Util;

namespace Common.Core.Table
{
    public class ClientOnly : ITable
    {
        public readonly string IconName;

        private ClientOnly(string iconName)
        {
            IconName = iconName;
        }

        void ITable.PostProcess()
        {
            Log.Debug($"PostProcess {nameof(ClientOnly)} : {IconName}");
        }

        void ITable.Valid()
        {
            Log.Debug($"Valid {nameof(ClientOnly)} : {IconName}");
        }

        [TableInfo(TableCategory.CLIENT, typeof(ClientOnly))]
        private class Row : IRow
        {
            public long Id { get; set; }
            public string IconName { get; set; }
            public void Build()
            {
                TableContainer<ClientOnly>.Add(Id, new ClientOnly(IconName));
            }
        }
    }
}