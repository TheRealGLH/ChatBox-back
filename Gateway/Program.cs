using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotBasic
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddJsonFile("ocelot.json")
                    .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                    .AddEnvironmentVariables();
            })
            .ConfigureServices(s =>
            {
                s.AddOcelot();
                s.AddCors(o =>
                    {
                        o.AddPolicy("CorsPolicy", p =>
                        {
                            //TODO: Change this to our hosted domain once we get there
                            p.WithOrigins(new string[] { "http://127.0.0.1:4200", "http://localhost:4200" })
                            .AllowCredentials()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                        });
                    });

            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                //add your logging
            })
            .UseIISIntegration()
            .Configure(app =>
            {
                app.UseCors("CorsPolicy");
                app.UsePreflightRequestHandler();
                app.UseWebSockets();
                app.UseOcelot().Wait();
            })
            .Build()
            .Run();
        }
    }

    public class PreflightRequestMiddleware
    {
        private readonly RequestDelegate Next;
        public PreflightRequestMiddleware(RequestDelegate next)
        {
            Next = next;
        }
        public Task Invoke(HttpContext context)
        {
            return BeginInvoke(context);
        }
        private Task BeginInvoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
            context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept, Athorization, ActualUserOrImpersonatedUserSamAccount, IsImpersonatedUser" });
            context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
            if (context.Request.Method == HttpMethod.Options.Method)
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return context.Response.WriteAsync("OK");
            }
            return Next.Invoke(context);
        }
    }

    public static class PreflightRequestExtensions
    {
        public static IApplicationBuilder UsePreflightRequestHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PreflightRequestMiddleware>();
        }
    }
}