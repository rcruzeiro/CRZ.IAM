using System;
using CRZ.Framework.Repository;
using CRZ.Framework.Web.Security;
using CRZ.IAM.Database.MySQL;
using CRZ.IAM.Database.MySQL.Repositories;
using CRZ.IAM.Infrastructure.Providers.Security;
using CRZ.IAM.Platform.Application.Account;
using CRZ.IAM.Platform.Application.Account.Commands;
using CRZ.IAM.Platform.Domain.Account;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRZ.IAM.Infrastructure.IoC
{
    public class IAMModule
    {
        public IAMModule(IConfiguration configuration)
           : this(new ServiceCollection(), configuration)
        { }

        public IAMModule(IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            // jwt
            var signingConfiguration = new SigninConfiguration(
                configuration.GetValue<string>("Security:Key"));
            var tokenConfiguration = new TokenConfiguration(configuration);

            // jwt module
            var secModule = new SecurityModule(services, signingConfiguration, tokenConfiguration);

            // security token
            services.AddTransient<ISecurityTokenService<User>>(provider =>
                new UserSecurityTokenService(signingConfiguration, tokenConfiguration));

            // data source
            services.AddSingleton<IDataSource>(provider =>
                new DefaultDataSource(configuration, "IAM"));

            // unit of work / db context
            services.AddTransient<IUnitOfWorkAsync, IAMContext>();

            // repositories
            services.AddTransient<IUserRepository, UserRepository>();

            // command handlers
            services.AddTransient<IAccountCommandHandler, AccountCommandHandler>();

            // query handlers

            // app services
            services.AddTransient<IAccountAppService, AccountAppService>();
        }
    }
}
