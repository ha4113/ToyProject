public class InputTest : IInputEvent
{
    private readonly int _id;

    public InputTest(int id)
    {
        _id = id;
    }
    
    public IEvent[] Execute()
    {
        return new IEvent[]
        {
            new OutputAddPlayer(_id),
            new OutputTest(_id)
        };
    }
}