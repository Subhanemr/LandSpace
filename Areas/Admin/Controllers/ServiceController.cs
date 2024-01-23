using LandSpace.Areas.Admin.ViewModels;
using LandSpace.DAL;
using LandSpace.Models;
using LandSpace.Utilities.Extantions;
using LandSpace.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LandSpace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class ServiceController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public ServiceController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0) return BadRequest();
            double count = await _context.Services.CountAsync();

            ICollection<Service> items = await _context.Services.Skip((page-1)*4).Take(4).ToListAsync();
            PaginationVM<Service> pagination = new PaginationVM<Service>
            {
                Items = items,
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / 4)
            };
            return View(pagination);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateServiceVM create)
        {
            if (!ModelState.IsValid) return View(create);
            if (await _context.Services.AnyAsync(x => x.Name.Trim().ToLower() == create.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", "Is exists");
                return View(create);
            }
            if (!create.Photo.IsValid())
            {
                ModelState.AddModelError("Photo", "Is not valid");
                return View(create);
            }
            if (!create.Photo.LimitSize())
            {
                ModelState.AddModelError("Photo", "Limit size 10MB");
                return View(create);
            }
            Service item = new Service
            {
                Name = create.Name,
                Description = create.Description,
                Img = await create.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img")
            };
            await _context.Services.AddAsync(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Service item = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            UpdateServiceVM update = new UpdateServiceVM
            {
                Name = item.Name,
                Description = item.Description,
                Img = item.Img
            };
            return View(update);
        }
        public async Task<IActionResult> Update(int id, UpdateServiceVM update)
        {
            if (!ModelState.IsValid) return View(update);
            Service item = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            if (await _context.Services.AnyAsync(x => x.Name.Trim().ToLower() == update.Name.Trim().ToLower() && x.Id != id))
            {
                ModelState.AddModelError("Name", "Is exists");
                return View(update);
            }
            if(update.Photo!= null)
            {
                if (!update.Photo.IsValid())
                {
                    ModelState.AddModelError("Photo", "Is not valid");
                    return View(update);
                }
                if (!update.Photo.LimitSize())
                {
                    ModelState.AddModelError("Photo", "Limit size 10MB");
                    return View(update);
                }
                item.Img.DeleteFile(_env.WebRootPath, "assets", "img");
                item.Img = await update.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            }
            item.Name= update.Name;
            item.Description = update.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Service item = await _context.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            item.Img.DeleteFile(_env.WebRootPath, "assets", "img");
            _context.Services.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
