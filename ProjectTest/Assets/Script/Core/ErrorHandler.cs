using System;
using Common.Protocol.Enums;
using Common.Protocol.Network;
using UniRx;
using UnityEngine;
using Zenject;

public interface IErrorHandler
{
    
}
public class ErrorHandler : IInitializable, IDisposable
{
    private readonly CompositeDisposable _disposable = new();
    private readonly IPacketManager _packetManager;

    public ErrorHandler(IPacketManager packetManager)
    {
        _packetManager = packetManager;
    }
    
    public void Initialize()
    {
        _packetManager.ResponseError
                      .Subscribe(ResponseError)
                      .AddTo(_disposable);
    }

    private void ResponseError(ResponseResult responseResult)
    {
        switch (responseResult)
        {
        case > ResponseResult.WARNING:
            {
                Debug.LogWarning(responseResult);
            }
            break;
        case > ResponseResult.ERROR:
            {
                Debug.LogError(responseResult);
            }
            break;
        case > ResponseResult.CRITICAL:
            {
                throw new NetException(responseResult);
            }
        }
    }
    
    public void Dispose()
    {
        _disposable.Dispose();
    }
}
