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

        public Task NotifySessionStarting(Guid gameId)
        {
            return Clients.GroupExcept(gameId.ToString(), Context.ConnectionId).SendAsync("SessionStarting");
        }

        public Task NotifySessionStarted(Guid gameId)
        {
            return Clients.GroupExcept(gameId.ToString(), Context.ConnectionId).SendAsync("SessionStarted");
        }

        public Task NotifyCardAmountChanged(Guid gameId, int cardAmount)
        {
            return Clients.GroupExcept(gameId.ToString(), Context.ConnectionId).SendAsync("CardAmountChanged", cardAmount);
        }

        public Task NotifyPlayerSubmittedQuestions(Guid gameId, Guid playerId)
        {
            return Clients.Group(gameId.ToString()).SendAsync("PlayerSubmittedQuestions", playerId);
        }

        public Task NotifyStartAnsweringQuestions(Guid gameId)
        {
            return Clients.Group(gameId.ToString()).SendAsync("StartAnsweringQuestions");
        }
    }
}
