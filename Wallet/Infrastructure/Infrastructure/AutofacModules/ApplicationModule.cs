using Autofac;
using WalletService.API.Application.Queries;
using WalletService.Domain.AggregatesModel.WalletService.Aggregate;
using WalletService.Service.Infrastructure.Idempotency;
using WalletService.Service.Infrastructure.Repositories;

namespace WalletService.API.Infrastructure.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register(c => new WalletQueries(QueriesConnectionString))
                .As<IWalletQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<WalletRepository>()
                .As<IWalletRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RequestManager>()
               .As<IRequestManager>()
               .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
