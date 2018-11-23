using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WhatIf.Server.Services.User;
using WhatIf.Shared.Services.User;

namespace WhatIf.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Session/{sessionId}")]
        public async Task<IActionResult> GetUsersInSession(Guid sessionId)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userService.GetUsersInSession(sessionId);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userService.GetUser(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userService.CreateUser(request);
            return Ok(user);
        }
    }
}