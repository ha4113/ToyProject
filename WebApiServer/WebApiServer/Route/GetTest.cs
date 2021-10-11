using System.Threading.Tasks;
using Common.Protocol.Enums;
using Common.Protocol.Network;
using WebApiServer.DBProtocol;
using WebApiServer.DBProtocol.Schema;

namespace WebApiServer.Route
{
    public class GetTest : RouteAction<GetTestReq,GetTestAck>
    {
        protected override async Task<GetTestAck> Run(GetTestReq req, User user)
        {
            var account = await user.GetModelAsync<Account>();
            return new GetTestAck(ResponseResult.Success, account.WakeType);
        }
    }
}
