using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using ServerlessMoedas.Mappings;

[assembly: FunctionsStartup(typeof(ServerlessMoedas.Startup))]
namespace ServerlessMoedas
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(
                    Environment.GetEnvironmentVariable("BaseCotacoes")))
                .Mappings(m => m.FluentMappings
                    .AddFromAssemblyOf<CotacoesMap>())
                .BuildSessionFactory();        
            
            builder.Services.AddSingleton(sessionFactory);
            builder.Services.AddScoped(factory => sessionFactory.OpenSession());
        }
    }
}