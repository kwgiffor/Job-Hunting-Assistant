using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Models;
using JobHuntingAssistant.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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
        /// Action to display the login page
        /// </summary>
        [HttpGet]
        public IActionResult Login()
        {
            // Action to display the login page
            // If the user is already authenticated, redirect them to the home page
            // Otherwise, show the login form

            // If the user is already authenticated, redirect them to the home page
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            // Otherwise, show the login form
            return View();
        }


        /// <summary>
        /// Action to log a user in
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // Action to log a user in
            // If successful, redirect to the resume generation page
            // If not, display an error message
            // If the user is invalid, display an error message

            // If the user is invalid, display an error message
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // If the credentials are invalid, display an error message
            if (!_userService.ValidateUserCredentials(model.Email, model.Password))
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            // Create a new list of claims
            var claims = new List<Claim>
            {
                // Add the user's email as a claim
                new Claim(ClaimTypes.Email, model.Email),
            };

            // Create a new ClaimsIdentity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            
            // Write the user's email to the cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            // Action to log a user out
            // Redirect to the home page 

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }



        /// <summary>
        /// Action to add a new user
        /// </summary>
        [HttpPost]
        public IActionResult AddUserInfo(User user)
        {
            // Add the user to the database
            // If successful, redirect to the resume generation page
            // If not, display an error message
            // If the user is invalid, display an error message

            Console.WriteLine($" UserController User: {user}");
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
            // Get the user's information
            // If successful, display the information
            // If not, display an error message

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
            // Update the user's information
            // If successful, redirect to the user's information page
            // If not, display an error message
            // If the user is invalid, display an error message

            Console.WriteLine($" UserController User: {user}");
            
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
