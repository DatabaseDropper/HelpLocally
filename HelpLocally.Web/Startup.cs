using HelpLocally.Application;
using HelpLocally.Common;
using HelpLocally.Domain;
using HelpLocally.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace HelpLocally.Web
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
            services.AddTransient<UserService>();
            services.AddTransient<OfferService>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddDbContext<HelpLocallyContext>(builder => builder.UseNpgsql(Configuration.GetConnectionString("HelpLocallyConnectionString")));

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, x =>
                {
                    x.LoginPath = "/Account/SignIn";
                    x.AccessDeniedPath = "/Account/Denied";
                    x.Cookie.Name = "RODO_CONSENT";
                    x.Cookie.HttpOnly = true;
                    x.Cookie.IsEssential = true;
                    x.ExpireTimeSpan = new TimeSpan(24, 0, 0);
                }); 
            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HelpLocallyContext _context, IPasswordHasher<User> _hasher)
        {
            //_context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                var roleAdmin = new Role(1, Roles.Admin);
                var roleCustomer = new Role(2, Roles.Customer);
                var roleCompany = new Role(3, Roles.Company);
                _context.Roles.Add(roleAdmin);
                _context.Roles.Add(roleCustomer);
                _context.Roles.Add(roleCompany);

                var user = new User("admin");
                user.PasswordHash = _hasher.HashPassword(user, "12345");

                var userRole = new UserRole
                {
                    User = user,
                    UserId = user.Id,
                    Role = roleAdmin,
                    RoleId = roleAdmin.Id
                };

                user.UserRoles.Add(userRole);
                _context.Add(user);
                _context.Add(new OfferType(1,"Voucher", "Do wykorzysatnia w przysz³oœci" ));
                _context.SaveChanges();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}