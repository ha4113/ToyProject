using System;
using UniRx;

public static class IEventObservableExtension
{
    public static IDisposable Subscribe(this IEventObservable eventObservable, Action action)
    {
        var subscribable = eventObservable as ISubscribable;
        if (subscribable == null)
        {
            return Disposable.Empty;
        }

        return subscribable.Subscribe(action);
    }

    public static IDisposable Subscribe<T>(this IEventObservable<T> eventObservable, Action<T> action)
    {
        var subscribable = eventObservable as ISubscribable<T>;
        if (subscribable == null)
        {
            return Disposable.Empty;
        }

        return subscribable.Subscribe(action);
    }

    public static IDisposable Subscribe(this IEventCommand eventCommand, Action action)
    {
        var subscribable = eventCommand as ISubscribable;
        if (subscribable == null)
        {
            return Disposable.Empty;
        }

        return subscribable.Subscribe(action);
    }

    public static IDisposable Subscribe<T>(this IEventCommand<T> eventCommand, Action<T> action)
    {
        var subscribable = eventCommand as ISubscribable<T>;
        if (subscribable == null)
        {
            return Disposable.Empty;
        }

        return subscribable.Subscribe(action);
    }
}