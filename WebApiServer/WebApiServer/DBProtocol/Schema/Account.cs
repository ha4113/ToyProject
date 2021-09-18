using WebApiServer.Attribute;

namespace WebApiServer.DBProtocol.Schema
{
    [DBTable(DB.MAIN, "account")]
    public class Account : IDBSchema
    {
        [DBColumn("id", typeof(long))]
        public long Id { get; }
        [DBColumn("wake_time_type", typeof(byte))]
        public byte WakeType { get; set; }
    }
}