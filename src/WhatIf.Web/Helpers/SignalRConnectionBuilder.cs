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
        private readonly IConfiguration _configuration;

        public SignalRConnectionBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HubConnection Build(string baseUri)
        {
            var uri = new Uri(baseUri + "game");
            var connection = new HubConnectionBuilder()
                .WithUrl(uri)
                .Build();

            return connection;
        }
    }
}
