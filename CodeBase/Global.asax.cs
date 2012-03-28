using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CodeBase.Models;
using Ninject;

namespace CodeBase
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            SetupDependencyInjection();
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
           //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<CodeBase.Models.CodeBaseContext>());
            // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseAlways<CodeBase.Models.CodeBaseContext>());
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.CreateDatabaseIfNotExists<CodeBase.Models.CodeBaseContext>());

        }

        public void SetupDependencyInjection()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<ICodeBaseRepository>().To<CodeBaseContext>();

            kernel.Bind<CodeBaseContext>().To<CodeBaseContext>();
            DependencyResolver.SetResolver(new NinjectDependencyReslover(kernel));
        }
    }
}