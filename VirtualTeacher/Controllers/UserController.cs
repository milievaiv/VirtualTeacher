using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using VirtualTeacher.Models;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Constants;
using Microsoft.AspNetCore.Authorization;
using VirtualTeacher.Attributes;
using System.Security;
using VirtualTeacher.Services;
using VirtualTeacher.Models.ViewModel.UserViewModel;
using Microsoft.AspNetCore.Components.Forms;
using iTextSharp.text;
using Google.Apis.Drive.v3.Data;
using VirtualTeacher.Models.DTO.AuthenticationDTO;
using VirtualTeacher.Models.DTO.UserDTO;

namespace VirtualTeacher.Controllers
{
    [AuthorizeUsers("student, teacher")]
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

        [Route("user/profile")]
        public async Task<IActionResult> Profile()
        {
            var _user = HttpContext.User;
            var email = _user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var user = _userService.GetByEmail(email);
            //var pfpName = _user.Id + ".png";
            //byte[] imageData = _cloudStorageService.GetImageContent(pfpName, "profilePictures");

            //Image profileImage = null;
            //if (imageData != null)
            //{
            //    // Convert byte array to Image
            //    using (MemoryStream ms = new MemoryStream(imageData))
            //    {
            //        profileImage = Image.FromStream(ms);
            //    }
            //}

            EditProfileViewModel profile = new EditProfileViewModel
            {
                HasProfileImage = user.HasProfileImage,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role
            };

            return View(profile);
            //if (user == null)
            //{
            //    return NotFound(Messages.UserNotFound);
            //}
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile([FromForm] EditProfileViewModel model)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var _user = _userService.GetByEmail(email);

            //if (ModelState.IsValidField("User"))

            //    if (_user == null)
            //{
            //    return NotFound(Messages.UserNotFound);
            //}

            if (ModelState.IsValid)
            {
                _user.FirstName = model.FirstName;
                _user.LastName = model.LastName;
                _userService.Update(_user.Id, _user);
            }
            else
            {
                model.FirstName = _user.FirstName;
                model.LastName = _user.LastName;
            }

            model.HasProfileImage = _user.HasProfileImage;
            model.Email = email;
            model.Role = _user.Role;

            return View("Profile", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromForm] EditProfileViewModel model)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var _user = _userService.GetByEmail(email);

            if (user == null)
            {
                return NotFound(Messages.UserNotFound);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userService.ChangePassword(_user.Id, model.CurrentPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("CurrentPassword", "Invalid password!");
                }
            }

            model.FirstName = _user.FirstName;
            model.LastName = _user.LastName;
            model.HasProfileImage = _user.HasProfileImage;
            model.Email = email;
            model.Role = _user.Role;

            return View("Profile", model);
        }
        public async Task<IActionResult> ProfilePicture(string email)
        {
            var user = _userService.GetByEmail(email);
            var pfpName = user.Id + ".png";

            // Load profile picture data
            byte[] imageData = _cloudStorageService.GetImageContent(pfpName, "profile-pictures/");

            return File(imageData, "image/png");
        }

        [HttpPost]
        public async Task<IActionResult> ProfilePicture(IFormFile file, string _email)
        {
            var user = HttpContext.User;
            var email = user.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
            var _user = _userService.GetByEmail(email);

            //string extension = Path.GetExtension(file.FileName)?.ToLower();
            string newFileName = _user.Id + ".png";
            if (file != null && file.Length > 0)
            {
                using (var fileStream = file.OpenReadStream())
                {
                    // Use the dynamically fetched bucket name
                    await _cloudStorageService.UploadFileAsync("profile-pictures/" + newFileName, fileStream);
                }
            }

            _user.HasProfileImage = true;

            _userService.Update(_user.Id, _user);

            return RedirectToAction("Profile");
        }

        private string GetImageContentType(string imageName)
        {
            // Determine the content type based on the file extension
            string extension = Path.GetExtension(imageName)?.ToLower();

            switch (extension)
            {
                case ".jpg":
                    return "image/jpg";
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                // Add additional cases for other supported formats
                default:
                    return null; // Unsupported format
            }
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
