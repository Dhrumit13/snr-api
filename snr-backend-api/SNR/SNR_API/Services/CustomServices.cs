using System.Reflection;
using SNR_Business.Common.Handler;
using SNR_Data;

namespace SNR_API.Services
{
    public static class CustomServices
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IMediator, Mediator>();
            Assembly queryHandlerAssembly = typeof(IQueryHandler<,>).GetTypeInfo().Assembly;
            IList<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(queryHandlerAssembly);

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            #region BusinessLayer Services
           
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            #endregion

            #region DataLayer Services
            services.AddTransient<ISQLHelper>(d => new SQLHelper(config["Database:ConnectionString"]));
            services.AddTransient<IdCustomer, dCustomer>();
            services.AddTransient<IdRate, dRate>();
            services.AddTransient<IdUser, dUser>();
            services.AddTransient<IdOtherCharges, dOtherCharges>();
            services.AddTransient<IdReceiver, dReceiver>();
            #endregion

            return services;
        }
    }
}
