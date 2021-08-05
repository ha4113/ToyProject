[DBTable(DB.ACCOUNT, "account")]
public class AccountModel : IDBModel
{
    public enum WakeTimeType : byte
    {
        NONE = 0,
        NINE = 9,
        TEN = 10,
    }
    [DBColumn("id", typeof(long))]
    public long Id { get; set; }
    [DBColumn("wake_time_type", typeof(byte))]
    public WakeTimeType WakeType { get; set; }
}