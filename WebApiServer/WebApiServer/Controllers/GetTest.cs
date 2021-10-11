using System.Threading.Tasks;
using Common.Protocol.Network;
using WebApiServer.DBProtocol;
using WebApiServer.DBProtocol.Schema;
using WebApiServer.Utility;

namespace WebApiServer.Controllers
{
    public class GetTest : RouteAction<GetTestReq,GetTestAck>
    {
        protected override async Task<GetTestAck> Run(GetTestReq req, User user)
        {
            var account = await user.GetModel<Account>();
            return new GetTestAck(ResponseResult.Success, account.WakeType);
        }
    }
}
