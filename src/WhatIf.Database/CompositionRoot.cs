using LightInject;
using System;
using AutoMapper;
using CQRS.LightInject;
using Microsoft.EntityFrameworkCore;
using WhatIf.Core.Services;
using WhatIf.Database.Services.Answers;
using WhatIf.Database.Services.Players;
using WhatIf.Database.Services.Questions;
using WhatIf.Database.Services.Sessions;

namespace WhatIf.Database
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            if (!(serviceRegistry is IServiceFactory factory))
                throw new Exception("Impossibru!");

            serviceRegistry.RegisterQueryHandlers()
                .RegisterCommandHandlers();
            serviceRegistry.Register<WhatIfDbContext>(new PerRequestLifeTime());

            serviceRegistry.Register<ISessionService, SessionService>();
            serviceRegistry.Register<IPlayerService, PlayerService>();
            serviceRegistry.Register<IQuestionService, QuestionService>();
            serviceRegistry.Register<IAnswerService, AnswerService>();

            serviceRegistry.RegisterInstance<IMapper>(new Mapper(new MapperConfiguration(x =>
            {
                x.AddProfile(typeof(AutoMapperProfile));
                x.ConstructServicesUsing(t => Create(t, factory));
            })));

            //using var _ = factory.BeginScope();
            //using var context = factory.GetInstance<WhatIfDbContext>();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
            //context.Database.Migrate();
        }


        private static object Create(Type type, IServiceFactory factory)
        {
            using var scope = factory.BeginScope();
            return scope.Create(type);
        }
    }
}
