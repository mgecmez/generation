using Generation.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Generation.Domain;
using Generation.Service;
using Generation.Service.Mappers;
using Hangfire;
using System.Linq;
using Hangfire.SqlServer;

namespace Generation.Api
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Generation.Api", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddCors(c =>
              {
                  c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
              });

            ConfigureDatabase(services);

            services.AddScoped<ITimedValueRepository, TimedValueRepository>();
            services.AddScoped<IPowerPlantRepository, PowerPlantRepository>();

            services.AddScoped<ITimedValueService, TimedValueService>();
            services.AddScoped<IPowerPlantService, PowerPlantService>();
            services.AddScoped<IJobService, JobService>();

            services.AddTransient<ITimedValueMapper, TimedValueMapper>();
            services.AddTransient<IPowerPlantMapper, PowerPlantMapper>();

            var hangfireOptions = new SqlServerStorageOptions
            {
                PrepareSchemaIfNecessary = true
            };
            services.AddHangfire(configure =>
            {
                configure.UseSqlServerStorage(Configuration.GetConnectionString("GenerationDbConnection"), hangfireOptions);
            });

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<GenerationDbContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "Generation.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<GenerationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("GenerationDbConnection")));
        }
    }
}
