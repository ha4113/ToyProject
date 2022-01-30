using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;

public interface IReadOnlyEventProperty<out T> : IEventObservable<T>
{
    T Value { get; }

    //bool HasValue { get; }
}

public interface IEventProperty<T> : IReadOnlyEventProperty<T>
{
    new T Value { get; set; }
    void SetValueAndForceNotify(T value);
    IDisposable Subscribe(Action<T> action, bool actionImmediately);
    IDisposable Subscribe(Action<T, T> action, bool actionImmediately = true);
}

[Serializable]
public sealed class EventProperty<T> : IEventProperty<T>, ISubscribable<T>, IDisposable
{
    public T Value
    {
        get => _value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(_value, value))
            {
                var prev = _value;
                _value = value;
                if (IsDisposed)
                {
                    return;
                }

                OnRaiseEvent(prev, _value);
            }
        }
    }
    //public bool HasValue { get; } // always true?

    public event Action<T> RaiseEvent
    {
        add
        {
            _changeEvent += value;
            OnRaiseEvent(_value, _value);
        }
        remove => _changeEvent -= value;
    }

    public event Action<T, T> RaiseEvent2
    {
        add
        {
            _changeEvent2 += value;
            OnRaiseEvent(_value, _value);
        }
        remove => _changeEvent2 -= value;
    }

    public int ListenerCount => (_changeEvent?.GetInvocationList().Length ?? 0) + (_changeEvent2?.GetInvocationList().Length ?? 0);
    public bool IsDisposed { get; private set; }

    private T _value;
    private event Action<T> _changeEvent;
    private event Action<T, T> _changeEvent2;
    public EventProperty(T value = default) { Value = value; }

    public void Dispose()
    {
        _changeEvent = null;
        _changeEvent2 = null;
        IsDisposed = true;
    }

    private void OnRaiseEvent(T prev, T e)
    {
        _changeEvent?.Invoke(e);
        _changeEvent2?.Invoke(prev, e);
    }

    [MustUseReturnValue]
    IDisposable ISubscribable<T>.Subscribe(Action<T> action) { return Subscribe(action, true); }

    [MustUseReturnValue]
    public IDisposable Subscribe(Action<T> action, bool actionImmediately)
    {
        if (IsDisposed)
        {
            return Disposable.Empty;
        }

        _changeEvent += action;

        if (actionImmediately)
        {
            action(_value);
        }

        return new Subscription(this, action);
    }

    [MustUseReturnValue]
    public IDisposable Subscribe(Action<T, T> action, bool actionImmediately = true)
    {
        if (IsDisposed)
        {
            return Disposable.Empty;
        }

        _changeEvent2 += action;

        if (actionImmediately)
        {
            action(_value, _value);
        }

        return new Subscription2(this, action);
    }

    public void SetValueAndForceNotify(T value)
    {
        var prev = _value;
        _value = value;
        if (IsDisposed)
        {
            return;
        }

        OnRaiseEvent(prev, value);
    }

    private class Subscription : IDisposable
    {
        private readonly EventProperty<T> _parent;
        private readonly Action<T> _action;

        public Subscription(EventProperty<T> parent, Action<T> action)
        {
            _parent = parent;
            _action = action;
        }

        public void Dispose() { _parent.RaiseEvent -= _action; }
    }

    private class Subscription2 : IDisposable
    {
        private readonly EventProperty<T> _parent;
        private readonly Action<T, T> _action;

        public Subscription2(EventProperty<T> parent, Action<T, T> action)
        {
            _parent = parent;
            _action = action;
        }

        public void Dispose() { _parent.RaiseEvent2 -= _action; }
    }
}