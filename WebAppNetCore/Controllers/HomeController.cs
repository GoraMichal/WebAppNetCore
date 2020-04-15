using Microsoft.AspNetCore.Mvc;
using WebAppNetCore.Models;
using WebAppNetCore.Models.Pages;

namespace WebAppNetCore.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;
        private ICategoryRepository categoryRepository;

        public HomeController(IRepository tempRepo, ICategoryRepository tempCatRepo) 
        {
            repository = tempRepo;
            categoryRepository = tempCatRepo;
        }

        //public IActionResult Index() => View(repository.Products);

        public IActionResult Index(QueryOptions options)
        {
            return View(repository.GetProducts(options));
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            repository.AddProduct(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateProduct(long key)
        {
            ViewBag.Categories = categoryRepository.Categories;
            return View(key == 0 ? new Product() : repository.GetProduct(key));
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (product.Id == 0)
            {
                repository.AddProduct(product);
            }
            else
            {
                repository.UpdateProduct(product);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAll()
        {
            ViewBag.Categories = categoryRepository.Categories;
            ViewBag.UpdateAll = true;
            return View(nameof(Index), repository.Products);
        }

        [HttpPost]
        public IActionResult UpdateAll(Product[] products)
        {
            repository.UpdateAll(products);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            repository.Delete(product);
            return RedirectToAction(nameof(Index));
        }
    }
}