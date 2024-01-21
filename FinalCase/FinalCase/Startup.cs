
using AutoMapper;
using FinalCase.Base.Token;
using FinalCase.Business.Cqrs;
using FinalCase.Business.Mapper;
using FinalCase.Data.DbOperations;
using FinalCase.Middlewares;
using FinalCase.Services;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Reflection;
using System.Text;

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

            // Swagger üzerinden yetkilendirmeler için token girilme alanı oluşturuldu
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vb Api Management", Version = "v1.0" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Vb Management for IT Company",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
            });


            services.AddResponseCaching();
            services.AddMemoryCache();

            // redis
            var redisConfig = new ConfigurationOptions();
            redisConfig.EndPoints.Add(Configuration["Redis:Host"], Convert.ToInt32(Configuration["Redis:Port"]));
            redisConfig.DefaultDatabase = 0;
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.ConfigurationOptions = redisConfig;
                opt.InstanceName = Configuration["Redis:InstanceName"];
            });

            // JwtToken configurasyonları yapıldı
            JwtConfig jwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Secret)),
                    ValidAudience = jwtConfig.Audience,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireSqlConnection"), new SqlServerStorageOptions
                {
                    TransactionTimeout = TimeSpan.FromMinutes(5),
                    InvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.FromMinutes(5),
                }));
            services.AddHangfireServer();

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
            // Yetkilendirme eklendi
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            //middlewaare
            app.UseCustomExceptionMiddleware();

            app.UseResponseCaching();

            app.UseHangfireDashboard();

            app.UseEndpoints(x => { x.MapControllers(); });
        }
    }
}