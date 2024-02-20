using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VirtualTeacher.Models.ViewModel.LectureViewModel;
using VirtualTeacher.Services.Contracts;

namespace VirtualTeacher.Controllers
{
    public class LecturesController : Controller
    {
        private readonly ILectureService lectureService;
        private readonly IMapper mapper;

        public LecturesController(ILectureService lectureService, IMapper mapper)
        {
            this.lectureService = lectureService;
            this.mapper = mapper;
        }

        public IActionResult Index(int id)
        {
            //var lecture = lectureService.GetById(id);

            //var viewModel = new LectureViewModel
            //{
            //    Id = lecture.Id,
            //    Title = lecture.Title,
            //    Description = lecture.Description,
            //    VideoURL = lecture.VideoURL,
            //    AssignmentId = lecture.Assignment.Id,
            //    AssignmentContent = lecture.Assignment.Content
            //};

            return View();
        }
    }
}
