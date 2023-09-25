using Microsoft.EntityFrameworkCore;
using Books.Models;
using Books.Repository;
using Books.Services;
using Books.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace db_cp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration["DatabaseConnection"] = configuration.GetConnectionString("GuestConnection");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDBContext>(options =>
                options.UseSqlServer(Configuration["DatabaseConnection"]),
                ServiceLifetime.Transient);

			services.AddAuthentication(
                CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Login";
                        options.AccessDeniedPath = "/Account/Login";

                        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    });

			services.AddHttpContextAccessor();

			services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBookshelfService, BookshelfService>();
            services.AddTransient<ISeriesService, SeriesService>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookshelfRepository, BookshelfRepository>();
            services.AddTransient<ISeriesRepository, SeriesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

			services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Search}/{action=SimpleSearch}/{id?}");
            });
        }
    }
}