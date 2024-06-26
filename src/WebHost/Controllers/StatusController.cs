﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modm.Engine;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebHost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IDeploymentEngine engine;

        public StatusController(IDeploymentEngine engine)
        {
            this.engine = engine;
        }

        [HttpGet]
        public async Task<EngineInfo> Get()
        {
            return await this.engine.GetInfo();
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<string> GetAsync([FromRoute] string name)
        {
            var modmHome = Environment.GetEnvironmentVariable("MODM_HOME");
            return $"Hello, {modmHome}";
        }
    }
}
