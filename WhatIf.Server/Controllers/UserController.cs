using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WhatIf.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetUserById(int id)
        {
            return await _userService.GetUser(id);
        }
        [HttpGet]
        public async Task<ActionResult<string>> GetAll()
        {
            return await _userService.GetUser(42);
        }
    }
}