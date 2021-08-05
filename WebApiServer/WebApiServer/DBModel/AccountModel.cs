public class AccountModel : IDBModel
{
    public long id { get; set; }
    public byte wake_time_type { get; set; }

    public override string ToString()
    {
        return $"Id = {id} / WakeTimeType = {wake_time_type}";
    }
}