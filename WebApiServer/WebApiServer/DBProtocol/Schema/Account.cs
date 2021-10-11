using WebApiServer.Attribute;

namespace WebApiServer.DBProtocol.Schema
{
    [DBTable(DB.MAIN, Table.ACCOUNT)]
    public class Account : IDBModel
    {
        [DBColumn("id", ColumnType.PRIMARY_KEY)]
        public long Id { get; private init; }
        [DBColumn("SubId", ColumnType.KEY)]
        public int SubId { get; private init; }
        [DBColumn("wake_time_type")]
        public byte WakeType { get; set; }

        public IDBModel DeepCopy()
        {
            return new Account
            {
                Id = Id, 
                SubId = SubId, 
                WakeType = WakeType,
            };
        }
    }
}