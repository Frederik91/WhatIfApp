using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WhatIf.Server.Services.Session;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpGet("{joinId}")]
        public async Task<IActionResult> GetByJoinId(int joinId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var session = await _sessionService.Get(joinId);
            return Ok(session);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSessionRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var session = await _sessionService.CreateNew(request);
            return Ok(session);
        }

        [HttpPut("Leader/")]
        public async Task<IActionResult> SetLeader([FromBody] SetLeaderRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _sessionService.SetLeader(request);
            return Ok();
        }
    }
}