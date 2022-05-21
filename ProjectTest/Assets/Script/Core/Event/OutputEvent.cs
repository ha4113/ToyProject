using System.Reflection;
using UniRx;

public interface IOutputEvent : IEvent
{
    static string SubscribeName => nameof(Subscribe);
    void Execute();
    void Subscribe(IOutputSubscriber subscriber, MethodInfo action);
}

public abstract class OutputEvent<T> : IOutputEvent
    where T : OutputEvent<T>
{
    private static readonly EventCommand<T> _output = new();

    public virtual void Execute()
    {
        _output.Execute((T)this);
    }

    private static void Subscribe(IOutputSubscriber subscriber, MethodInfo action)
    {
        _output.Subscribe(e => action.Invoke(subscriber, new object[] { e }))
               .AddTo(subscriber.OutputDisposable);
    }

    void IOutputEvent.Subscribe(IOutputSubscriber subscriber, MethodInfo action) => Subscribe(subscriber, action);
}