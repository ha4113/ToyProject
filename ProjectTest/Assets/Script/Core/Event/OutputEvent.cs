public interface IOutputEvent : IEvent
{
    void Execute();
}

public abstract class OutputEvent<T> : IOutputEvent
{
    public static IEventObservable<T> Output => _output;
    protected static readonly EventCommand<T> _output = new();
    public abstract void Execute();
}