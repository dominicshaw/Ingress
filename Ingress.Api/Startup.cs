using System;
using System.Web.Http;
using log4net;
using Microsoft.Owin.Diagnostics;
using Newtonsoft.Json;
using Owin;

namespace Ingress.Api
{
    internal class Startup
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Startup));

        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };

            config.Formatters.JsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.All; // when it serialises to json, include the type of the object (for abstract vs concrete classes, like ActivityDTO)

            config.MapHttpAttributeRoutes();

            appBuilder.UseErrorPage(new ErrorPageOptions
            {
                //Shows the OWIN environment dictionary keys and values. This detail is enabled by default if you are running your app from VS unless disabled in code. 
                ShowEnvironment = true,
                //Hides cookie details
                ShowCookies = false,
                //Shows the lines of code throwing this exception. This detail is enabled by default if you are running your app from VS unless disabled in code. 
                ShowSourceCode = true,
            });

            appBuilder.Use(async (env, next) =>
            {
                var start = DateTime.Now;
                _log.Debug($"IP: {env.Request.RemoteIpAddress}, Method: {env.Request.Method}, Path: {env.Request.Path}");

                try
                {
                    await next();
                }
                catch (System.Threading.Tasks.TaskCanceledException e)
                {
                    _log.Warn(e);
                    throw;
                }
                catch (OperationCanceledException e)
                {
                    _log.Warn(e);
                    throw;
                }
                catch (Exception e)
                {
                    _log.Error(e);
                    throw;
                }

                if (env.Response.StatusCode == 500)
                    _log.Error("Server Error: " + env.Response.ReasonPhrase);

                _log.Debug($"Response code: {env.Response.StatusCode}, Response time: {(DateTime.Now - start).TotalMilliseconds:#,0}ms");
            });

            appBuilder.UseWebApi(config);
        }
    }
}