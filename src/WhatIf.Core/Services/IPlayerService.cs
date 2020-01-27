﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WhatIf.Core.Models;

namespace WhatIf.Core.Services
{
    public interface IPlayerService
    {
        Task<PlayerDto> Create(string name, Guid sessionId);
        Task<PlayerDto> Get(Guid playerId);
    }
}