using Server.Attribute;
using Server.Enums;

namespace Server.DBProtocol.Schema
{
    [DBTable(DB.MAIN, Table.ACCOUNT)]
    public class Account : IDBModel
    {
        [DBColumn("id", ColumnType.PRIMARY_KEY)]
        public long Id { get; private init; }
        
        [DBColumn("wake_time_type")] 
        public byte WakeTimeType { get; private init; }
        
        [DBColumn("work_type")] 
        public bool WakeType { get; private init; }

        public IDBModel DeepCopy()
        {
            return new Account
            {
                Id = Id, 
                WakeTimeType = WakeTimeType,
                WakeType = WakeType,
            };
        }
    }
}