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


namespace ActiveLearning.Web.Tests.Controllers
{
    [TestClass]
    public class AdminControllerTest
    {
        private Mock<CourseManager> _mockCourseManager;

        private AdminController _controller;
        private const int _partitionSize = 10;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCourseManager = new Mock<CourseManager>();

            _controller = new AdminController();
        }

        [TestMethod]
        public void ManageCourse_ExpectViewResultReturned()
        {
           
            string message = string.Empty;

            _mockCourseManager.Verify(e => e.GetAllCourses(out message), Times.AtMostOnce);

        }











    }
}
