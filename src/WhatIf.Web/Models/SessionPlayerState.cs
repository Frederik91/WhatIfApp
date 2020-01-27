using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatIf.Web.Models
{
    public class SessionPlayerState
    {
        public Guid PlayerId { get; set; }
        public Guid SessionId { get; set; }
        public bool IsGameMaster { get; set; }
    }
}
