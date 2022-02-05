using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UniRx;

public interface IOutputEventSubscriber
{
    CompositeDisposable Disposable { get; }
}

public class OutputEventSubscribe
{
    public OutputEventSubscribe(List<IOutputEventSubscriber> eventSubscribers)
    {
        foreach (var outputEventSubscriber in eventSubscribers)
        {
            foreach (var method in outputEventSubscriber.GetType().GetMethods())
            {
                var attr = method.GetCustomAttribute<OutputSubscribeAttribute>();
                if (attr == null)
                {
                    continue;
                }
                
                var parentType = attr.OutputType.GetParentTypes().First().GetInterfaces();
                if (!parentType.ContainsItem(typeof(IOutputEvent)))
                {
                    continue;
                }

                attr.OutputType
                    .GetParentTypes().First()
                    .GetMethod("Subscribe", BindingFlags.Public | BindingFlags.Static)
                    ?.Invoke(null, new object[] { outputEventSubscriber, method, outputEventSubscriber.Disposable});
            }
        }
    }
}