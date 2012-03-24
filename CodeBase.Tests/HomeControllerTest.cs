using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeBase.Controllers;
using Moq;
using CodeBase.Models;
using CodeBase.Tests.Models;
using System.Web.Mvc;
using CodeBase.ViewModel;

namespace CodeBase.Tests
{
	[TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            ICodeBaseRepository repo = new FakeCodeBaseContext();
        	// Arrange
        	var controller = new HomeController(repo);
        
        	// Act
        	var actionResult = controller.Index() as ViewResult;

            Assert.IsTrue(((IndexViewModel)actionResult.Model).Articles.Count() == 0);
            Assert.AreEqual("Index", actionResult.ViewName);
        }
    }
}