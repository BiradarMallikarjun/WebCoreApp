using System;
using Xunit;
using WebAppCore.Controllers;
using WebAppCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace XUnitTestWebAppCore
{
    public class PersonTests : IWebHostEnvironment
    {
        public IFileProvider WebRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WebRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IFileProvider ContentRootFileProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ContentRootPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string EnvironmentName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Fact]
        public void Home_Index_GetPeople()
        {
           //Arrange
            HomeController home = new HomeController(new MoqPersonRepository(), new PersonTests());

            //Act
            var index = home.Index() as ViewResult;
            string viewName = "Index";

            //Assert
            Assert.NotNull(index);
            Assert.Equal(viewName, index.ViewName);

        }

        [Fact]
        public void Home_Details_GetPeople1ById()
        {
            //Arrange
            HomeController home = new HomeController(new MoqPersonRepository(), new PersonTests());

            //Act
            int id = 2;
            var People = GetPersonTest();
            var expPeoplebyId2 = People.FirstOrDefault(e => e.Id == id);
            
            var index = home.Details(id) as ViewResult;
            var actPerson = (Person)index.Model;
            //Assert
            Assert.NotNull(actPerson);

            Assert.NotEmpty(actPerson.Id.ToString());

            Assert.True(expPeoplebyId2.Equals(actPerson));

        }

        private List<Person> GetPersonTest()
        {
            return new List<Person>()
            {
                new Person() { Id = 1, FirstName = "Maha", LastName = "Biradar", Age = 22 },
                new Person() { Id = 2, FirstName = "Manju", LastName = "Biradar", Age = 32 }
            };
        }
    }
}
