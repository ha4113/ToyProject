using System;
using Common.Protocol.Enums;
using Common.Protocol.Network;
using UniRx;
using UnityEngine;
using Zenject;

public class ErrorHandler : IInitializable, IDisposable
{
    private readonly CompositeDisposable _disposable = new CompositeDisposable();
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
        if (responseResult > ResponseResult.WARNING)
        {
            Debug.LogWarning(responseResult);
        }
        else if (responseResult > ResponseResult.ERROR)
        {
            Debug.LogError(responseResult);
        }
        else if (responseResult > ResponseResult.CRITICAL)
        {
            throw new NetException(responseResult);
        }
    }
    
    public void Dispose()
    {
        _disposable.Dispose();
    }
}
