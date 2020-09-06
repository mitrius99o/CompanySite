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
            //��������� ������������ � ������������� � ��������� ������������� � asp.net core 3.0
            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //����������� config-����� �� appsettings.json
            Configuration.Bind("Project", new Config());
            //������� ����������� �������� ����� �����!!!
            //1.����� ��������� �� ������� ��� �� �������
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //2.��������� �������������
            app.UseRouting();

            //3.�������������������� ������ (css, html, js)
            app.UseStaticFiles();
            
            //4.���������� ������ ��� ���������(endpoints)
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
