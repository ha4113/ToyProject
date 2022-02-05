using UnityEngine;

public interface IPlayer
{
    long PlayerId { get; }
}
public class Player : IPlayer
{
    private PlayerBinder _playerBinder;
    public long PlayerId { get; }
    
    public Player(long playerId)
    {
        PlayerId = playerId;
        Debug.Log(playerId);
    }

    public void Init(PlayerBinder playerBinder)
    {
        _playerBinder = playerBinder;
        Debug.Log("Player Init");
    }
}
