using System;
using Zenject;

public interface IUIView : IInitializable, IDisposable
{
    
}

public class UIView : IUIView
{
    public void Initialize() { }

    public void Dispose() { }
}