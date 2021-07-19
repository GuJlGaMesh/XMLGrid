using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Xml.Linq;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Model;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BooksController : Controller
    {
        private readonly XmlContext _context;

        private readonly IXmlService _xmlService;

        private IWebHostEnvironment _environment;

        public BooksController(XmlContext context, IXmlService xmlService, IWebHostEnvironment host)
        {
            _environment = host;
            _context = context;
            //ClearContext();
            _xmlService = xmlService;
        }
        
        // Нужно для очистки контекста базы данных
        // к своему стыду, не нашёл нормального способа очистки таблицы перед стартом приложения.
        // Если оставить вызов этого метода при каждом запуске приложения, то почему-то данные перестают сохраняться
        // в контексте, а куда-то исчезают после того как метод LoadXml отарбатывает.
        // Поэтому запуск, в котором я хочу чистую таблицу, я расскоментирую вызов метода, запускаю, потом снова коммнтирую
        private void ClearContext()
        {
            _context.Books.RemoveRange(_context.Books.ToList());
            _context.SaveChanges();
        }
        
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Books.ToList());
        }

        
        [HttpGet]
        public IActionResult DownLoadXML()
        {
            var contentPath = Path.Combine(_environment.WebRootPath, "Downloads");
            
            _xmlService.WriteToXMLFile(_context.Books.ToList());
            var filePath = Path.Combine(contentPath, "serBooks.xml");
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            return PhysicalFile(filePath, "application/xml", "file.xml");
        }
        
        [HttpGet]
        public IActionResult DownLoadHTML()
        {
            var contentPath = Path.Combine(_environment.WebRootPath, "Downloads");
            
            _xmlService.WriteToHTML(_context.Books.ToList());
            var filePath = Path.Combine(contentPath, "report.html");
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            return PhysicalFile(filePath, "application/html", "report.html");
        }
        
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Category,Author,Price,Year,Language,Cover")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/LoadXml
        public IActionResult LoadXml()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoadXml([FromForm] XmlTransfer xmlData)
        {
            var xml = XDocument.Parse(xmlData.XML);
            var books = _xmlService.Read(xml);
                //_context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT BOOKS ON;");
            _context.AddRange(books);
             _context.SaveChanges();
           // var script = "window.location ='" + Url.Action("Index", "Books") + "' ;";
            //return JavaScript(script);
            return RedirectToAction("Index");
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Category,Author,Price,Year,Language,Cover")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }


    }
}
