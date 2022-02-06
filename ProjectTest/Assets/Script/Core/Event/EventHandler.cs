using System.Collections.Generic;
using Zenject;

public interface IEvent { }

public interface IEventHandler
{
    void AddEvent(IEvent e);
    void AddEvent(params IEvent[] events);
}

public class EventHandler : IEventHandler, ITickable
{
    private readonly Queue<IInputEvent> _inputEvents = new();
    private readonly Queue<IOutputEvent> _outputEvents = new();

    
    public void AddEvent(IEvent e)
    {
        if (e == null)
        {
            return;
        }

        switch (e)
        {
        case IInputEvent input:
            {
                _inputEvents.Enqueue(input);
            }
            break;
        case IOutputEvent output:
            {
                _outputEvents.Enqueue(output);
            }
            break;
        }
        
    }
    
    public void AddEvent(params IEvent[] events)
    {
        if (events == null)
        {
            return;
        }
        
        foreach (var e in events)
        {
            AddEvent(e);
        }
    }
    
    public void Tick()
    {
        var inputCount = _inputEvents.Count;
        if (inputCount == 0)
        {
            return;
        }

        for (var i = 0; i < inputCount; ++i)
        {
            var input = _inputEvents.Dequeue();
            Hook(input); // 비 정규화된 동작

            var events = input.Execute(); // 정규화된 동작 수행
            foreach (var e in events)
            {
                // input 일 경우 다음틱에 실행
                // output 일 경우 이번틱에 실행
                AddEvent(e);
            }
        }
        
        var outputCount = _outputEvents.Count;
        if (outputCount == 0)
        {
            return;
        }

        for (var i = 0; i < outputCount; ++i)
        {
            var output = _outputEvents.Dequeue();
            Hook(output); // 비 정규화된 동작 수행
            output.Execute(); // 정규화된 동작 수행
        }
    }

    private void Hook(IEvent e)
    {
        // TODO : 테이블 작성 후 어떻게 분업화할지 고민 필요
        switch (e)
        {
        case InputAddPlayer input:
            {
                // Debug.Log("Input Hooking");
            }   
            break;
        case OutputAddPlayer output:
            {
                // Debug.Log("Output Hooking");
            } 
            break;
        }
    }
}