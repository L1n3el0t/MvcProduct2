using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie1.Models;
using MvcMovie1.Services;

namespace MvcMovie1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _products;
        private readonly ILogger<ProductsController> _logger;
       
        public ProductsController(IProductService products, ILogger<ProductsController> logger)
        {
            _products = products;
            _logger = logger;

        }



        // GET: Products
        public async Task<IActionResult> Index(string productGenre, string searchString)
        {
            
            IEnumerable<Product> all = await _products.GetAllAsync();
            IEnumerable<Product> products = all;
            IEnumerable<string?> genreQuery = all.Select(product => product.Genre).Distinct();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(product => product.Title != null && product.Title.ToUpper().Contains(searchString, StringComparison.OrdinalIgnoreCase));

                _logger.LogInformation("Searching by string {searchString} ", searchString);
            }

            if (!String.IsNullOrEmpty(productGenre))
            {
                products = products.Where(product => product.Genre == productGenre);
                _logger.LogInformation("Searching by genre {productGenre} ", productGenre);
            }

            var productGenreVM = new ProductGenreViewModel
            {
                Genres = new SelectList(genreQuery),
                Products = products.ToList()
            };

            return View(productGenreVM);
           
        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
          

            var product = await _products.GetByIdAsync(id);

            _logger.LogInformation("Displaying details for product {id} ", id);
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Create GET");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Product product)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create POST model invalid");
                return View(product);
            }

                await _products.AddAsync(product);
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var product = await _products.GetByIdAsync(id);
            _logger.LogInformation("Edit GET for product {id} ", id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Product product)
        {
            if (!ModelState.IsValid) {
                _logger.LogWarning("Edit POST model invalid for product {id} ", id);
                return View(product);
            }
            await _products.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
           
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _products.GetByIdAsync(id);
            _logger.LogInformation("Delete GET for product {id} ", id);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _products.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
