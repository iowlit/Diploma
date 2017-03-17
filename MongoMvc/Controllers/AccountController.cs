using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MongoMvc.Model;
using MongoMvc.Repository;


namespace MongoMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _UserRepository;
        public AccountController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserView model)
        {
            if (ModelState.IsValid)
            {
                User user = await _UserRepository.GetUserAsync(model.Email, model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некоректні вхідні дані");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserView model)
        {
            if (ModelState.IsValid)
            {
                User user = await _UserRepository.GetUserAsync(model.Email);
                if (user == null)
                {                    
                    await _UserRepository.AddAsync(new User(new UserView { Email = model.Email, Password = model.Password }));                    

                    await Authenticate(model.Email); 

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некоректні вхідні дані");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {            
            var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
                    };
            
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.Authentication.SignInAsync("Cookies", new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }
    }
}