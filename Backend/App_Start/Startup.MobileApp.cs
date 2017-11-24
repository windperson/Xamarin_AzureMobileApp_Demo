using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Backend.DataObjects;
using Backend.Logger;
using Backend.Models;
using Owin;

namespace Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.EnableSystemDiagnosticsTracing();
#if DEBUG
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
#endif
            config.Services.Add(typeof(IExceptionLogger), new TraceExceptionLogger());
            var mobileConfig = new MobileAppConfiguration();

            mobileConfig.AddTablesWithEntityFramework().ApplyTo(config);
            
            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new MobileServiceInitializer());

            app.UseWebApi(config);
        }
    }

    public class MobileServiceInitializer : CreateDatabaseIfNotExists<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false }
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }
}

