public class OutputAddPlayer : OutputEvent<OutputAddPlayer>
{
    public bool IsMine { get; }
    public int ID { get; }
    public string Name { get; }

    public OutputAddPlayer(bool isMine, int id, string name)
    {
        IsMine = isMine;
        ID = id;
        Name = name;
    }
}