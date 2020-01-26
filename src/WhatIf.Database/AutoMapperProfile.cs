using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using WhatIf.Core.Models;
using WhatIf.Database.Tables;

namespace WhatIf.Database
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PlayerTbl, PlayerDto>().ReverseMap();
            CreateMap<SessionTbl, SessionDto>().ReverseMap();
        }
    }
}
