using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using WhatIf.Database;
using WhatIf.Server.Services.Session;
using WhatIf.Server.Services.User;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Server
{
    public class CompositionRoot
    {
        public static void Compose(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IJoinIdGenerator, JoinIdGenerator>();
            DatabaseCompositionRoot.Compose(services);
        }
    }
}
