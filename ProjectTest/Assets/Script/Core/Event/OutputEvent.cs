using System;
using System.Reflection;
using UniRx;

public interface IOutputEvent : IEvent
{
    void Execute();
}

public abstract class OutputEvent<T> : IOutputEvent
    where T : OutputEvent<T>
{
    private static readonly EventCommand<T> _output = new();

    public virtual void Execute()
    {
        _output.Execute((T)this);
    }
    
    public static void Subscribe(IOutputEventSubscriber subscriber, MethodInfo action, CompositeDisposable disposable)
    {
        _output.Subscribe(e => action.Invoke(subscriber, new object[]{e})).AddTo(disposable);
    }
}