using EmpleadosEBS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EmpleadosEBS.Models;
using EmpleadosEBS.Repositories;
using Rotativa.AspNetCore;
using EmpleadosEBS.Hubs;


namespace EmpleadosEBS
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>().AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IArticuloRepository, ArticuloRepository>();
            services.AddTransient<IPlatoRepository, PlatoRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));

            services.AddMvc(options =>
                   options.EnableEndpointRouting = false)
                   .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddMemoryCache();
            services.AddSession();

            services.AddSignalR();


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

            });

            CreateRolesAndAdminUser(serviceProvider);

            RotativaConfiguration.Setup(env, "..\\Rotativa\\Windows\\");

            app.UseSignalR(routes =>
            {
                routes.MapHub<InformeHub>("/InformeHub");
            });
        }

        //----------------------------------------------------------------------------
        private static void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            const string adminRoleName = "Administrador";
            // lista de Roles
            string[] roleNames = { adminRoleName, "Cocinero", "Cajero", "Cliente" };

            foreach (string roleName in roleNames)
            {
                CreateRole(serviceProvider, roleName);
            }

            string adminUserEmail = "seba_storm@hotmail.com";
            string cajeroUserEmail = "cajero@cajero.com";
            string cocineroUserEmail = "cocinero@cocinero.com";
            string cliente1 = "usuario1@usuario.com";
            string cliente2 = "Usuario2@usuario.com";
            string cliente3 = "Usuario3@usuario.com";
            string Pwd = "Dana.2002";

            AddUserToRole(serviceProvider, adminUserEmail, Pwd, adminRoleName);
            AddUserToRole(serviceProvider, cajeroUserEmail, Pwd, "Cajero");
            AddUserToRole(serviceProvider, cocineroUserEmail, Pwd, "Cocinero");
            AddUserToRole(serviceProvider, cliente1, Pwd, "Cliente");
            AddUserToRole(serviceProvider, cliente2, Pwd, "Cliente");
            AddUserToRole(serviceProvider, cliente3, Pwd, "Cliente");
           
            
        }

        // Crear un rol si no existe
        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }

        //Agrega usuario a un rol si el usuario existe, de lo contrario, cree al usuario y agréguelo al rol.
        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail, string userPwd, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            Task<User> checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();

            User appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                User newAppUser = new User
                {
                    Registro = DateTime.Now,
                    Email = userEmail,
                    UserName = userEmail
                };

                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    appUser = newAppUser;
                }
            }

            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
            newUserRole.Wait();
        }
    }
}
