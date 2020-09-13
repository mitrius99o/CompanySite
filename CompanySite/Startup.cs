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
            //����������� config-����� �� appsettings.json
            Configuration.Bind("Project", new Config());

            //����������� ����������� � �������� �������� ����� ���������� ������
            services.AddTransient<ITextFieldsRepositoriy, EFTextFieldsRepository>();//�������� ���������� � ����������
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<DataManager>();

            //����������� ��������� ���� ������
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            //��������� identity �������
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //��������� authentication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "BsCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/accout/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

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

            //����������� ��������������� � ����������� ������ ����� UseRouting � ����� UseEndpoints
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

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
