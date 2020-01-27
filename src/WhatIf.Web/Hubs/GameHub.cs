using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WhatIf.Web.Hubs
{
    public class GameHub : Hub
    {

        public async Task JoinGame(Guid gameId, Guid playerId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString(), CancellationToken.None);
            await Clients.GroupExcept(gameId.ToString(), Context.ConnectionId).SendAsync("PlayerJoined", playerId);
        }

        public async Task LeaveGame(Guid gameId, Guid playerId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString(), CancellationToken.None);
            await Clients.GroupExcept(gameId.ToString(), Context.ConnectionId).SendAsync("PlayerLeft", playerId);
        }
    }
}
