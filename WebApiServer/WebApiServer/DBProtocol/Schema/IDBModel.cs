namespace WebApiServer.DBProtocol
{
    public interface IDBModel
    {
        long Id { get; }
        IDBModel DeepCopy();
    }
}