using System;
using System.IO;
using System.Reflection;
using Domain.Context;
using Domain.Model.Users.Factory;
using Domain.Repositories;
using Domain.Services.Users;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Sat.Recruitment.Api.Infrastructure.AutoMapper;

namespace Sat.Recruitment.Api
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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Users API",
                    Description = "Users API",
                });
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSingleton<IAsyncPolicy>(serviceProvider =>
                Policy
                    .Handle<IOException>()
                    .WaitAndRetryAsync(3, retryNumber => TimeSpan.FromSeconds(Math.Pow(2, retryNumber))));

            services.AddTransient<IUsersContext>(s => new UsersContext(Directory.GetCurrentDirectory() + this.Configuration["UsersConnectionString"]));
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IUserFactoryResolver>(s => new UserFactoryResolver(new IUserFactory[] { new PremiumUserFactory(), new SuperUserFactory(), new NormalUserFactory() }));

            services.AddAutoMapper(typeof(AutoMapperProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Users API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}