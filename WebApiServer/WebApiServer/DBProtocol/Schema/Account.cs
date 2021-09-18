using WebApiServer.Attribute;

namespace WebApiServer.DBProtocol.Schema
{
    public interface IReadOnlyAccount : IReadOnlyDBSchema
    {
        public byte WakeType { get; }
    }
    [DBTable(DB.MAIN, Table.ACCOUNT)]
    public class Account : IDBSchema, IReadOnlyDBSchema
    {
        [DBColumn("id")]
        public long Id { get; private init; }
        [DBColumn("wake_time_type")]
        public byte WakeType { get; set; }

        public IReadOnlyDBSchema DeepCopy()
        {
            return new Account
            {
                Id = Id, 
                WakeType = WakeType,
            };
        }
    }
}