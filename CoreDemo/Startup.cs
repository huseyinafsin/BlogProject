using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace CoreDemo
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

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));

            });
            services.AddMvc();
            services.AddDbContext<Context>();
            services.AddAuthentication(
                    CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/Account/Login";
                });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.AddSession();
            services.AddControllersWithViews();

            services.AddDbContext<DataAccessLayer.Concrete.Context>();
            services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = false;

            }).AddEntityFrameworkStores<DataAccessLayer.Concrete.Context>();
            services.AddTransient<IBlogService, BlogManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<ICommentService, CommentManager>();
            services.AddTransient<IWriterService, WriterManager>();
            services.AddTransient<INewsletterService, NewsletterManager>();
            services.AddTransient<IAboutService, AboutManager>();
            services.AddTransient<IContactService, ContactManager>();
            services.AddTransient<INotificationService, NotificationManager>();
            services.AddTransient<IMessageService, MessageManager>();
            services.AddTransient<IMessage2Service, Message2Manager>();

            services.AddTransient<IBlogDal, EFBlogRepository>();
            services.AddTransient<ICategoryDal, EFCategoryRepository>();
            services.AddTransient<ICommentDal, EFCommentRepository>();
            services.AddTransient<IWriterDal, EFWriterRepository>();
            services.AddTransient<INewsletterDal, EfNewsletterRepository>();
            services.AddTransient<IAboutDal, EFAboutRepository>();
            services.AddTransient<IContactDal, EFContactRepository>();
            services.AddTransient<INotificationDal, EFNotificationRepository>();
            services.AddTransient<IMessageDal, EFMessageRepository>();
            services.AddTransient<IMessage2Dal, EFMessage2Repository>();


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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1","?code={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "admin",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "blog",
                   pattern: "{controller=Blog}/{action=BlogReadAll}/{blogUrlId}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Blog}/{action=Index}/{id?}");
            });

        }
    }
}
