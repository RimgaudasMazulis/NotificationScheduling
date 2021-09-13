using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NotificationScheduling.Domain.Exceptions.Filter;
using NotificationScheduling.Domain.Models;
using NotificationScheduling.Infrastructure;
using NotificationScheduling.Web.Extensions;

namespace NotificationScheduling.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            configuration.GetSection("AppSettings").Get<AppSettings>(options => options.BindNonPublicProperties = true);
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllersWithViews();
            services.AddMemoryCache();
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"), options => options.BindNonPublicProperties = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationScheduling.Web", Version = "v1" });
            });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDatabase(Configuration)
                .AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, NotificationSchedulingContext context)
        {
            //context.Database.Migrate();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });

            loggerFactory.AddFile("Logs/log-{Date}.txt");

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
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseCors(x => x
                 .SetIsOriginAllowed(origin => true)
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials());

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            //DbInitializer: (Seed method)
            DbInitializer.Initialize(context);
        }

        //private IConfiguration InitConfiguration(IWebHostEnvironment env)
        //{
        //    // Config the app to read values from appsettings base on current environment value.
        //    var configuration = new ConfigurationBuilder()
        //        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //        .AddJsonFile("appsettings.json", false, true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
        //        .AddEnvironmentVariables().Build();
        //    //
        //    // Map AppSettings section in appsettings.json file value to AppSetting model
        //    configuration.GetSection("AppSettings").Get<AppSettings>(options => options.BindNonPublicProperties = true);
        //    return configuration;
        //}
    }
}
