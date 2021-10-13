using System.Threading.Tasks;
using Common.Protocol.Enums;
using Common.Protocol.Network;
using Server.DBProtocol;
using Server.DBProtocol.Schema;

namespace Server.NetworkProtocol.Route
{
    public class GetTest : RouteAction<GetTestReq,GetTestAck>
    {
        protected override async Task<GetTestAck> Run(GetTestReq req, User user)
        {
            var account = await user.GetModelAsync<Account>();
            return new GetTestAck(ResponseResult.Success, account.WakeTimeType);
        }
    }
}
