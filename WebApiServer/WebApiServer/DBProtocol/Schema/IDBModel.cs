namespace Server.DBProtocol.Schema
{
    public interface IDBModel
    {
        long Id { get; }
        IDBModel DeepCopy();
    }
}