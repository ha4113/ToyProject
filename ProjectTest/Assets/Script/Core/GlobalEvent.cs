using System;

public interface IGlobalEvent
{
    IEventCommand<byte> ReceiveTest { get; }
}
public class GlobalEvent : IGlobalEvent, IDisposable
{
    public IEventCommand<byte> ReceiveTest => _receiveTest;
    private readonly EventCommand<byte> _receiveTest = new EventCommand<byte>();

    public void Dispose()
    {
        _receiveTest?.Dispose();
    }
}
