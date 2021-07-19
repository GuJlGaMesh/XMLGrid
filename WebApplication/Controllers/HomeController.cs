using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BusinessLogic.Interfaces;
using BusinessLogic.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;


namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        static XDocument _xml = XDocument.Load(@"D:\Files\first.xml");
        IWebHostEnvironment _environment;
        IXmlService _xmlService;
        private readonly XmlContext _context;
        public HomeController(IWebHostEnvironment host, IXmlService xmlService, XmlContext context)
        {
            _context = context;
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Books]");
            _environment = host;
            _xmlService = xmlService;

        }

        public async Task<ActionResult> Index(List<Book> books = null)
        {
            return View(await _context.Books.ToListAsync());
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Book());
            else
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }
                return View(book);
            }
        }


        // GET: Transaction/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }




        //[ HttpPost]
        //public async Task<ActionResult> LoadFile([FromForm] IFormFile file)
        //{
        //    if (file is not null)
        //    {
        //        var path = Path.Combine(_environment.WebRootPath, "Uploads");

        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }

        //        using (var stream = System.IO.File.Create(path))
        //        {
        //            await file.CopyToAsync(stream);
        //        }
        //        return RedirectToAction("Index", path);
        //    }

        //    return RedirectToAction("Index");
        //}

        //[ HttpPost]
        //public async Task<ActionResult> AddXmlItem([FromForm]Book b)
        //{
        //    b.Id = _books.LastOrDefault().Id + 1;
        //    _books.Add(b);
        //    return View("Index", _books);
        //}

        ////[ HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var result = _books.Remove(_books.FirstOrDefault(x => x.Id == id));

        //    return View(nameof(Index), _books);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}