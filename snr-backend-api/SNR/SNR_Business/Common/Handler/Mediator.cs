using SNR_Business.Common.Handler;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace SNR_Business.Common.Handler
{
    public interface IMediator
    {
        TResponse Query<TResponse>(IQuery<TResponse> query);
        void Execute<TCommand>(TCommand cmd);
        //TResponse Execute<TResponse>(ICommandHandler<TResponse> cmd);
        TResponse Execute<TResponse>(ICommandWithResponse<TResponse> cmd);
    }

    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResponse Query<TResponse>(IQuery<TResponse> query)
        {
            Type queryType = query.GetType();
            Type queryHandlerType;

            // Query by ID is a special case handler here, so we can generic fetch entities (type params are different so need to instantiate the handler differently)
            //if (queryType.IsConstructedGenericType && queryType.GetGenericTypeDefinition() == typeof(GetByIdQuery<,>))
            //{
            //    queryHandlerType = typeof(IQueryByIdHandler<,>).MakeGenericType(queryType.GenericTypeArguments.First(), typeof(TResponse));
            //}
            //else
            {
                queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));
            }
            dynamic handler = _serviceProvider.GetService(queryHandlerType);
            return handler.Get((dynamic)query);
        }

        public void Execute<TCommand>(TCommand cmd)
        {
            _serviceProvider.GetService<ICommandHandler<TCommand>>().Handle(cmd);
        }

        public TResponse Execute<TResponse>(ICommandWithResponse<TResponse> cmd)
        {
            Type cmdHamdlerType = typeof(ICommandHandler<,>).MakeGenericType(cmd.GetType(), typeof(TResponse));
            dynamic handler = _serviceProvider.GetService(cmdHamdlerType);
            return handler.Handle((dynamic)cmd);
        }
    }
}
