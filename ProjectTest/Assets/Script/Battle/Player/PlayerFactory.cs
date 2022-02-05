using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PlayerFactory : PlaceholderFactory<long, Task<Player>>
{
    private readonly IPrefabManager _prefabManager;

    public PlayerFactory(IPrefabManager prefabManager)
    {
        _prefabManager = prefabManager;
    }
    
    public override async Task<Player> Create(long playerId)
    {
        Debug.Log("PlayerCreate");
        
        var player = new Player(playerId);
        var playerBinder = await _prefabManager.Get<PlayerBinder>("Test");
        player.Init(playerBinder);
        return player;
    }
}