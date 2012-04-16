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
            CodeBaseContext repo = new FakeCodeBaseContext();
           
        	// Arrange
        	var controller = new HomeController();
            controller.context = repo;
           
        
        	// Act
        	var actionResult = controller.Index() as ViewResult;

            Assert.IsTrue(((IndexViewModel)actionResult.Model).Articles.Count() == 0);
            Assert.AreEqual("Index", actionResult.ViewName);
        }

        [TestMethod]
        public void ContrololerAddTest()
        {
            CodeBaseContext repo = new FakeCodeBaseContext();
            CategoriesController controller = new CategoriesController { context = repo };
            controller.Create(new Category { Title = "C#" , CategoryId=1, Articles=null});
            repo.SaveChanges();

            Assert.AreEqual("C#", repo.Categories.First().Title);
        }

        [TestMethod]
        public void ContrololerAddShouRedispayIfCategoryInvalid()
        {
            CodeBaseContext repo = new FakeCodeBaseContext();
            CategoriesController controller = new CategoriesController { context = repo };
            controller.ModelState.AddModelError("eror", "model error");
            var c = new Category { CategoryId = 1, Title = "1", Articles = null };
            var result = controller.Create();

            Assert.IsNotNull(result);
            Assert.IsTrue((result as ViewResult).ViewData.ModelState.Count > 0, "Expected errors");
        }

        
        public void ComtrollerEditCategory()
        {

            CodeBaseContext repo = new FakeCodeBaseContext();
            repo.Categories.Add(new Category { Title = "C#", CategoryId = 1, Articles = null });
            repo.SaveChanges();
            var category = repo.Categories.First();
            category.Title = "C++";

            CategoriesController controller = new CategoriesController { context = repo };
            controller.Edit(category);
            

            Assert.AreEqual("C++", repo.Categories.First().Title);
        }
    }
}