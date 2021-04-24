using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeApp.AppContext;
using BikeApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeApp.Controllers
{
    [Route("Make")]
    [Route("Make/Bikes")]
    [Authorize(Roles="Admin,Executive")]
    public class MakeController : Controller
    {
        private readonly VroomDbContext _context;

        public MakeController(VroomDbContext _context)
        {
            this._context = _context;
        }
        public IActionResult Index()
        {

            Make make = new Make { Id = 1, Name = "Harley divison" };
            return View(_context.Make.ToList()) ;
        }

        [Route("Make/Bikes/{year:int:length(4)}/{month:int:range(1,13)}")]
        public IActionResult ByYearMonth(int year,int month)
        {

            Make make = new Make { Id = 1, Name = "Harley divison" };
            return View(year+","+month);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Make make)
        {
            if (ModelState.IsValid)
            {
                _context.Add(make);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(make);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var make = _context.Make.Find(id);
            if (make == null)
            {
                return NotFound();
            }
            _context.Make.Remove(make);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Make make)
        {

            if (ModelState.IsValid)
            {
                _context.Update(make);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(make);

        }
    }
}
