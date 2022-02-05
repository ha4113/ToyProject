public class IdViewEvent : OutputEvent<IdViewEvent>
{
    public int ID { get; }

    public IdViewEvent(int id)
    {
        ID = id;
    }
    
    public override void Execute()
    {
        _output.Execute(this);
    }
}