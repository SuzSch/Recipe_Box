using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
    [Authorize]
    public class RecipesController : Controller
    {
        private readonly RecipeBoxContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public RecipesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.IngId = new SelectList(_db.Ings, "IngId", "Name");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            List<Recipe> userRecipes = _db.Recipes
                                    .Where(entry => entry.User.Id == currentUser.Id)
                                    .OrderByDescending(entry => entry.Rating)
                                    .ToList();
            ViewBag.Title = "Recipes List";
            return View(userRecipes);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create Recipe";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return View(recipe);
            }
            else
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
                recipe.User = currentUser;
                _db.Recipes.Add(recipe);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public async Task<ActionResult> Details(int id)
        {
            ViewBag.Title = "Recipe Details";
            ViewBag.IngId = new SelectList(_db.Ings, "IngId", "Name");
            Recipe thisRecipe = _db.Recipes
            .Include(recipe => recipe.JoinEntities)
            .ThenInclude(join => join.Ing)
            .FirstOrDefault(recipe => recipe.RecipeId == id);

            //Then set current user (see line 51-52)
            // then do branching logic to see View or redirect if current user does not match user for recipe
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == thisRecipe.User)
            {
            return View(thisRecipe);
            }
            else 
            {
            return RedirectToAction ("BozoCatcher");
            }
        }

        public async Task<ActionResult> BozoCatcher()
        {
            ViewBag.Title = "Unauthorized!! :X";
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            return View(currentUser);
        }

        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Title = "Edit Recipe";
            Recipe modelRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == modelRecipe.User)
            {
            return View(modelRecipe);
            }
            else
            {
            return RedirectToAction ("BozoCatcher");   
            }
        }

        [HttpPost]
        public ActionResult Edit(Recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return View(recipe);
            }
            else
            {
                _db.Recipes.Update(recipe);
                _db.SaveChanges();
                return RedirectToAction("Details", new { id = recipe.RecipeId });
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.Title = "Delete Recipe";
            Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser == thisRecipe.User)
            {
            return View(thisRecipe);
            }
            else
            {
            return RedirectToAction("BozoCatcher");
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe thisRecipe = _db.Recipes.FirstOrDefault(recipe => recipe.RecipeId == id);
            _db.Recipes.Remove(thisRecipe);
            _db.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        public ActionResult AddIng(Recipe recipe, int ingId)
        {
            #nullable enable
            RecipeIng? joinEntity = _db.RecipeIngs.FirstOrDefault(join => (join.IngId == ingId && join.RecipeId == recipe.RecipeId));
            #nullable disable
            if (joinEntity == null && ingId != 0)
            {
                _db.RecipeIngs.Add(new RecipeIng() { IngId = ingId, RecipeId = recipe.RecipeId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new { id = recipe.RecipeId });
        }

        [HttpPost]
        public ActionResult DeleteJoin(int joinId)
        {
            RecipeIng joinEntry = _db.RecipeIngs.FirstOrDefault(joinEntry => joinEntry.RecipeIngId == joinId);
            _db.RecipeIngs.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Details", new {id = joinEntry.RecipeId});
        }

        [HttpPost]
        public async Task<ActionResult> SortByIng(int ingId)
        {
            ViewBag.IngId = new SelectList(_db.Ings, "IngId", "Name");
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
            List<RecipeIng> entries = _db.RecipeIngs
                                    .Where(entry => entry.IngId == ingId)                                    
                                    .Include(entry => entry.Recipe)  
                                    .Where(entry => entry.Recipe.User.Id == currentUser.Id)                                
                                    .ToList();
            Ing ingredient = _db.Ings.FirstOrDefault(entry => entry.IngId == ingId);
            string chosenOne = ingredient.Name;
            ViewBag.Ingredient = chosenOne;
   
            return View(entries);
        }

        // public ActionResult SortByIng()
        // {
        //     ViewBag.Title = "Sorted";
        //     return View();
        // }
    }
}
