using System;

public class OutputSubscribeAttribute : Attribute
{
    public Type OutputType { get; }
    public OutputSubscribeAttribute(Type type)
    {
        OutputType = type;
    }
}
