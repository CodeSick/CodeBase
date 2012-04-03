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
    public class RoutesTest
    {

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            // Initialize the application's routing
            MvcApplication.RegisterRoutes(RouteTable.Routes);
        }


        [TestMethod]
        [Description("Home controllers route test")]
        public void TestRootUrlMatchesHomeIndexUsingMvcContrib()
        {
           
            "~/".ShouldMapTo<HomeController>(controller => controller.Index());
            "~/Home/Index".ShouldMapTo<HomeController>(controler => controler.Index());
        }

        [TestMethod]
        [Description("Article controller route tests")]
        public void ArticleControlerRouteTest()
        {
            "~/Articles".ShouldMapTo<ArticlesController>(controller => controller.Index());
            "~/Articles/Edit/5".ShouldMapTo<ArticlesController>(controler => controler.Edit(5));
        }

        [TestMethod]
        public void AjaxRatingTest()
        {
            var route = "~/Articles/Rate/".WithMethod(HttpVerbs.Post);
            route.Values["id"] = 1;
            route.Values["value"] = 2;
            route.ShouldMapTo<ArticlesController>(controller => controller.Rate(1, 2));
        }



    }
}
