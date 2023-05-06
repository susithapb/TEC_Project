using EntityFrameworkCoreMock;
using Xunit;
using WebApp.API.Tests;
using WebApp.API.Controllers;
using WebApp.API.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using WebApp.API.CoursesDbContext;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.API.Tests
{
    public class CoursesTests
    {
        #region Setting up MockDbContext and Seed data

        private CoursesController? _controller;
        DbContextMock<CoursesDbContext.CoursesDbContext>? _dbContextMock;

        public DbContextMock<CoursesDbContext.CoursesDbContext> GetDbContext(Course[] seedData)
        {

            DbContextMock<CoursesDbContext.CoursesDbContext> dbContextMock = new DbContextMock<CoursesDbContext.CoursesDbContext>(new DbContextOptionsBuilder<CoursesDbContext.CoursesDbContext>().Options);
            dbContextMock.CreateDbSetMock(x => x.Courses, seedData);
            return dbContextMock;
        }

        private CoursesController CoursesControllerInit(DbContextMock<CoursesDbContext.CoursesDbContext> dbContextMock)
        {
            return new CoursesController(dbContextMock.Object);
        }

        private Course[] GetInitialDbEntities()
        {
            return new Course[]
             {
                new Course() { Id = Guid.Parse("fae5eb71-a6f1-4945-8349-d3053b281aea"), Title = "AZ-900", Description = "Azure Fundamentals", StartDate = DateTime.Now },
                new Course() { Id = Guid.Parse("3863ff5c-af17-487e-a001-7e01ff0d921b"), Title = "AZ-204", Description = "Developing Solutions for Microsoft Azure", StartDate = DateTime.Now },
                new Course() { Id = Guid.Parse("637076d4-dea4-4c91-ad99-8ba7a8cedc4f"), Title = "AZ-104", Description = "Microsoft Azure Administrator", StartDate = DateTime.Now },
                new Course() { Id = Guid.Parse("5e348d74-999b-454d-960a-92ad75f7661d"), Title = "AZ-500", Description = "Microsoft Azure Security Technologies", StartDate = DateTime.Now }
            };
        }

        private void Setup()
        {
            _dbContextMock = GetDbContext(GetInitialDbEntities());
            _controller = CoursesControllerInit(_dbContextMock);
        }

        #endregion

        [Fact]
        public void GetAllCourses_Positive_ReturnAllCourses()
        {
            //arrange
            Setup();

            //act
            var result = _controller.GetAllCourses().Result;
            OkObjectResult objectResponse = Assert.IsType<OkObjectResult>(result);

            List<Course>? value = objectResponse.Value as List<Course>;

            //assert
            Assert.Equal(4, value?.Count);

        }

        [Fact]
        public void GetCourse_Positive_ReturnRelatedCourse()
        {
            //arrange
            Setup();

            //act
            var result = _controller.GetCourse(Guid.Parse("fae5eb71-a6f1-4945-8349-d3053b281aea")).Result;
            OkObjectResult objectResponse = Assert.IsType<OkObjectResult>(result);

            Course? value = objectResponse.Value as Course;

            //assert
            Assert.Equal("AZ-900", value?.Title);          

        }


        [Fact]
        public void GetCourse_Negative_ReturnBadRequest()
        {
            //arrange
            Setup();

            //act
            var result = _controller.GetCourse(Guid.Parse("d3c4b026-5445-4928-87dd-dea80089f308")).Result;            
          
            //assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public void UpdateCourse_Positive_ReturnOkResult()
        {
            //arrange
            Setup();

            Course courseToUpdate = GetInitialDbEntities()[2];

            courseToUpdate.Title = "AZ-140";
            courseToUpdate.Description = "Configuring and Operating Microsoft Azure Virtual Desktop";

            //act
            var result = _controller.UpdateCourse(Guid.Parse("637076d4-dea4-4c91-ad99-8ba7a8cedc4f"),courseToUpdate).Result;
            Course? updatedCourse = _dbContextMock.Object.Courses.Find(Guid.Parse("637076d4-dea4-4c91-ad99-8ba7a8cedc4f"));
            //assert
            Assert.Equal(courseToUpdate.Title, updatedCourse?.Title);
            Assert.Equal(courseToUpdate.Description, updatedCourse?.Description);

        }

        [Fact]
        public void UpdateCourse_Negative_ReturnBadResult()
        {
            //arrange
            Setup();

            Course courseToUpdate = GetInitialDbEntities()[2];

            courseToUpdate.Title = "AZ-140";
            courseToUpdate.Description = "Configuring and Operating Microsoft Azure Virtual Desktop";

            //act
            var result = _controller.UpdateCourse(Guid.Parse("89873496-2d1c-4261-9028-b15ba5bb7fa8"), courseToUpdate).Result;
         
            //assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public void DeleteCourse_Positive_ReturnOkResult()
        {
            //arrange
            Setup();

            //act
            var result = _controller.DeleteCourse(Guid.Parse("fae5eb71-a6f1-4945-8349-d3053b281aea")).Result;

            //assert
            Assert.Null(_dbContextMock.Object.Courses.Find(Guid.Parse("fae5eb71-a6f1-4945-8349-d3053b281aea")));

        }

        [Fact]
        public void DeleteCourse_Negative_ReturnBadResult()
        {
            //arrange
            Setup();

            //act
            var result = _controller.DeleteCourse(Guid.Parse("89873496-2d1c-4261-9028-b15ba5bb7fa8")).Result;

            //assert
            Assert.IsType<BadRequestResult>(result);

        }

        [Fact]
        public void AddCourse_Negative_ReturnBadResult()
        {
            //arrange
            Setup();

            Course newCourse = new Course() { Id = Guid.Parse("89873496-2d1c-4261-9028-b15ba5bb7fa8"), 
                                              Title = "AZ-700",
                                              Description = "Designing and Implementing Microsoft Azure Networking Solutions",
                                              StartDate = DateTime.Now
                                             };

            //act
            var result = _controller.AddCourse(newCourse).Result;

            //assert
            Assert.Equal(newCourse, _dbContextMock.Object.Courses.Find(newCourse.Id));

        }
    }
}