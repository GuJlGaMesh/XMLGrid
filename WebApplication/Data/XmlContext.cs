using BusinessLogic.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class XmlContext : DbContext
    {
        public XmlContext(DbContextOptions<XmlContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
    }
}
