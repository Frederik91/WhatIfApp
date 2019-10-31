using System;
using LightInject;
using WhatIf.Core.Services.Sessions;

namespace WhatIf.Core
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISessionService, SessionService>();
        }
    }
}
