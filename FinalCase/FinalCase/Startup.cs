
using FinalCase.Data;
using Microsoft.EntityFrameworkCore;
using Vb.Data.DbContext;

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
            services.AddScoped<IVbDbContext>(provider => provider.GetService<VbDbContext>());

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


            app.UseEndpoints(x => { x.MapControllers(); });
        }
    }
}