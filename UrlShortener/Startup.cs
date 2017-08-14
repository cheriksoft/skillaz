using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using UrlShortener.Entities;
using UrlShortener.Factories;
using UrlShortener.Infrastructure;
using UrlShortener.Models.UrlEntries;
using UrlShortener.MongoInfrastructure;
using UrlShortener.Repositories;

namespace UrlShortener
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<MongoDbConfiguration>(Configuration.GetSection("MongoDb"));
            services.AddOptions();

            services.AddSingleton<IMongoDbProvider, MongoDbProvider>();
            services.AddSingleton<IMongoTableNameResolver, MongoTableNameResolver>();
            services.AddSingleton<IMongoCollectionProvider<IMongoEntity>, MongoCollectionProvider<IMongoEntity>>();

            services.AddSingleton<IUrlIdGenerator, UrlIdGenerator>();
            services.AddSingleton<IUrlIdStringConverter, UrlIdStringConverter>();

            services.AddScoped<ICookieSessionIdProvider, CookieSessionIdProvider>();

            services.AddScoped<IUrlEntryRepository, UrlEntryRepository>();
            services.AddScoped<IUrlEntryModelBuilder, UrlEntryModelBuilder>();
            services.AddScoped<IUrlEntryHandler, UrlEntryHandler>();
            services.AddScoped<IUrlEntryFactory, UrlEntryFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();

            app.UseMvc();

            app.UseMiddleware<StatusCodeExceptionHandler>();
        }
    }
}
