using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using LightInject;
using WhatIf.Database;

namespace WhatIf.Web
{
    public class CompositionRoot : ICompositionRoot

    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<Core.CompositionRoot>();
            serviceRegistry.RegisterFrom<Database.CompositionRoot>();
        }
    }
}
