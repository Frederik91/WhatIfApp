using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhatIf.Web.Helpers;

namespace WhatIf.Web
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<Core.CompositionRoot>();
            serviceRegistry.RegisterFrom<Database.CompositionRoot>();
            serviceRegistry.Register<ISignalRConnectionBuilder, SignalRConnectionBuilder>(new PerScopeLifetime());
        }
    }
}
