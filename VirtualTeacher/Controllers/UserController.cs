using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using VirtualTeacher.Models;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Constants;
using Microsoft.AspNetCore.Authorization;
using VirtualTeacher.Attributes;
using System.Security;

namespace VirtualTeacher.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly CloudStorageService _cloudStorageService;
        //private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IUserService userService, CloudStorageService cloudStorageService)
        {
            _userService = userService;
            _cloudStorageService = cloudStorageService;
        }

        // GET: /User/Profile
        [AuthorizeUsers("student, teacher")]

        [Route("user/profile")]
        public async Task<IActionResult> Profile()
        {

            // var email = User.FindFirst(ClaimTypes.Email)?.Value; 
            
          

            var user = _userService.GetByEmail("martin23@gmail.com");
            if (user == null)
            {
                return NotFound(Messages.UserNotFound);
            }

                return View(user);
        }

        // GET: /User/ChangePassword
        //public IActionResult ChangePassword()
        //{
        //    return View();
        //}

        //// POST: /User/ChangePassword
        //[HttpPost]
        //public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var email = User.FindFirst(ClaimTypes.Email)?.Value; // Get the email from the JWT token
        //    var user = await _userService.GetByEmail(email);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        await _userService.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
        //        return RedirectToAction(nameof(Profile));
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View(model);
        //    }
        //}

        //// GET: /User/UpdateProfile
        //public async Task<IActionResult> UpdateProfile()
        //{
        //    var email = User.FindFirst(ClaimTypes.Email)?.Value; // Get the email from the JWT token
        //    var user = await _userService.GetByEmail(email);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(user);
        //}

        //// POST: /User/UpdateProfile
        //[HttpPost]
        //public async Task<IActionResult> UpdateProfile(UserProfileUpdateDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    var email = User.FindFirst(ClaimTypes.Email)?.Value; // Get the email from the JWT token
        //    var user = await _userService.GetByEmail(email);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        user.FirstName = model.FirstName;
        //        user.LastName = model.LastName;
        //        // Update other properties as needed

        //        await _userService.Update(user.Id, user);
        //        return RedirectToAction(nameof(Profile));
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View(model);
        //    }
        //}

        //public IActionResult UpdateProfilePicture()
        //{
        //    return View();
        //}

        //// POST: /User/UpdateProfilePicture
        //[HttpPost]
        //public async Task<IActionResult> UpdateProfilePicture(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //    {
        //        ModelState.AddModelError("", "Please select a file.");
        //        return View();
        //    }

        //    var email = User.FindFirst(ClaimTypes.Email)?.Value; // Get the email from the JWT token
        //    var user = _userService.GetByEmail(email);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        // Upload the file to cloud storage service
        //        // Example: await _cloudStorageService.UploadProfilePicture(user.Id, file);

        //        // Update user's profile picture URL in the database
        //        // Example: user.ProfilePictureUrl = "<URL_of_uploaded_picture>";
        //        // await _userService.Update(user.Id, user);

        //        return RedirectToAction(nameof(Profile));
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View();
        //    }
        //}
    }
}
