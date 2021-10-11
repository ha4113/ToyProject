namespace WebApiServer.Attribute
{
    public class ReqAttribute : System.Attribute
    {
        public string Api { get; set; }
        public ReqAttribute(string api) { Api = api; }
    }
}