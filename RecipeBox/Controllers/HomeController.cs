﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace RecipeBox.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
