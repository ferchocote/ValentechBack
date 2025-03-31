using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime;
using PruebaAppApi.DataAccess.DataAccess;
using AplicationServices.ScopeService;
using AplicationServices.Application.Contracts.Helpers;
using AplicationServices.Helpers.Logger;
using AplicationServices.Application.Contracts.Authentication;
using AplicationServices.Application.Authentication;
using DomainServices.Domain.Contracts.User;
using DomainServices.Domain.User;
using PruebaAppApi.DataAccess.Entities;
using AplicationServices.Application.Contracts.Patient;
using AplicationServices.Application.Patient;
using DomainServices.Domain.Patient;
using DomainServices.Domain.Contracts.Patient;



namespace PruebaAppApi.DI
{
    /// <summary>
    /// Provee la carga de los perfiles de inyección de dependencias
    /// de toda la solución
    /// </summary>
    public static class DependencyInjectionProfile
    {
        public static void RegisterProfile(IServiceCollection services, IConfiguration configuration)
        {
            #region Context

            CustomDbSettings val = new CustomDbSettings();


            services.AddDbContextFactory<ValentechCoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"))
                .LogTo(System.Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            });

            #endregion Context

            #region Application

            services.AddTransient<IAuthenticationAppServices, AuthenticationAppServices>();
            services.AddTransient<IPatientAppServices, PatientAppServices>();

            #endregion

            #region Domain

            services.AddTransient<IUserDomain, UserDomain>();
            services.AddTransient<IPatientDomain, PatientDomain>();

            #endregion Domain

            #region Others
            services.AddTransient<IServiceScopeDI, ServiceScope>();
            services.AddTransient<IServiceProvider, ServiceProvider>();
            #endregion
        }

    }
}
