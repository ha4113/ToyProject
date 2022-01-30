using System;

public interface IEventObservable
{
    bool IsDisposed { get; }
}

public interface IEventObservable<out T>
{
    bool IsDisposed { get; }
}

public interface ISubscribable
{
    IDisposable Subscribe(Action action);
}

public interface ISubscribable<out T>
{
    IDisposable Subscribe(Action<T> action);
}