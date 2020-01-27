using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WhatIf.SignalRServer.Hubs;

namespace WhatIf.SignalRServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSignalR()
                .AddAzureSignalR(
                    "Endpoint=https://teggames.service.signalr.net;AccessKey=RECyDVnujGUquGBs8j3j4Bh5UQ8LNIsVyfAhSS7i5cQ=;Version=1.0;");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseFileServer();
            app.UseAzureSignalR(routes => { routes.MapHub<GameHub>("/game"); });
        }
    }
}
