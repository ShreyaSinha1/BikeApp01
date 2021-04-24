using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeApp.AppContext;
using BikeApp.Controllers.Resource;
using BikeApp.Models;
using BikeApp.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace BikeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : Controller
    {
        private readonly VroomDbContext _context;
        private readonly ILogger<ModelController> _logger;

        private readonly IMapper _mapper;

        [BindProperty]
        public ModelViewModel ModelVm { get; set; }
        public ModelController(VroomDbContext _context, ModelViewModel ModelVm, ILogger<ModelController> logger, IMapper _mapper)
        {
            this._context = _context;
            this.ModelVm = new ModelViewModel()
            {
                Makes=_context.Make.ToList(),
                Model=new Models.Model()
            };
            this._logger = logger;
            this._mapper = _mapper;
        }
        public IActionResult Index()
        {
            return View(ModelVm);
        }

        public IActionResult Create()
        {
            return View(ModelVm);
        }
        [HttpPost,ActionName("Create")]
        public IActionResult CreatePost()
        {
            if (!ModelState.IsValid)
            {
                return View(ModelVm);
            }
            _context.Models.Add(ModelVm.Model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost,ActionName("Edit")]
        public IActionResult Edit(int id)
        {
            ModelVm.Model = _context.Models.Include(m=>m.Make).SingleOrDefault(m=>m.Id==id);
            if (ModelVm.Model == null)
            {
                return NotFound();
            }
            return View(ModelVm);
        }
        [HttpPost]
        public IActionResult EditPost()
        {
            //ModelVm.Model = _context.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
            if (!ModelState.IsValid)
            {
                return View(ModelVm);
            }
            _context.Update(ModelVm.Model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            //ModelVm.Model = _context.Models.Include(m => m.Make).SingleOrDefault(m => m.Id == id);
            Model model = _context.Models.Find(id);
            if (model==null)
            {
                return NotFound();
            }
            _context.Models.Remove(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("api/model")]
        public IEnumerable<Model> Models()
        {
            return _context.Model.ToList();
        }

        [AllowAnonymous]
        [HttpGet("api/models")]///{MakeId}")]
        public IEnumerable<ModelResources> Models(int id)
        {
            // return _context.Make.ToList().where(mbox => mbox.MakeId == id);
            var models = _context.Models.ToList();

            var config = new MapperConfiguration(x=>x.Createmap<Model,ModelResources>());
            var mapper = new Mapper(config);

            var modelResource = mapper.Map<List<Model>,List<ModelResources>>(models);
            var modelResource = models.Select(models =>
              new ModelResources
              {
                  Id = models.Id,
                  Name = models.Name

              }).ToList();

            return modelResource;
        }
    }
}
