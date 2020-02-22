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
        private string _baseUri;


        public SignalRConnectionBuilder(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public async Task<HubConnection> Build()
        {
            while (string.IsNullOrWhiteSpace(_baseUri))
            {
                try
                {
                    _baseUri = _navigationManager.BaseUri;
                }
                catch
                {
                    await Task.Delay(100);
                }
            }

            var uri = new Uri(_baseUri + "gamehub");
            var connection = new HubConnectionBuilder()
                .WithUrl(uri)
                .Build();

            return connection;
        }
    }
}
