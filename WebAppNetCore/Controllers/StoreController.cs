using Microsoft.AspNetCore.Mvc;
using WebAppNetCore.Models;
using WebAppNetCore.Models.Pages;

namespace WebAppNetCore.Controllers
{
    public class StoreController : Controller
    {
        private IRepository productRepository;
        private ICategoryRepository categoryRepository;

        public StoreController(IRepository prodRepo, ICategoryRepository catRepo)
        {
            productRepository = prodRepo;
            categoryRepository = catRepo;
        }

        //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1
        public IActionResult Index([FromQuery(Name = "options")]
            QueryOptions productOptions,
            QueryOptions categoryOptions,
            long category)
        {
            ViewBag.Categories = categoryRepository.GetCategories(categoryOptions);
            ViewBag.SelectedCategory = category;
            return View(productRepository.GetProducts(productOptions, category));
        }
    }
}