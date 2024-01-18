﻿
using AutoMapper;
using FinalCase.Business.Cqrs;
using FinalCase.Business.Mapper;
using FinalCase.Data.DbOperations;
using FinalCase.Middlewares;
using FinalCase.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinalCase
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<VbDbContext>(options => options.UseSqlServer(connection));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateContactCommand).GetTypeInfo().Assembly));
            
            services.AddFluentValidation(conf =>
            {
                conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
                conf.AutomaticValidationEnabled = false;
            });
            
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig()));
            services.AddSingleton(mapperConfig.CreateMapper());


            services.AddSingleton<ILoggerService, ConsoleLogger>();

            services.AddControllers(); // httppatch için eklendi
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

           }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();


            //middlewaare
            app.UseCustomExceptionMiddleware();

            app.UseEndpoints(x => { x.MapControllers(); });
        }
    }
}