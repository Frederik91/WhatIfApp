using Blazor.Extensions;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using WhatIf.Shared;
using WhatIf.Shared.Services.Session;
using WhatIf.Shared.Services.User;

namespace WhatIf.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISessionClient, SessionClient>();
            services.AddTransient<IUserClient, UserClient>();
            services.AddTransient<IRestClientWrapper, RestClientWrapper>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
