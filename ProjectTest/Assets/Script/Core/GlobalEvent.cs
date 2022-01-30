using System;

public class GlobalEvent : IDisposable
{
    public IEventCommand<byte> ReceiveTest => _receiveTest;
    private readonly EventCommand<byte> _receiveTest = new EventCommand<byte>();

    public void Dispose()
    {
        _receiveTest?.Dispose();
    }
}
