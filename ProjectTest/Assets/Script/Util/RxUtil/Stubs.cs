using System;

public static class Stubs
{
    public static readonly Action Nop = () => { };
    public static readonly Action<Exception> Throw = ex => throw ex;
    public static readonly Func<bool> AlwaysTrue = () => true;
    public static readonly Func<bool> AlwaysFalse = () => false;
}

public static class Stubs<T>
{
    public static readonly Action<T> Ignore = t => { };
    public static readonly Func<T, T> Identity = t => t;
    public static readonly Action<Exception, T> Throw = (ex, _) => throw ex;
    public static readonly Predicate<T> AlwaysTrue = _ => true;
}

public static class Stubs<T1, T2>
{
    public static readonly Action<T1, T2> Ignore = (x, y) => { };
    public static readonly Action<Exception, T1, T2> Throw = (ex, _, __) => throw ex;
}

public static class Stubs<T1, T2, T3>
{
    public static readonly Action<T1, T2, T3> Ignore = (x, y, z) => { };
    public static readonly Action<Exception, T1, T2, T3> Throw = (ex, _, __, ___) => throw ex;
}