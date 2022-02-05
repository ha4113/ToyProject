public class NameViewEvent : OutputEvent<NameViewEvent>
{
    public string Name { get; }

    public NameViewEvent(string name)
    {
        Name = name;
    }
    
    public override void Execute()
    {
        _output.Execute(this);
    }
}