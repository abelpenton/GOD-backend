using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.src.GOD.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using backend.src.GOD.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Game;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Player;
using backend.src.GOD.DataAccess.Repositories.GODRepositories.Round;
using backend.src.GOD.DataAccess.Repositories.UnitOfWork;

using backend.src.GOD.BussineServices.Services.Game;
using backend.src.GOD.BussineServices.Services.Player;
using backend.src.GOD.BussineServices.Services.Round;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var autoMapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });

            var mapper = autoMapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<DbContext, GODDataContext>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IRoundRepository, RoundRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            void MigrationAssembly(SqlServerDbContextOptionsBuilder x) => x.MigrationsAssembly("GOD.DataAccess..Migrations");
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<GODDataContext>(options =>
                options.UseSqlServer(connectionString, MigrationAssembly));

            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddScoped<IPlayerService, PlayerService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
