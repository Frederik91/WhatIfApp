using System;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using WhatIf.Shared;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddSingleton<IJoinIdGenerator, JoinIdGenerator>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddTransient<IUserUsecase, UserUsecase>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
