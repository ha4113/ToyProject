using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<AccountModel.WakeTimeType> GetWakeType(long id)
        {
            using (var dbConn = await DBConnection.Connect(_logger, DB.ACCOUNT.GetConfig()))
            {
                var accountModel = await dbConn.GetData<AccountModel>(id);
                return accountModel.WakeType;
            }
        }
    }
}
