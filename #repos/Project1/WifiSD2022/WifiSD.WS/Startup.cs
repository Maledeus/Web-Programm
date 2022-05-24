using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WifiSD.Application.Authentication;
using WifiSD.Application.Bootstrap;
using WifiSD.Application.Movies;
using WifiSD.Common.Services;
using WifiSD.Persistence.Bootstrap;
using WifiSD.Persistence.Repositories.DBContext;

namespace WifiSD.WS
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
            /* Connection-String für DB Zugriff */
            var connectinString = Configuration.GetConnectionString("MovieDBContext");
            services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectinString));

            /*Query und Command registrieren */
            services.RegisterApplicationServices();

            /* Repositories registrieren*/
            services.RegisterRepositories();

            /*UserService registrieren */
            services.AddScoped<IUserService, UserService>();

            services.AddAuthentication("BasicAuthentication")
                    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            /*MediatR inkl. Handler registrieren */
            services.AddMediatR(typeof(MovieQueryHandler).GetTypeInfo().Assembly);

            services.AddSwaggerGen(g =>
            {
                g.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "WifiSD2022 - Service", Version = "v1" });

                g.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the basic scheme!"
                });

                g.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme

                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "basic"
                        }
                    },
                    new string[] {}
                    }

                });

            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                /* Swagger UI configuration*/
                app.UseSwagger();
                app.UseSwaggerUI(ui =>
                {
                    ui.SwaggerEndpoint("/swagger/v1/swagger.json", "WifiSD2022 - Service");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MovieDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
