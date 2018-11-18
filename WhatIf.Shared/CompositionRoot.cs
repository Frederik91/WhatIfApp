using System;
using System.Collections.Generic;
using System.Text;
using LightInject;
using WhatIf.Shared.Services.Session;

namespace WhatIf.Shared
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISessionService, SessionService>();
        }
    }
}
