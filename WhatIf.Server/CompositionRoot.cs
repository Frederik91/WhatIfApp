using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WhatIf.Database;
using WhatIf.Server.Services.User;

namespace WhatIf.Server
{
    public static class CompositionRoot
    {
        internal static void Compose(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            DatabaseCompositionRoot.Compose(services);
        }
    }
}
