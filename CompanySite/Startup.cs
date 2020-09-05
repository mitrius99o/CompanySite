using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompanySite
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //��������� ������������ � ������������� � ��������� ������������� � asp.net core 3.0
            services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
