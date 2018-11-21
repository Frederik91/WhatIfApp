using System;
using System.Net.Http;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;
using WhatIf.Shared;
using WhatIf.Shared.Services.Session;
using WhatIf.Shared.Services.User;

namespace WhatIf.Client
{
    public class Startup
    {
        private readonly string _serverApiBaseUrl = "http://localhost:53562/api/";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISessionUsecase, SessionUsecase>();
            services.AddTransient<IUserUsecase, UserUsecase>();
            services.AddSingleton(new HttpClient {BaseAddress = new Uri(_serverApiBaseUrl)});
            services.AddTransient<IRestClientWrapper, RestClientWrapper>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
