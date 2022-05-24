using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using WifiSD.Application.Bootstrap;
using WifiSD.Application.Movies;
using WifiSD.Persistence.Bootstrap;
using WifiSD.Persistence.Repositories.DBContext;
using WifiSD.Resources;
using WifiSD.Resources.Attributes;
using WifiSD.WebAPP.Data;
using Microsoft.AspNetCore.Mvc.Razor;

namespace WifiSD.WebAPP
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
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("UserDbConnection")));

            services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MovieDbContext")));

            services.RegisterApplicationServices();
            services.RegisterRepositories();

            /* Browser-Sprache-Erkennung implementieren */
            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("de"),
                    new CultureInfo("de-AT"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("de-CH"),
                    new CultureInfo("en")
                };

                opts.DefaultRequestCulture = new RequestCulture("de");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;

            });

            /* Sprachabhängige Views verwenden (Razor) */
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; });

            // MediatR inkl. Handler registrieren
            services.AddMediatR(typeof(MovieQueryHandler).GetTypeInfo().Assembly);


            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages() // für das erstellen der HTML seiten (Razor Engine)
                    .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            /* Browser-Sprach-Erkennung aktivieren */
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<MovieDbContext>();
                dbContext.Database.Migrate();

                var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                applicationDbContext.Database.Migrate();       // aus der Servicecollection wird der DbContext geladen und führt die Änderungen und update in der DB durch
            }

            LocalizedDescriptionAttribute.Setup(new ResourceManager(typeof(BasicRes)));
        }
    }
}
