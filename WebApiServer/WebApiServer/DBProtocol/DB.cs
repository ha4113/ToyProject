using WebApiServer.Attribute;

namespace WebApiServer.DBProtocol
{
    public enum DB
    {
        [StringValue("LsyDataBase")] MAIN,
    }
    public enum Table
    {
        [StringValue("account")] ACCOUNT,
    }
}