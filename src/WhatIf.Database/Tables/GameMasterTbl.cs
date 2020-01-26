using System;
using System.Collections.Generic;
using System.Text;

namespace WhatIf.Database.Tables
{
    public class GameMasterTbl
    {
        public Guid Id { get; set; }

        public Guid SessionId { get; set; }
        public SessionTbl Session { get; set; }


        public Guid PlayerId { get; set; }
        public PlayerTbl Player { get; set; }
    }
}
