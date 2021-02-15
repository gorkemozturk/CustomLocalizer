using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace CustomLocalizer
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
            services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStringLocalizer<Startup> stringLocalizer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRequestLocalization(BuildLocalizationOptions());

            app.Use(async (context, next) =>
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/html; charset=utf-8";

                await context.Response.WriteAsync(BuildResponse(stringLocalizer));
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private RequestLocalizationOptions BuildLocalizationOptions()
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-US"),
                new CultureInfo("it-IT"),
                new CultureInfo("ja-JP"),
                new CultureInfo("nl-NL"),
                new CultureInfo("ru-RU"),
                new CultureInfo("sv-SE"),
                new CultureInfo("tr-TR")
            };

            return new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
        }

        private string BuildResponse(IStringLocalizer stringLocalizer)
        {
            var currentCultureName = CultureInfo.CurrentCulture.EnglishName;
            var currentUICultureName = CultureInfo.CurrentUICulture.EnglishName;

            return "<html><body>"
                + $"<h2>{stringLocalizer["Hello"]}!</h2><table border=\"1\" cellpadding=\"5\" style=\"border-collapse:collapse;\">"
                + $"<tr><td>{stringLocalizer["Current Culture"]}</td><td>{currentCultureName}</td></tr>"
                + $"<tr><td>{stringLocalizer["Current UI Culture"]}</td><td>{currentUICultureName}</td></tr>"
                + $"<tr><td>{stringLocalizer["The Current Date"]}</td><td>{DateTime.Now.ToString("D")}</td></tr>"
                + $"<tr><td>{stringLocalizer["A Formatted Number"]}</td><td>{(1234567.89).ToString("n")}</td></tr>"
                + $"<tr><td>{stringLocalizer["A Currency Value"]}</td><td>{(42).ToString("C")}</td></tr></table>"
                + $"<h2>{stringLocalizer["Goodbye"]}</h2>"
                + "</body></html>";
        }
    }
}
