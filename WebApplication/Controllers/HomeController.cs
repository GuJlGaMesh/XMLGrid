using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> logger)
        {
            
        }

        public async Task<IActionResult> Index()
        {
            var books = new List<Book>()
            {
                new Book() {Title = "Awesome book", Author = "My mind", Category = "Bestsellers", Price = 300, Year = "2000"},
                new Book() {Title = "Awesome book", Author = "My mind", Category = "Bestsellers", Price = 300, Year = "2000"},
                new Book() {Title = "Awesome book", Author = "My mind", Category = "Bestsellers", Price = 300, Year = "2000"},
                new Book() {Title = "Awesome book", Author = "My mind", Category = "Bestsellers", Price = 300, Year = "2000"},
                new Book() {Title = "Awesome book", Author = "My mind", Category = "Bestsellers", Price = 300, Year = "2000"},
                new Book() {Title = "Awesome book", Author = "My mind", Category = "Bestsellers", Price = 300, Year = "2000"},
                new Book() {Title = "Awesome book", Author = "My mind", Category = "Bestsellers", Price = 300, Year = "2000"},
                new Book() {Title = "Awesome book", Author = "My mind", Category = "Bestsellers", Price = 300, Year = "2000"}
            };
            return View(books);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}