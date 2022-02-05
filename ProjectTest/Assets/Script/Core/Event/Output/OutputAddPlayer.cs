public class OutputAddPlayer : OutputEvent<OutputAddPlayer>
{
    public int ID { get; }

    public OutputAddPlayer(int id)
    {
        ID = id;
    }
}