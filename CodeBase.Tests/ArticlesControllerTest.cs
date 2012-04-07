using System;
using CodeBase.Controllers;
using CodeBase.Models;
using CodeBase.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace CodeBase.Tests
{
    [TestClass]
    public class ArticlesControllerTest
    {
        [TestMethod]
        public void CreatingNewArticleShouldSetUsernameAndDate()
        {
            var repo = new FakeCodeBaseContext();
            repo.Users.Add(new CodeBase.Models.User { Username = "snuderl" });

            
            MockRepository mock = new MockRepository(MockBehavior.Default);
            var membershipMock = mock.Create<ICodeBaseMembership>();
            membershipMock.Setup(x=>x.LoggedInUser()).Returns("snuderl");

            ArticlesController controller = new ArticlesController{ context=repo, membership=membershipMock.Object};
            controller.Create(new Article{ CategoryId=1, Content="blabla", Title="mock"});

            Assert.AreEqual(repo.Users.First().UserId, repo.Articles.First().UserId);


        }
    }
}
