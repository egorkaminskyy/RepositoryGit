using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LogA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LogA.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext context;
        public HomeController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> List()
        {
            
            var s= from q in context.Profiles.Include("User")
                select q;

            return View(await s.ToListAsync());
        }

        public IActionResult SearchUser()
        {
            

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
