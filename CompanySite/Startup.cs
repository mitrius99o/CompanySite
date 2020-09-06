using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanySite.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            //поддержка контроллеров и представлений и установка совместимости с asp.net core 3.0
            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Подключение config-файла из appsettings.json
            Configuration.Bind("Project", new Config());
            //Порядок подключения сервисов очень важен!!!
            //1.Вывод сообщения об ошибках при их наличии
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //2.Настройка маршрутизации
            app.UseRouting();

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
