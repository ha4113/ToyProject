public class InputAddPlayer : IInputEvent
{
    public bool IsMine { get; }
    public int ID { get; }
    public string Name { get; }

    public InputAddPlayer(bool isMine, int id, string name)
    {
        IsMine = isMine;
        ID = id;
        Name = name;
    }
    
    public IEvent[] Execute()
    {
        return new IEvent[]
        {
            new OutputAddPlayer(IsMine, ID, Name)
        };
    }
}