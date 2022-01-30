using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlayerConroller
{
    public IReadOnlyDictionary<long, IPlayer> Players { get; }
    Task AddPlayer(long userId);
}
public class PlayerController : IPlayerConroller
{
    public IReadOnlyDictionary<long, IPlayer> Players => _players;
    
    private readonly Dictionary<long, IPlayer> _players = new Dictionary<long, IPlayer>();
    private readonly PlayerFactory _playerFactory;
    
    public PlayerController(PlayerFactory playerFactory)
    {
        _playerFactory = playerFactory;
    }
    
    public async Task AddPlayer(long playerId)
    {
        if (false == _players.ContainsKey(playerId))
        {
            var player = await _playerFactory.Create(playerId);
            _players.Add(playerId, player);
        }
    }
}
