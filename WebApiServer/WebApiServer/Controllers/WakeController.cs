using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebApiServer.DBProtocol;
using WebApiServer.DBProtocol.Schema;

namespace WebApiServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WakeController : ControllerBase
    {
        private readonly ILogger<WakeController> _logger;

        public WakeController(ILogger<WakeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<byte> GetWakeType(long id)
        {
            // 필요한 DB의 사본 제공
            // 편집여부 확인
            // 편집되었다면 DB에 기록
            
            var account = await DBConnection.Connect<Account>(_logger, id);
            return account.WakeType;
        }
    }
}
