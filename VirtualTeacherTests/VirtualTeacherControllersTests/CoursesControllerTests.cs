using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using VirtualTeacher.Controllers;
using VirtualTeacher.Models;
using VirtualTeacher.Models.ViewModel.CourseViewModel;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Tests.Controllers
{
    [TestClass]
    public class CoursesControllerTests
    {
        private CoursesController _controller;
        private Mock<ICourseService> _courseServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IStudentService> _studentServiceMock;

        [TestInitialize]
        public void Setup()
        {
            _courseServiceMock = new Mock<ICourseService>();
            _mapperMock = new Mock<IMapper>();
            _studentServiceMock = new Mock<IStudentService>();
            _controller = new CoursesController(
                _courseServiceMock.Object,
                null, // Pass null for other services not used in these tests
                null,
                null,
                null,
                null,
                _mapperMock.Object,
                null // Pass null for CloudStorageService
            );

            // Setup course service to return an empty list by default
            _courseServiceMock.Setup(service => service.GetAll()).Returns(new List<Course>());
        }

        [TestMethod]
        public void Create_GET_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
