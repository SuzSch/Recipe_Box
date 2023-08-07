using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RecipeBox.Models;
using System.Threading.Tasks;
using RecipeBox.ViewModels;

namespace RecipeBox.AddControllers
{
    public class AccountController: AddControllers
    {
        private readonly RecipeBoxContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManger;

        public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RecipeBoxContext db)
        {
            _userManager = userManager;
            _signInManger = signInManager;
            _db = db;
        }
        public ActionResult Index()
        {
            return View();
        } 
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register (RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                ApplicationUser user = new ApplicationUser { userName = model.Email}:
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
            }
        }

    }
}