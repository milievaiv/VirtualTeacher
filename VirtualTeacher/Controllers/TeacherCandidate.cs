using Microsoft.AspNetCore.Mvc;

namespace VirtualTeacher.Controllers
{
    public class TeacherCandidate : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Candidature()
        {
            return View();
        }
    }
}
