using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerFactory : PlaceholderFactory<long, Task<Player>>
{
    private readonly IPrefabManager _prefabManager;
    private readonly IPacketManager _packetManager;
    private readonly IGlobalEvent _globalEvent;

    public PlayerFactory(IPrefabManager prefabManager,
                         IPacketManager packetManager,
                         IGlobalEvent globalEvent)
    {
        _prefabManager = prefabManager;
        _packetManager = packetManager;
        _globalEvent = globalEvent;
    }
    
    public override async Task<Player> Create(long playerId)
    {
        Debug.Log("PlayerCreate");
        
        var player = new Player(playerId);
        
        await _packetManager.ReqTest();
        
        IDisposable disposable = null;
        disposable = _globalEvent.ReceiveTest.Subscribe(result =>
        {
            Debug.Log(result);
            disposable.Dispose();
        });
        
        var playerBinder = await _prefabManager.Get<PlayerBinder>("Test");
        player.Init(playerBinder);
        return player;
    }
}