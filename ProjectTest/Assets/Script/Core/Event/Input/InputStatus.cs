public class InputStatus : IInputEvent
{
    private readonly int _id;
    private readonly string _name;

    public InputStatus(int id, string name)
    {
        _id = id;
        _name = name;
    }
    
    public IEvent[] Execute()
    {
        return new IEvent[]
        {
            new OutputIdView(_id),
            new OutputNameView(_name),
        };
    }
}