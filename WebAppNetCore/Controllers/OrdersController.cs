using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppNetCore.Models;
using Microsoft.AspNetCore;


namespace WebAppNetCore.Controllers
{
    public class OrdersController : Controller
    {
        private IRepository productRepository;
        private IOrdersRepository ordersRepository;

        public OrdersController(IRepository tempProductRepository, IOrdersRepository tempOrdersRepository)
        {
            productRepository = tempProductRepository;
            ordersRepository = tempOrdersRepository;
        }

        public IActionResult Index()
        {
            return View(ordersRepository.Orders);
        }

        public IActionResult EditOrder(long id)
        {
            var products = productRepository.Products;
            
            Order order = id == 0 ? new Order() : ordersRepository.GetOrder(id);

            IDictionary<long, OrderLine> linesMap = order.Lines?.ToDictionary(l => l.ProductId)
                ?? new Dictionary<long, OrderLine>();

            //Dla istniejacych zamowien wypelnia obiekty OrderLines zczytane z BD
            ViewBag.Lines = products.Select(p => linesMap.ContainsKey(p.Id) 
                ? linesMap[p.Id]
                : new OrderLine { Product = p, ProductId = p.Id, Quantity = 0 });

            return View(order);
        }

        [HttpPost]
        public IActionResult AddOrUpdateOrder(Order order)
        {
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteOrder(Order order)
        {
            ordersRepository.DeleteOrder(order);
            return RedirectToAction(nameof(Index));
        }
    }
}