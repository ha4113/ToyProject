using System;
using Zenject;

public interface IUIView
{
    
}

public class UIView : IUIView, IInitializable, IDisposable
{
    public void Initialize() { }
    public void Dispose() { }
}