using System;
using LightInject;
using WhatIf.Core.Helpers;

namespace WhatIf.Core
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISessionIdGenerator, SessionIdGenerator>();
        }
    }
}
