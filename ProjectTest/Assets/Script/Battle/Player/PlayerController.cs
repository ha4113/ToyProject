using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;

public interface IPlayerConroller
{
    public IReadOnlyDictionary<long, IPlayer> Players { get; }
}
public class PlayerController : IPlayerConroller, IOutputSubscriber
{
    public IReadOnlyDictionary<long, IPlayer> Players => _players;
    public CompositeDisposable OutputDisposable { get; } = new();
    
    private readonly Dictionary<long, IPlayer> _players = new();
    private readonly PlayerFactory _playerFactory;
    private IOutputSubscriber _outputSubscriberImplementation;

    public PlayerController(PlayerFactory playerFactory)
    {
        _playerFactory = playerFactory;
    }

    [OutputSubscribe]
    private async Task AddPlayer(OutputAddPlayer output)
    {
        if (false == _players.ContainsKey(output.ID))
        {
            var player = await _playerFactory.Create(output.ID);
            _players.Add(output.ID, player);
        }
    }

    public void Dispose() { OutputDisposable.Dispose(); }
}
