using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pgcode.Api;
using Pgcode.Connection;
using Pgcode.Middleware;

namespace Pgcode
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddGrpc();
            services.AddSignalR();
            services.AddSingleton<ConnectionManager, ConnectionManager>();
            services.AddSingleton(Program.Settings);
            services.AddSingleton<CookieMiddleware, CookieMiddleware>();
            //services.AddSingleton<GrpcWebServiceFilter, GrpcWebServiceFilter>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRouting();
            //app.UseGrpcWeb();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGrpcService<ConnectionService>().EnableGrpcWeb();
                endpoints.MapHub<ConnectionsHub>("/connectionsHub");
            });

            IMiddleware cookieMiddleware = new CookieMiddleware();
            IMiddleware loggingMiddleware = new LoggingMiddleware(loggerFactory);

            if (env.IsDevelopment())
            {
                cookieMiddleware.Use(app);
                if (Program.Settings.LogRequests)
                {
                    loggingMiddleware.Use(app);
                }
                app.UseDevelopmentMiddleware();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                if (Program.Settings.LogRequests)
                {
                    app.UseResourceMiddleware(cookieMiddleware, loggingMiddleware);
                }
                else
                {
                    app.UseResourceMiddleware(cookieMiddleware);
                }
            }

            if (Program.Settings.LogPgCodeCommandNotice || Program.Settings.LogPgCodeDbCommands)
            {
                ConnectionManager.AddLoggers(loggerFactory);
            }

            if (Program.Settings.LogPgCodeCommandNotice)
            {
                ConnectionManager.AddNoticeHandlersToConnections(loggerFactory);
            }

            app.UseHttpsRedirection();
        }
    }
}