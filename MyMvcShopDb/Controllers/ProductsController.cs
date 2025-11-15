using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMvcShopDb.Infrastructure.Data;
using MyMvcShopDb.Core.Models;
using MyMvcShopDb.ViewModels;
using MyMvcShopDb.Services;

namespace MyMvcShopDb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPhotoService _photoService;

        public ProductsController(ApplicationDbContext context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var productsList = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Select(p => new ProductIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    CategoryName = (p.Category == null) ? null : p.Category.Name,
                    ManufacturerName = (p.Manufacturer == null) ? null : p.Manufacturer.Name
                });

            return View(await productsList.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductViewModel
            {
                Name = string.Empty,
                CategoryList = await _context.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToListAsync(),
                ManufacturerList = await _context.Manufacturers.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }).ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string imageUrl = null;
                if (viewModel.ImageFile != null)
                {
                    imageUrl = await _photoService.AddPhotoAsync(viewModel.ImageFile);
                }

                var product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    ImageUrl = imageUrl,
                    CategoryId = viewModel.CategoryId,
                    ManufacturerId = viewModel.ManufacturerId
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.CategoryList = await _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
            viewModel.ManufacturerList = await _context.Manufacturers.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToListAsync();

            return View(viewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl, // <-- Передаємо існуючий URL у View
                CategoryId = product.CategoryId,
                ManufacturerId = product.ManufacturerId,
                CategoryList = await _context.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }).ToListAsync(),
                ManufacturerList = await _context.Manufacturers.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }).ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // <-- 7. ПОКРАЩЕНА ЛОГІКА ОНОВЛЕННЯ
                    // Знаходимо існуючий продукт, щоб не втратити дані
                    var product = await _context.Products.FindAsync(viewModel.Id);
                    if (product == null)
                    {
                        return NotFound();
                    }

                    // Оновлюємо властивості
                    product.Name = viewModel.Name;
                    product.Price = viewModel.Price;
                    product.CategoryId = viewModel.CategoryId;
                    product.ManufacturerId = viewModel.ManufacturerId;

                    // Перевіряємо, чи користувач завантажив НОВИЙ файл
                    if (viewModel.ImageFile != null)
                    {
                        // Якщо так, завантажуємо новий
                        string newImageUrl = await _photoService.AddPhotoAsync(viewModel.ImageFile);
                        product.ImageUrl = newImageUrl; // і оновлюємо URL
                    }
                    // Якщо viewModel.ImageFile == null,
                    // то product.ImageUrl просто залишається таким, яким був у базі.

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // ... (Без змін) ...
            viewModel.CategoryList = await _context.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();
            viewModel.ManufacturerList = await _context.Manufacturers.Select(m => new SelectListItem
            {
                Text = m.Name,
                Value = m.Id.ToString()
            }).ToListAsync();

            return View(viewModel);
        }

        // GET: Products/Delete/5
        // ... (Без змін) ...
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        // ... (Без змін) ...
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                // TODO: По-хорошому, тут треба додати логіку видалення
                // старого фото з Cloudinary, але це вимагає 
                // окремого методу в IPhotoService
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}