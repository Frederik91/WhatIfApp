using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WhatIf.Server.Hubs
{
    public class SessionHub : Hub
    {
        public async Task RegisterUser(Guid sessionId, Guid userId)
        {
            await Clients.Group(sessionId.ToString()).SendAsync("UserJoined", userId);
            await Groups.AddToGroupAsync(Context.ConnectionId, sessionId.ToString());
        }
    }
}
