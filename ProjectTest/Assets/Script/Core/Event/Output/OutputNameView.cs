public class OutputNameView : OutputEvent<OutputNameView>
{
    public string Name { get; }

    public OutputNameView(string name)
    {
        Name = name;
    }
}