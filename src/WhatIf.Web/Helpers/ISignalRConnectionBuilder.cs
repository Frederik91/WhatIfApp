using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace WhatIf.Web.Helpers
{
    public interface ISignalRConnectionBuilder
    {
        Task<HubConnection> Build();
    }
}