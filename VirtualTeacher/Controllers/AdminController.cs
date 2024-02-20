using Microsoft.AspNetCore.Mvc;

namespace VirtualTeacher.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
