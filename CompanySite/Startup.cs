using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using CompanySite.Domain;
using CompanySite.Domain.Repositories.Abstract;
using CompanySite.Domain.Repositories.EntityFramework;
using CompanySite.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompanySite
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            //Подключение config-файла из appsettings.json
            Configuration.Bind("Project", new Config());

            //Подключение функционала в качестве сервисов через связывание данных
            services.AddTransient<ITextFieldsRepositoriy, EFTextFieldsRepository>();//привязка интерфейса к реализации
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            //Подключение контекста базы данных
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            //Настройка identity системы
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //Настройка authentication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "BsCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/accout/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //поддержка контроллеров и представлений и установка совместимости с asp.net core 3.0
            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            //Порядок подключения сервисов очень важен!!!
            //1.Вывод сообщения об ошибках при их наличии
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //2.Настройка маршрутизации
            app.UseRouting();

            //Подключение аунтентификации и авторизации только после UseRouting и перед UseEndpoints
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //3.Подключениеподдержки файлов (css, html, js)
            app.UseStaticFiles();
            
            //4.Регистация нужных нам маршрутов(endpoints)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
