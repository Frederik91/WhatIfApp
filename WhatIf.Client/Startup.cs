using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IJoinIdGenerator, JoinIdGenerator>();
            services.AddSingleton<ISessionService, SessionService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
