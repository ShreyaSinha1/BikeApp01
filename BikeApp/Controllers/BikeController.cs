using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeApp.AppContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BikeApp.Controllers
{
    [Authorize(Roles="Admin,Executive")]
    public class BikeController : Controller
    {
        private readonly VroomDbContext _context;
        private readonly ILogger<BikeController> _logger;
        public readonly HostingEnvironment hostingEnvironment;
        [BindProperty]
        public BikeViewModel BikeVM;
        public BikeController(VroomDbContext _context, ILogger<BikeController> _logger, HostingEnvironment hostingEnvironment)
        {
            this._context = _context;
            this._logger = _logger;
            BikeVM = new BikeViewModel()
            {
                Makes = _context.Make.ToList(),
                Models = _context.Models.ToList(),
                BikeApp=new Models.Bike()
            };
            hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(string searchResult,string sortOrder,int pageno,int pageSize=1)
        {
            int excludeRecords = (pageno * pageSize)- pageSize;
            ViewBag.PriceSortParam = string.IsNullorempty(sortOrder)?"price_desc":"";
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentFilter = searchResult;

           var bikes = _context.Bikes.Include(x=>x.Make).Include(x=>x.Model)
                .Skip(excludeRecords).Take(pageSize);
            if (!string.IsNullorempty(searchResult))
            {
                bikes = bikes.Where(bikes => bikes.Make.Name.Contains(searchResult))
                    && bikes.Model.Name.Contains();
                bikes = bikes.Count();

            }
            var result = new PagedResult<Bike>()
            {
                Data=bikes.AsNoTracking().ToList(),
                TotalItems=_context.Bikes.Count(),
                PageNumber= pageno,
                pageSize= pageSize
            };
            switch(sortOrder)
            {
                case "price_desc":
                    bikes = bikes.OrderByDescending(bikes=>bikes.Price);
                    break;
               default:
                    bikes = bikes.OrderBy(bikes => bikes.Price);
                    break;

            }
            return View(bikes.ToList());
        }

        public IActionResult Index2()
        {
            var bikes = _context.Bikes.Include(x => x.Make).Include(x => x.Model);
            return View(bikes.ToList());
        }

        public IActionResult Edit(int id)
        {
            BikeVM.Bike = _context.Bikes.SingleOrDefault(x =>x.Id==id);
            BikeVM.Models = _context.Bikes.Where(x => x.MakeId == Bikes.Bike.MakeId);

            if (BikeVM.Bike == null)
            {
                return NotFound();
            }
            return View(BikeVM);
        }
        public IActionResult Create()
        {
           // var bikes = _context.Bikes.Include(x => x.Make).Include(x => x.Model);
            return View(BikeVM);
        }


        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            // var bikes = _context.Bikes.Include(x => x.Make).Include(x => x.Model);
            if (!ModelState.IsValid)
            {
                BikeVM.Makes = _context.Make.ToList();
                BikeVM.Models = _context.Models.ToList();

                return View(BikeVM);
            }
            UploadImageUpload();
            _context.Models.Add(BikeVM.Model);
            _context.SaveChanges();

            var bikeId = BikeVM.Bike.Id;
            string wwroot = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files();
            var savedBikes = _context.Bikes.Find(bikeId);
            if (files.Count > 0)
            {

                var imagePath = @"images\bikes";
                var extension = Path.GetExtension(files[0].FileName);
                var relativeImagePath = imagePath+ bikeId + extension;
                var absImg = imagePath.Combine(wwroot, relativeImagePath);
            }
            using (var fileStream=new FilStream(absImg,FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }
            savedBikes.ImagePath = relativeImagePath;
            _context.SaveChanges();
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            //ModelVm.Model = _context.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
            Bike bike = _context.Bikes.Find(id);
            if (bike == null)
            {
                return NotFound();
            }
            _context.Bikes.Remove(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private void UploadImageUpload()
        {
            var bikeId = BikeVM.Bike.Id;

            string wwroot = _hostingEnvironment.WebRoot;

            var files = HttpContext.Request.Form.Files;
            var savedBikes = _context.Bikes.Find(bikeId);
        }
    }
}
