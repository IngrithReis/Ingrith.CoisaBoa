using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using Ingrith.CoisaBoa.WebApp.Data;
using Ingrith.CoisaBoa.WebApp.Domain;
using System.Threading.Tasks;

namespace Ingrith.CoisaBoa.WebApp
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
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("AppDbContext")));

            services.AddIdentity<Usuario, IdentityRole>(options =>
            {

                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;


                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(
                options => options.TokenLifespan = TimeSpan.FromHours(3));




            services.ConfigureApplicationCookie(options => options.LoginPath = "/Home/Login");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            CreateRoles(app).Wait();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }

        private static async Task CreateRoles(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var roleManagger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();

            var role = await roleManagger.FindByNameAsync("Admin");
            var userAdmin = await userManager.FindByNameAsync("Administrador");

            if (role == null)
                await roleManagger.CreateAsync(new IdentityRole("Admin"));

            if (userAdmin == null) {
                userAdmin = new Usuario
                {
                    Nome = "Laura",
                    UserName = "Administrador",
                    Email = "admin@coisaboa.com.br"
                };
               var createResult = await userManager.CreateAsync(userAdmin, "!Admin123");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(userAdmin, "Admin");
                }
            }
            else
            {
                if(!await userManager.IsInRoleAsync(userAdmin, "Admin"))
                {
                    await userManager.AddToRoleAsync(userAdmin, "Admin");
                }

            }
               

            
        }
    }
}
