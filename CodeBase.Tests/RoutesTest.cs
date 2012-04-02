using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CodeBase.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcContrib.TestHelper;

namespace CodeBase.Tests
{



    [TestClass]
    class RoutesTest
    {


        [TestMethod]
        [Description("Home controllers route test")]
        public void TestRootUrlMatchesHomeIndexUsingMvcContrib()
        {
            // Initialize the application's routing
            MvcApplication.RegisterRoutes(RouteTable.Routes);
            "~/".ShouldMapTo<HomeController>(controller => controller.Index());
            "~/Home/Index".ShouldMapTo<HomeController>(controler => controler.Index());
        }

        [TestMethod]
        [Description("Article controller route tests")]
        public void ArticleControlerRouteTest()
        {
            // Initialize the application's routing
            MvcApplication.RegisterRoutes(RouteTable.Routes);
            "~/Articles".ShouldMapTo<ArticlesController>(controller => controller.Index());
            "~/Articles/Edit/5".ShouldMapTo<ArticlesController>(controler => controler.Edit(5));
        }

        [TestMethod]
        public void AjaxRatingTest()
        {
            MvcApplication.RegisterRoutes(RouteTable.Routes);
            var route = "~/Articles/Rate/".WithMethod(HttpVerbs.Post);
            route.Values["id"] = 1;
            route.Values["value"] = 2;
            route.ShouldMapTo<ArticlesController>(controller => controller.Rate(1, 2));
        }



    }
}
