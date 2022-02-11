using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ModestTree;
using UniRx;
using Zenject;

public interface IOutputSubscriber : IDisposable
{
    CompositeDisposable OutputDisposable { get; }
}

public class OutputSubscribe : IDisposable, IInitializable
{
    private readonly List<IOutputSubscriber> _outputSubscribers;
    private const int MAX_OUTPUT_PARAM_COUNT = 1;
    public OutputSubscribe(List<IOutputSubscriber> outputSubscribers)
    {
        _outputSubscribers = outputSubscribers;
    }

    public void Initialize()
    {
        foreach (var subscriber in _outputSubscribers)
        {
            foreach (var method in subscriber.GetType().GetMethods())
            {
                var attr = method.GetCustomAttribute<OutputSubscribeAttribute>();
                if (attr == null)
                {
                    continue;
                }

                var parameters = method.GetParameters();
                if (parameters.Length != MAX_OUTPUT_PARAM_COUNT)
                {
                    throw new ArgumentException($"To Many Output : {method.Name}");
                }
                
                var output = parameters[0].ParameterType;
                if (false == output.GetInterfaces().Contains(typeof(IOutputEvent)))
                {
                    throw new InvalidDataException($"Is Not OutputEvent : {method.Name}");
                }

                output.GetParentTypes().First()
                      .GetMethod(IOutputEvent.SubscribeName, BindingFlags.NonPublic | BindingFlags.Static)
                      ?.Invoke(null, new object[] { subscriber, method });
            }
        }
    }
    
    public void Dispose()
    {
        foreach (var outputSubscriber in _outputSubscribers)
        {
            outputSubscriber.OutputDisposable.Clear();
        }
    }
}