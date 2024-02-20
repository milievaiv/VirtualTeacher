//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Collections.Generic;
//using VirtualTeacher.Models;
//using VirtualTeacher.Repositories.Contracts;
//using VirtualTeacher.Services;
//using VirtualTeacher.Services.Contracts;

//namespace VirtualTeacherServicesTests
//{
//    [TestClass]
//    public class AssignmentServiceTests
//    {
//        [TestMethod]
//        public void Create_Returns_Created_Assignment()
//        {
//            Arrange
//           var mockAssignmentRepository = new Mock<IAssignmentRepository>();
//            var assignmentService = new AssignmentService(mockAssignmentRepository.Object);
//            var lecture = new Lecture();
//            var assignment = new Assignment();

//            mockAssignmentRepository.Setup(repo => repo.Create(lecture, assignment))
//                .Returns(assignment);

//            Act
//           var result = assignmentService.Create(lecture, assignment);

//            Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(assignment, result);
//        }

//        [TestMethod]
//        public void GetAll_Returns_All_Assignments()
//        {
//            Arrange
//           var expectedAssignments = new List<Assignment>
//           {
//                new Assignment { Id = 1, Title = "Assignment 1" },
//                new Assignment { Id = 2, Title = "Assignment 2" },
//                new Assignment { Id = 3, Title = "Assignment 3" }
//           };

//            var mockAssignmentRepository = new Mock<IAssignmentRepository>();
//            mockAssignmentRepository.Setup(repo => repo.GetAll())
//                .Returns(expectedAssignments);

//            var assignmentService = new AssignmentService(mockAssignmentRepository.Object);

//            Act
//           var result = assignmentService.GetAll();

//            Assert
//            Assert.IsNotNull(result);
//            CollectionAssert.AreEqual(expectedAssignments, result);
//        }

//        [TestMethod]
//        public void GetById_Returns_Assignment_With_Specified_Id()
//        {
//            Arrange
//           var id = 1;
//            var expectedAssignment = new Assignment
//            {
//                Id = id,
//                Title = "Assignment 1"
//            };

//            var mockAssignmentRepository = new Mock<IAssignmentRepository>();
//            mockAssignmentRepository.Setup(repo => repo.GetById(id))
//                .Returns(expectedAssignment);

//            var assignmentService = new AssignmentService(mockAssignmentRepository.Object);

//            Act
//           var result = assignmentService.GetById(id);

//            Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(expectedAssignment, result);
//        }

//        [TestMethod]
//        public void Update_Returns_Updated_Assignment()
//        {
//            Arrange
//           var assignment = new Assignment { Id = 1, Title = "Assignment 1" };

//            var mockAssignmentRepository = new Mock<IAssignmentRepository>();
//            mockAssignmentRepository.Setup(repo => repo.Update(assignment))
//                .Returns(assignment);

//            var assignmentService = new AssignmentService(mockAssignmentRepository.Object);

//            Act
//           var result = assignmentService.Update(assignment);

//            Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(assignment, result);
//        }

//        [TestMethod]
//        public void Delete_Returns_True_If_Deletion_Successful()
//        {
//            Arrange
//           var assignment = new Assignment { Id = 1, Title = "Assignment 1" };

//            var mockAssignmentRepository = new Mock<IAssignmentRepository>();
//            mockAssignmentRepository.Setup(repo => repo.Delete(assignment))
//                .Returns(true);

//            var assignmentService = new AssignmentService(mockAssignmentRepository.Object);

//            Act
//           var result = assignmentService.Delete(assignment);

//            Assert
//            Assert.IsTrue(result);
//        }

//        [TestMethod]
//        public void SubmitAssignment_Returns_Submitted_Assignment()
//        {
//            Arrange
//           var student = new Student();
//            var assignment = new Assignment();

//            var mockAssignmentRepository = new Mock<IAssignmentRepository>();
//            mockAssignmentRepository.Setup(repo => repo.SubmitAssignment(student, assignment))
//                .Returns(assignment);

//            var assignmentService = new AssignmentService(mockAssignmentRepository.Object);

//            Act
//           var result = assignmentService.SubmitAssignment(student, assignment);

//            Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(assignment, result);
//        }

//        [TestMethod]
//        public void IsAssignmentSubmitted_Returns_True_If_Assignment_Submitted_By_Student()
//        {
//            Arrange
//           var student = new Student();
//            var assignment = new Assignment();

//            var mockAssignmentRepository = new Mock<IAssignmentRepository>();
//            mockAssignmentRepository.Setup(repo => repo.IsAssignmentSubmitted(student, assignment))
//                .Returns(true);

//            var assignmentService = new AssignmentService(mockAssignmentRepository.Object);

//            Act
//           var result = assignmentService.IsAssignmentSubmitted(student, assignment);

//            Assert
//            Assert.IsTrue(result);
//        }
//    }
//}
