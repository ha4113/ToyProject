using System;
using JetBrains.Annotations;
using UniRx;

public interface IEventCommand : IEventObservable
{
    bool CanExecute();
    bool Execute();
}

public sealed class EventCommand : IEventCommand, ISubscribable, IDisposable
{
    public bool IsDisposed { get; private set; }
    public event Action RaiseEvent;

    private readonly Func<bool> _predicate;

    public EventCommand(Func<bool> predicate = null) { _predicate = predicate ?? Stubs.AlwaysTrue; }

    public void Dispose()
    {
        RaiseEvent = null;
        IsDisposed = true;
    }

    public bool CanExecute() => _predicate();

    public bool Execute()
    {
        if (_predicate())
        {
            OnChangeEvent();
            return true;
        }

        return false;
    }

    private void OnChangeEvent() { RaiseEvent?.Invoke(); }

    IDisposable ISubscribable.Subscribe(Action action)
    {
        if (IsDisposed)
        {
            return Disposable.Empty;
        }

        RaiseEvent += action;
        return new Subscription(this, action);
    }

    private class Subscription : IDisposable
    {
        private readonly EventCommand _parent;
        private readonly Action _action;

        public Subscription(EventCommand parent, Action action)
        {
            _parent = parent;
            _action = action;
        }

        public void Dispose() { _parent.RaiseEvent -= _action; }
    }
}


public interface IEventCommand<T> : IEventObservable<T>
{
    bool CanExecute(T parameter);
    bool Execute(T parameter);
}

public sealed class EventCommand<T> : IEventCommand<T>, ISubscribable<T>, IDisposable
{
    public bool IsDisposed { get; private set; }
    public event Action<T> RaiseEvent;
    public int ListenerCount => RaiseEvent?.GetInvocationList().Length ?? 0;

    private readonly Predicate<T> _predicate;

    public EventCommand(Predicate<T> predicate = null) { _predicate = predicate ?? Stubs<T>.AlwaysTrue; }

    public void Dispose()
    {
        RaiseEvent = null;
        IsDisposed = true;
    }

    public bool CanExecute(T parameter) => _predicate(parameter);

    public bool Execute(T parameter)
    {
        if (_predicate(parameter))
        {
            OnRaiseEvent(parameter);
            return true;
        }

        return false;
    }

    private void OnRaiseEvent(T e) { RaiseEvent?.Invoke(e); }

    [MustUseReturnValue]
    IDisposable ISubscribable<T>.Subscribe(Action<T> action)
    {
        if (IsDisposed)
        {
            return Disposable.Empty;
        }

        RaiseEvent += action;
        return new Subscription(this, action);
    }

    private class Subscription : IDisposable
    {
        private readonly EventCommand<T> _parent;
        private readonly Action<T> _action;

        public Subscription(EventCommand<T> parent, Action<T> action)
        {
            _parent = parent;
            _action = action;
        }

        public void Dispose() { _parent.RaiseEvent -= _action; }
    }
}