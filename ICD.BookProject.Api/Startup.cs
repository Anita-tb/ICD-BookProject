using System;
using System.Collections.Generic;
using ICD.Framework.Diagnosis;
using ICD.Framework.Extensions;
using ICD.Framework.Web;
using ICD.Infrastructure.Exception;
using ICD.BookProject.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;


namespace ICD.SampleSystem.Api
{
    public class Startup : TotalSystemWebStartup
    {
        //private IServiceCollection myServices;
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override IServiceProvider ConfigureServices(IServiceCollection services)
        {
            RegisterSwagger(services, "v1", "Book");

            services.AddObjectiveAppSettings("Base", "BAS");

            services.ConfigureComponentsDiagnosis(
               q => q.RequireDatabase()
               .RequireMessageBroker());

            var config = base.ConfigureServices(services);

            var configOption = config.GetAppSettings();
            configOption.ConnectionString.BaseDbContext =
                "Data Source=172.18.176.74,1436;Initial Catalog=AnitaDb;User Id=Sana;password=Tot@licd;MultipleActiveResultSets=true;Persist Security Info=True; TrustServerCertificate=True;";
            var connection = configOption.ConnectionString.BaseDbContext;
            

            services.AddDbContext<BaseDbContext>(options =>
            {
                options.UseSqlServer(connection, b => b.MigrationsAssembly("ICD.BookProject.Api"));
            });

            return config;
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnitaAPI V1"); });
            app.UseCors();
            app.UseExceptionHandlerMiddleware();
            app.UseRequestTracerMiddleware();
            base.Configure(app, env);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}