public class OutputIdView : OutputEvent<OutputIdView>
{
    public int ID { get; }

    public OutputIdView(int id)
    {
        ID = id;
    }
}