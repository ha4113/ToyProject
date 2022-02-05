using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerFactory : PlaceholderFactory<long, Task<Player>>
{
    private readonly IPrefabManager _prefabManager;
    private readonly IEventHandler _eventHandler;

    public PlayerFactory(IPrefabManager prefabManager,
                         IEventHandler eventHandler)
    {
        _prefabManager = prefabManager;
        _eventHandler = eventHandler;
    }
    
    public override async Task<Player> Create(long playerId)
    {
        Debug.Log("PlayerCreate");
        
        var player = new Player(playerId);
        
        // await _packetManager.ReqTest();
        //
        //
        // .ReceiveTest.Subscribe(result =>
        // {
        //     Debug.Log(result);
        //     disposable.Dispose();
        // });
        //
        var playerBinder = await _prefabManager.Get<PlayerBinder>("Test");
        player.Init(playerBinder);
        return player;
    }
}