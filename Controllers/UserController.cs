using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Models;
using JobHuntingAssistant.Services;

namespace JobHuntingAssistant.Controllers
{
    /// <summary>
    /// Controller for user information
    /// </summary>
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Action to add a new user
        /// </summary>
        [HttpPost]
        public IActionResult AddUserInfo(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.AddUser(user);
                    return RedirectToAction("ResumeGeneration", "Home");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding user");
                    ModelState.AddModelError("", "An error occurred while adding the user.");
                }
            }
            else
            {
                ModelState.AddModelError("", "The provided data is not valid.");
            }

            return View(user);
        }

        /// <summary>
        /// Action to view a user's information
        /// </summary>
        public IActionResult ViewUserInfo(int id)
        {
            try
            {
                var user = _userService.GetUser(id);
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error viewing user");
            }

            return View();
        }

        /// <summary>
        /// Updates a user's information
        /// </summary>
        [HttpPost]
        public IActionResult UpdateUserInfo(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userService.UpdateUser(user);
                    return RedirectToAction("ViewUserInfo", new { id = user.Id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating user");
                }
            }

            return View(user);
        }

    }
}
