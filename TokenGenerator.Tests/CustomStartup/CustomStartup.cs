using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TokenGenerator.Controllers;
using TokenGenerator.Data;

namespace TokenGenerator.Tests.CustomStartup
{
    class CustomStartup : Startup
    {
        public CustomStartup(IConfiguration configuration) : base(configuration)
        {

        }
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(CardsController).Assembly);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            base.ConfigureServices(services);
        }

        protected  void AddDatabaseContext(IServiceCollection services)
        {
            // "DefaultConnection": "Data Source=:memory:;"
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            services.AddDbContext<TokenGeneratorContext>(options =>
                options.UseSqlite(connection));
        }
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.Configure(app, env);

           
        }
    }
}
