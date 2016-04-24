using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ActiveLearning.Web.Controllers;
using ActiveLearning.Business.Implementation;
using ActiveLearning.Business.Interface;
using ActiveLearning.DB;
using System.Web.Mvc;
using ActiveLearning.Business.Mock;
using ActiveLearning.Business.Common;

namespace ActiveLearning.Web.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        private MockCourseManager _mockCourseManager;
        private AdminController _controller;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockCourseManager = new MockCourseManager();

            _controller = new AdminController(_mockCourseManager);
        }

        [TestMethod]
        public void ManageCourse_ExpectViewResultReturnedWithCorrectCourseCount()
        {
            // Arrange
            var stubCourses = (new List<Course>
            {
                new Course()
                {
                    Sid = 1,
                     CourseName = "Andy Lau"

                },
                new Course()
                {
                    Sid = 1,
                     CourseName = "Donnie Yen"
                }
            }).AsQueryable();

            _mockCourseManager.MockCourses = stubCourses;

            // Act
            var view = (ViewResult)_controller.ManageCourse();

            // Assert  
            var Model = (IEnumerable<Course>)view.Model;
            Assert.IsTrue(Model.Count() == 2);
        }

        [TestMethod]
        public void ManageCourse_ExpectError()
        {
            // Arrange

            // Act
            var view = (ViewResult)_controller.ManageCourse();

            // Assert  
            Assert.IsTrue(view.ViewData["Message"].ToString() == "The List is empty !");
        }

        [TestMethod]
        public void CreateCourse_NoError()
        {
            // Arrange
            Course _course = new Course();
            _mockCourseManager.MockCourse = _course;

            // Act
            var action = (RedirectToRouteResult)_controller.CreateCourse(_course);

            // Assert  
            Assert.AreEqual("ManageCourse", action.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateCourse_NullParameter()
        {
            // Arrange

            // Act
            var view = (ViewResult)_controller.CreateCourse(null);

            // Assert  
            Assert.IsTrue(view.ViewData["Message"].ToString() == Constants.ValueIsEmpty(Constants.Course));
        }

        [TestMethod]
        public void CreateCourse_EF_Failure()
        {
            // Arrange
            _mockCourseManager.MockCourse = null;

            // Act
            var view = (ViewResult)_controller.CreateCourse(new Course());

            // Assert  
            Assert.IsTrue(view.ViewData["Message"].ToString() == Constants.OperationFailedDuringAddingValue(Constants.Course));
        }


















    }
}
