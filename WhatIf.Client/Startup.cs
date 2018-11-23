using Microsoft.AspNet.SignalR.Client;
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
            //var hubConnection = new HubConnection("http://localhost:53562/");
            //hubConnection.Start().GetAwaiter().GetResult();
            //services.Add(new ServiceDescriptor(typeof(HubConnection), hubConnection));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
