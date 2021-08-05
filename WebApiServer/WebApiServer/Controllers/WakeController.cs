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
        public async void Get()
        {
        }
    }
}
