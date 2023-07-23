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
        /// GET: /User/Login
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
        /// POST: /User/Login
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
                ModelState.AddModelError("", "Invalid email or password.");
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

        /// <summary>
        /// POST: /User/Logout
        /// </summary>
        public async Task<IActionResult> Logout()
        {
            // Action to log a user out
            // Redirect to the home page 

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "User");
        }

        /// <summary>
        /// GET: /User/SignUp
        /// </summary>
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// POST: /User/SignUp
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new user
                var user = new User { Email = model.Email };

                // Hash the password and store it in the user object
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

                // Add the user to the database
                await _userService.AddUser(user);

                // Redirect the user to the login page
                return RedirectToAction("Login", "User");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// GET: /User/Edit
        /// </summary>
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        /// <summary>
        /// POST: /User/Edit
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUser(user);
                if (result)
                {
                    return RedirectToAction("ViewUserInfo", new { id = user.Id });
                }
                else
                {
                    ModelState.AddModelError("", "An error occurred while updating the user.");
                }
            }

            return View(user);
        }

    }
}
