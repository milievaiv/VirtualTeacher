using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Helpers;
using VirtualTeacher.Attributes;
using VirtualTeacher.Exceptions;
using VirtualTeacher.Models;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.QueryParameters;
using VirtualTeacher.Models.Search;
using VirtualTeacher.Models.ViewModel.AdminViewModel;
using VirtualTeacher.Models.ViewModel.CourseViewModel;
using VirtualTeacher.Models.ViewModel.UserViewModel;
using VirtualTeacher.Services;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers
{
    [AuthorizeUsers("admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAdminService _adminService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;
        public AdminController(IUserService _userService, IAdminService _adminService, ITeacherService _teacherService,
            IStudentService _studentService, ICourseService _courseService)
        {
            this._userService = _userService;
            this._adminService = _adminService;
            this._teacherService = _teacherService;
            this._studentService = _studentService;
            this._courseService = _courseService;
        }

        public IActionResult Index()
        {
            int total_admins = _adminService.GetAll().Count;
            int total_students = _studentService.GetAll().Count;
            int total_teachers = _teacherService.GetAll().Count;
            int total_courses = _courseService.GetAll().Count;
            int total_enrollments = _courseService.GetAllCourseEnrollments();
            IList<Course> recentCourses = _courseService.GetAll().Take(7).ToList();
            IList<Log> recentLogs = _adminService.Logs().Take(7).ToList();

            DashboardViewModel dashboardViewModel = new DashboardViewModel
            {
                TotalAdmins = total_admins,
                TotalStudents = total_students,
                TotalCourses = total_courses,
                TotalEnrollments = total_enrollments,
                TotalTeachers = total_teachers,
                RecentCourses = recentCourses,
                RecentLogs = recentLogs
            };

            return View(dashboardViewModel);
        }

        #region Course Methods

        public IActionResult DeleteCourse(CoursesViewModel viewModel, int courseId, int page, int pageSize = 7, string Property = null, string Value = null)
        {
            try
            {
                var course = _courseService.GetById(courseId);
                _courseService.Delete(course);

                viewModel.CurrentPage = page;
                viewModel.PageSize = pageSize;

                viewModel.SearchModel = new Search();

                viewModel.SearchModel.Property = Property;
                viewModel.SearchModel.Value = Value;

                viewModel = GenerateCourseView(viewModel, page, pageSize);

                var _user = HttpContext.User;
                var name = _user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

                _adminService.CreateLog(new Log { Message = $"{name} has deleted a course with id {courseId}." });

                return View("Courses", viewModel);
            }
            catch
            {
                return View("Courses");
            }
        }

        [HttpPost]
        public IActionResult MarkDraft(int courseId, int page, int pageSize = 7, string Property = null, string Value = null)
        {
            try
            {
                _courseService.MarkAsDraft(courseId);

                Search searchCourse = new Search
                {
                    Property = Property,
                    Value = Value
                };
                CoursesViewModel viewModel = new CoursesViewModel
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    SearchModel = searchCourse
                };

                viewModel = GenerateCourseView(viewModel, page, pageSize);

                var _user = HttpContext.User;
                var name = _user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

                _adminService.CreateLog(new Log { Message = $"{name} has marked a course with id {courseId} as draft." });

                return View("Courses", viewModel);
            }
            catch
            {
                return View("Courses");
            }

        }

        [HttpPost]
        public IActionResult MarkPublic(int courseId, int page, int pageSize = 7, string Property = null, string Value = null)
        {
            try
            {
                _courseService.PublicizeCourse(courseId);

                Search searchCourse = new Search
                {
                    Property = Property,
                    Value = Value
                };
                CoursesViewModel viewModel = new CoursesViewModel
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    SearchModel = searchCourse
                };

                viewModel = GenerateCourseView(viewModel, page, pageSize);

                var _user = HttpContext.User;
                var name = _user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

                _adminService.CreateLog(new Log { Message = $"{name} has marked a course with id {courseId} as public." });

                return View("Courses", viewModel);
            }
            catch
            {
                return View("Courses");
            }

        }

        [HttpPost]
        public IActionResult SearchCourses(CoursesViewModel viewModel)
        {
            //SearchUser searchUser = new SearchUser
            //{
            //    Property = viewModel.SearchModel.Property,
            //    Value = viewModel.SearchModel.Value
            //};
            //UserViewModel viewModel = new UserViewModel
            //{
            //    CurrentPage = page,
            //    PageSize = pageSize,
            //    SearchModel = searchUser
            //};
            viewModel = GenerateCourseView(viewModel);

            return View("Courses", viewModel);
        }
        public IActionResult Courses(CoursesViewModel viewModel, int page = 1, int pageSize = 7, string Property = null, string Value = null)
        {
            // Update the SearchModel based on the provided parameters

            if (viewModel.SearchModel == null)
            {
                viewModel.SearchModel = new Search { Property = Property, Value = Value };
            }

            viewModel = GenerateCourseView(viewModel, page, pageSize);

            return View(viewModel);
        }

        public CoursesViewModel GenerateCourseView(CoursesViewModel viewModel, int page = 1, int pageSize = 7)
        {
            string sortOrder = viewModel.SortOrder;
            string sortBy = viewModel.SortBy;

            CourseQueryParameters postQueryParameters = new CourseQueryParameters();

            switch (viewModel.SearchModel.Property)
            {
                case "Title":
                    postQueryParameters.Title = viewModel.SearchModel.Value;
                    break;
                case "Creator":
                    postQueryParameters.Creator = viewModel.SearchModel.Value;
                    break;
                case "Topic":
                    postQueryParameters.CourseTopic = viewModel.SearchModel.Value;
                    break;
            }


            var filteredCourses= _courseService.FilterBy(postQueryParameters);

            switch (sortBy)
            {
                case "Id":
                    filteredCourses = sortOrder == "asc" ? filteredCourses.OrderBy(c => c.Id).ToList() : filteredCourses.OrderByDescending(c => c.Id).ToList();
                    break;
                case "Creator":
                    filteredCourses = sortOrder == "asc" ? filteredCourses.OrderBy(c => c.Creator.Email).ToList() : filteredCourses.OrderByDescending(c => c.Creator.Email).ToList();
                    break;
                case "Title":
                    filteredCourses = sortOrder == "asc" ? filteredCourses.OrderBy(c => c.Title).ToList() : filteredCourses.OrderByDescending(c => c.Title).ToList();
                    break;
                case "Lectures_Count":
                    filteredCourses = sortOrder == "asc" ? filteredCourses.OrderBy(c => c.Lectures.Count).ToList() : filteredCourses.OrderByDescending(c => c.Lectures.Count).ToList();
                    break;
                case "Topic":
                    filteredCourses = sortOrder == "asc" ? filteredCourses.OrderBy(c => c.CourseTopic.Topic).ToList() : filteredCourses.OrderByDescending(c => c.CourseTopic.Topic).ToList();
                    break;
                case "Start_Date":
                    filteredCourses = sortOrder == "asc" ? filteredCourses.OrderBy(c => c.StartDate).ToList() : filteredCourses.OrderByDescending(c => c.StartDate).ToList();
                    break;
                case "IsPublic":
                    filteredCourses = sortOrder == "asc" ? filteredCourses.OrderBy(c => c.IsPublic).ToList() : filteredCourses.OrderByDescending(c => c.IsPublic).ToList();
                    break;
                    // Add cases for other columns as needed
            }

            viewModel.Courses = filteredCourses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            // Update pagination information
            viewModel.CurrentPage = page;
            viewModel.PageSize = pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(filteredCourses.Count / (double)pageSize);

            return viewModel;
        }
        #endregion

        #region Teacher Methods
        [HttpPost]
        public IActionResult DeleteTeacher(TeacherViewModel viewModel, int teacherId, int page, int pageSize = 7, string Property = null, string Value = null)
        {
            try
            {
                _teacherService.Delete(teacherId);

                viewModel.CurrentPage = page;
                viewModel.PageSize = pageSize;

                viewModel.SearchModel = new Search();

                viewModel.SearchModel.Property = Property;
                viewModel.SearchModel.Value = Value;

                viewModel = GenerateTeacherView(viewModel, page, pageSize);

                var _user = HttpContext.User;
                var name = _user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

                _adminService.CreateLog(new Log { Message = $"{name} has deleted a teacher with id {teacherId}." });


                return View("Teachers", viewModel);
            }
            catch
            {
                return View("Teachers");
            }
        }

        [HttpPost]
        public IActionResult SearchTeachers(TeacherViewModel viewModel)
        {
            //SearchUser searchUser = new SearchUser
            //{
            //    Property = viewModel.SearchModel.Property,
            //    Value = viewModel.SearchModel.Value
            //};
            //UserViewModel viewModel = new UserViewModel
            //{
            //    CurrentPage = page,
            //    PageSize = pageSize,
            //    SearchModel = searchUser
            //};
            viewModel = GenerateTeacherView(viewModel);

            return View("Teachers", viewModel);
        }
        [HttpPost]
        public IActionResult ApproveTeacher(string email)
        {
            bool approvalSuccess = true;
            try
            {
                var approvedTeacher = _adminService.ApproveTeacher(email);

                var _user = HttpContext.User;
                var name = _user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

                _adminService.CreateLog(new Log { Message = $"{name} has approved a teacher candidate with email {email}." });
            }
            catch (Exception)
            {
                approvalSuccess = false;
            }
            TempData["approvalSuccess"] = approvalSuccess;


            return RedirectToAction("Teachers");
        }
        public IActionResult Teachers(TeacherViewModel viewModel, int page = 1, int pageSize = 7, string Property = null, string Value = null)
        {
            // Update the SearchModel based on the provided parameters

            if (viewModel.SearchModel == null)
            {
                viewModel.SearchModel = new Search { Property = Property, Value = Value };
            }

            viewModel = GenerateTeacherView(viewModel, page, pageSize);

            return View(viewModel);
        }
        public TeacherViewModel GenerateTeacherView(TeacherViewModel viewModel, int page = 1, int pageSize = 7)
        {
            string sortOrder = viewModel.SortOrder;
            string sortBy = viewModel.SortBy;

            UserQueryParameters userQueryParameters = new UserQueryParameters();

            switch (viewModel.SearchModel.Property)
            {
                case "Email":
                    userQueryParameters.Email = viewModel.SearchModel.Value;
                    break;
                case "FirstName":
                    userQueryParameters.FirstName = viewModel.SearchModel.Value;
                    break;
                case "LastName":
                    userQueryParameters.LastName = viewModel.SearchModel.Value;
                    break;
            }

            var filteredTeachers = _teacherService.FilterBy(userQueryParameters);

            switch (sortBy)
            {
                case "Id":
                    filteredTeachers = sortOrder == "asc" ? filteredTeachers.OrderBy(t => t.Id).ToList() : filteredTeachers.OrderByDescending(t => t.Id).ToList();
                    break;
                case "Email":
                    filteredTeachers = sortOrder == "asc" ? filteredTeachers.OrderBy(t => t.Email).ToList() : filteredTeachers.OrderByDescending(t => t.Email).ToList();
                    break;
                case "FirstName":
                    filteredTeachers = sortOrder == "asc" ? filteredTeachers.OrderBy(t => t.FirstName).ToList() : filteredTeachers.OrderByDescending(t => t.FirstName).ToList();
                    break;
                case "LastName":
                    filteredTeachers = sortOrder == "asc" ? filteredTeachers.OrderBy(t => t.LastName).ToList() : filteredTeachers.OrderByDescending(t => t.LastName).ToList();
                    break;                
                case "Courses_Created":
                    filteredTeachers = sortOrder == "asc" ? filteredTeachers.OrderBy(t => t.CoursesCreated.Count).ToList() : filteredTeachers.OrderByDescending(t => t.CoursesCreated.Count).ToList();
                    break;
                case "IsDeleted":
                    filteredTeachers = sortOrder == "asc" ? filteredTeachers.OrderBy(t => t.IsDeleted).ToList() : filteredTeachers.OrderByDescending(t => t.IsDeleted).ToList();
                    break;
                    // Add cases for other columns as needed
            }

            viewModel.Teachers = filteredTeachers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            // Update pagination information
            viewModel.CurrentPage = page;
            viewModel.PageSize = pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(filteredTeachers.Count / (double)pageSize);

            return viewModel;
        }
        #endregion

        #region Student Methods
        [HttpPost]
        public IActionResult DeleteStudent(StudentsViewModel viewModel, int studentId, int page, int pageSize = 7, string Property = null, string Value = null)
        {
            try
            {
                _studentService.Delete(studentId);

                viewModel.CurrentPage = page;
                viewModel.PageSize = pageSize;

                viewModel.SearchModel = new Search();

                viewModel.SearchModel.Property = Property;
                viewModel.SearchModel.Value = Value;

                viewModel = GenerateStudentsView(viewModel, page, pageSize);

                var _user = HttpContext.User;
                var name = _user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

                _adminService.CreateLog(new Log { Message = $"{name} has deleted a student with id {studentId}." });

                return View("Students", viewModel);
            }
            catch
            {
                return View("Students");
            }
        }

        [HttpPost]
        public IActionResult SearchStudents(StudentsViewModel viewModel)
        {
            //SearchUser searchUser = new SearchUser
            //{
            //    Property = viewModel.SearchModel.Property,
            //    Value = viewModel.SearchModel.Value
            //};
            //UserViewModel viewModel = new UserViewModel
            //{
            //    CurrentPage = page,
            //    PageSize = pageSize,
            //    SearchModel = searchUser
            //};
            viewModel = GenerateStudentsView(viewModel);

            return View("Students", viewModel);
        }
        public IActionResult Students(StudentsViewModel viewModel, int page = 1, int pageSize = 7, string Property = null, string Value = null)
        {
            // Update the SearchModel based on the provided parameters

            if (viewModel.SearchModel == null)
            {
                viewModel.SearchModel = new Search { Property = Property, Value = Value };
            }

            viewModel = GenerateStudentsView(viewModel, page, pageSize);

            return View(viewModel);
        }
        public StudentsViewModel GenerateStudentsView(StudentsViewModel viewModel, int page = 1, int pageSize = 7)
        {
            string sortOrder = viewModel.SortOrder;
            string sortBy = viewModel.SortBy;

            UserQueryParameters userQueryParameters = new UserQueryParameters();

            switch (viewModel.SearchModel.Property)
            {
                case "Email":
                    userQueryParameters.Email = viewModel.SearchModel.Value;
                    break;
                case "FirstName":
                    userQueryParameters.FirstName = viewModel.SearchModel.Value;
                    break;
                case "LastName":
                    userQueryParameters.LastName = viewModel.SearchModel.Value;
                    break;
            }

            var filteredStudents = _studentService.FilterBy(userQueryParameters);

            switch (sortBy)
            {
                case "Id":
                    filteredStudents = sortOrder == "asc" ? filteredStudents.OrderBy(t => t.Id).ToList() : filteredStudents.OrderByDescending(t => t.Id).ToList();
                    break;
                case "Email":
                    filteredStudents = sortOrder == "asc" ? filteredStudents.OrderBy(t => t.Email).ToList() : filteredStudents.OrderByDescending(t => t.Email).ToList();
                    break;
                case "FirstName":
                    filteredStudents = sortOrder == "asc" ? filteredStudents.OrderBy(t => t.FirstName).ToList() : filteredStudents.OrderByDescending(t => t.FirstName).ToList();
                    break;
                case "LastName":
                    filteredStudents = sortOrder == "asc" ? filteredStudents.OrderBy(t => t.LastName).ToList() : filteredStudents.OrderByDescending(t => t.LastName).ToList();
                    break;
                case "Courses_Enrolled_In":
                    filteredStudents = sortOrder == "asc" ? filteredStudents.OrderBy(t => t.EnrolledCourses.Count).ToList() : filteredStudents.OrderByDescending(t => t.EnrolledCourses.Count).ToList();
                    break;
                case "IsDeleted":
                    filteredStudents = sortOrder == "asc" ? filteredStudents.OrderBy(t => t.IsDeleted).ToList() : filteredStudents.OrderByDescending(t => t.IsDeleted).ToList();
                    break;
                    // Add cases for other columns as needed
            }

            viewModel.Students = filteredStudents
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            // Update pagination information
            viewModel.CurrentPage = page;
            viewModel.PageSize = pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(filteredStudents.Count / (double)pageSize);

            return viewModel;
        }

        #endregion

        #region Admin Methods

        [HttpPost]
        public IActionResult SearchAdmins(AdminsViewModel viewModel)
        {
            //SearchUser searchUser = new SearchUser
            //{
            //    Property = viewModel.SearchModel.Property,
            //    Value = viewModel.SearchModel.Value
            //};
            //UserViewModel viewModel = new UserViewModel
            //{
            //    CurrentPage = page,
            //    PageSize = pageSize,
            //    SearchModel = searchUser
            //};
            viewModel = GenerateAdminsView(viewModel);

            return View("Admins", viewModel);
        }
        public IActionResult Admins(AdminsViewModel viewModel, int adminId, int page = 1, int pageSize = 7, string Property = null, string Value = null)
        {
            // Update the SearchModel based on the provided parameters

            if (viewModel.SearchModel == null)
            {
                viewModel.SearchModel = new Search { Property = Property, Value = Value };
            }

            viewModel = GenerateAdminsView(viewModel, page, pageSize);

            return View(viewModel);
        }
        public AdminsViewModel GenerateAdminsView(AdminsViewModel viewModel, int page = 1, int pageSize = 7)
        {
            string sortOrder = viewModel.SortOrder;
            string sortBy = viewModel.SortBy;

            UserQueryParameters userQueryParameters = new UserQueryParameters();

            switch (viewModel.SearchModel.Property)
            {
                case "Email":
                    userQueryParameters.Email = viewModel.SearchModel.Value;
                    break;
                case "FirstName":
                    userQueryParameters.FirstName = viewModel.SearchModel.Value;
                    break;
                case "LastName":
                    userQueryParameters.LastName = viewModel.SearchModel.Value;
                    break;
            }

            var filteredAdmins = _adminService.FilterBy(userQueryParameters);

            switch (sortBy)
            {
                case "Id":
                    filteredAdmins = sortOrder == "asc" ? filteredAdmins.OrderBy(t => t.Id).ToList() : filteredAdmins.OrderByDescending(t => t.Id).ToList();
                    break;
                case "Email":
                    filteredAdmins = sortOrder == "asc" ? filteredAdmins.OrderBy(t => t.Email).ToList() : filteredAdmins.OrderByDescending(t => t.Email).ToList();
                    break;
                case "FirstName":
                    filteredAdmins = sortOrder == "asc" ? filteredAdmins.OrderBy(t => t.FirstName).ToList() : filteredAdmins.OrderByDescending(t => t.FirstName).ToList();
                    break;
                case "LastName":
                    filteredAdmins = sortOrder == "asc" ? filteredAdmins.OrderBy(t => t.LastName).ToList() : filteredAdmins.OrderByDescending(t => t.LastName).ToList();
                    break;
                    // Add cases for other columns as needed
            }

            viewModel.Admins = filteredAdmins
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Update pagination information
            viewModel.CurrentPage = page;
            viewModel.PageSize = pageSize;
            viewModel.TotalPages = (int)Math.Ceiling(filteredAdmins.Count / (double)pageSize);

            return viewModel;
        }
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public IActionResult RegisterAdmin([FromForm] RegisterDto _model)
        {
            RegisterDto model = _model;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Register");
                }
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");

                    return View("Register");
                }

                _ = _adminService.Register(model);
                bool registrationSuccess = true;
                TempData["registrationSuccess"] = registrationSuccess;

                var _user = HttpContext.User;
                var name = _user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value;

                _adminService.CreateLog(new Log { Message = $"{name} has created a new admin {_model.FirstName} {_model.LastName} with email {_model.Email}." });

                return View("Register");
            }
            catch (DuplicateEntityException)
            {
                ModelState.AddModelError("Email", "This email is already taken.");
            }
            return View("Register");

        }
        #endregion
        //public IActionResult GetCourses(int page = 1, int pageSize = 5, string sortBy = "Id", string sortOrder = "asc")
        //{
        //    // Retrieve all courses
        //    var courses = _courseService.GetAll();

        //    // Apply sorting
        //    courses = ApplySorting(courses, sortBy, sortOrder);

        //    // Paginate the sorted courses
        //    int totalCourses = courses.Count;
        //    int skip = (page - 1) * pageSize;
        //    courses = courses.Skip(skip).Take(pageSize).ToList();

        //    // Calculate total pages
        //    int totalPages = (int)Math.Ceiling((double)totalCourses / pageSize);

        //    var viewModel = new CoursesViewModel
        //    {
        //        Courses = courses,
        //        CurrentPage = page,
        //        PageSize = pageSize,
        //        TotalPages = totalPages
        //    };

        //    // Return courses and pagination information as JSON data
        //    return Json(viewModel);
        //}

        //private IList<Course> ApplySorting(IList<Course> courses, string sortBy, string sortOrder)
        //{
        //    switch (sortBy.ToLower())
        //    {
        //        case "title":
        //            courses = sortOrder.ToLower() == "asc" ? courses.OrderBy(c => c.Title).ToList() : courses.OrderByDescending(c => c.Title).ToList();
        //            break;
        //        case "creator":
        //            courses = sortOrder.ToLower() == "asc" ? courses.OrderBy(c => c.Creator.Email).ToList() : courses.OrderByDescending(c => c.Creator.Email).ToList();
        //            break;
        //        case "startdate":
        //            courses = sortOrder.ToLower() == "asc" ? courses.OrderBy(c => c.StartDate).ToList() : courses.OrderByDescending(c => c.StartDate).ToList();
        //            break;
        //        case "ispublic":
        //            courses = sortOrder.ToLower() == "asc" ? courses.OrderBy(c => c.IsPublic).ToList() : courses.OrderByDescending(c => c.IsPublic).ToList();
        //            break;
        //        default:
        //            courses = sortOrder.ToLower() == "asc" ? courses.OrderBy(c => c.Id).ToList() : courses.OrderByDescending(c => c.Id).ToList();
        //            break;
        //    }

        //    return courses;
        //}

    }
}
