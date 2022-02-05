public class StatusButtonEvent : IInputEvent
{
    private readonly int _id;
    private readonly string _name;

    public StatusButtonEvent(int id, string name)
    {
        _id = id;
        _name = name;
    }
    
    public IEvent[] Execute()
    {
        return new IEvent[]
        {
            new IdViewEvent(_id),
            new NameViewEvent(_name)
        };
    }
}