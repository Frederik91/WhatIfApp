using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace WhatIf.Web.Helpers
{
    public class SignalRConnectionBuilder : ISignalRConnectionBuilder
    {
        private readonly NavigationManager _navigationManager;

        public SignalRConnectionBuilder(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public HubConnection Build()
        {
            var uri = new Uri(_navigationManager.BaseUri + "game");
            var connection = new HubConnectionBuilder()
                .WithUrl(uri)
                .Build();

            return connection;
        }
    }
}
