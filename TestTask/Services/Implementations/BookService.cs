using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly DateTime _carolusRexReleaseDate = new DateTime(2012, 05, 25);
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Book> GetBook()
        {
            var booksList = _context.Books.ToList();
            return booksList.MaxBy(b => b.QuantityPublished * b.Price);
        }

        public async Task<List<Book>> GetBooks()
        {
            var set = _context.Set<Book>();
            return set.Where(b => b.PublishDate > _carolusRexReleaseDate && b.Title.Contains("Red")).ToList();
        }
    }
}
