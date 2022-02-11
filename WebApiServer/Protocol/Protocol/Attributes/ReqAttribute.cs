namespace Common.Protocol.Attributes
{
    public class ReqAttribute : System.Attribute
    {
        public string Api { get; }
        public ReqAttribute(string api) { Api = api; }
    }
}