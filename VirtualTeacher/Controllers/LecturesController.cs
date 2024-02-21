using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using VirtualTeacher.Attributes;
using VirtualTeacher.Models;
using VirtualTeacher.Models.ViewModel.LectureViewModel;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ILectureService lectureService;
        private readonly ITeacherService teacherService;
        private readonly ICourseService courseService;
        private readonly IMapper mapper;

        public LecturesController(
            ILectureService lectureService,
            ITeacherService teacherService,
            ICourseService courseService,
            IMapper mapper)
        {
            this.lectureService = lectureService;
            this.teacherService = teacherService;
            this.courseService = courseService;
            this.mapper = mapper;
        }

        [HttpGet]
        [AuthorizeUsers("teacher")]
        public IActionResult CreateLecture()
        {
            var courses = courseService.GetAll().Select(c => new { c.Id, c.Title }).ToList();

            var viewModel = new LectureCreateViewModel
            {
                // Populate the SelectList for the dropdown
                Courses = new SelectList(courses, "Id", "Title")
            };

            return View(viewModel);
        }

       
        [HttpPost]
        [AuthorizeUsers("teacher")]
        public async Task<IActionResult> CreateLecture(LectureCreateViewModel model)
        {
            ModelState.Remove("Courses");
            if (ModelState.IsValid)
            {
                var lecture = new Lecture
                {
                    CourseId = model.CourseId,
                    Title = model.Title,
                    Description = model.Description,
                    VideoURL = model.VideoURL
                };

                var course = courseService.GetById(model.CourseId);

                lectureService.Create(course, lecture);

                
                return RedirectToAction("Index", "Courses");
            }

            // If we get here, something was wrong with the form data
            // Repopulate the Courses SelectList to ensure the dropdown is correctly populated on return to the form
            var courses = courseService.GetAll().Select(c => new { c.Id, c.Title }).ToList();
            model.Courses = new SelectList(courses, "Id", "Title");
           
            return View(model);
        }
    }
}
