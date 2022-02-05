public class OutputTest : OutputEvent<OutputTest>
{
    public int ID { get; }

    public OutputTest(int id)
    {
        ID = id;
    }
}