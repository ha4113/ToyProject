public interface IInputEvent : IEvent
{
    IEvent[] Execute();
}