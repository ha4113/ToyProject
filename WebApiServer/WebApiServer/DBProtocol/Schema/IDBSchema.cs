namespace WebApiServer.DBProtocol
{
    public interface IReadOnlyDBSchema { }
    public interface IDBSchema
    {
        long Id { get; }
        IReadOnlyDBSchema DeepCopy();
    }
}