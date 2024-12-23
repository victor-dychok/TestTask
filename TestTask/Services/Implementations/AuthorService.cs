using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;
        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Author> GetAuthor()
        {
            //находим максимальную длинну названия
            var maxTitleLength = _context.Books.Max(b => b.Title.Length);
            //вытаскиваем все книги с этой длинной названия (чтобы предусмотреть случаи, когда таких книг несколько)
            var bookList = _context.Books.Where(b => b.Title.Length ==  maxTitleLength).ToList();

            //ищем минимальный Id автора в этом списке книг
            var authId = bookList.MinBy(b => b.AuthorId).AuthorId;
            return await _context.Authors.SingleOrDefaultAsync(a => a.Id == authId);
        }

        public async Task<List<Author>> GetAuthors()
        {
            var date = new DateTime(2014, 12, 31, 23, 59, 59);

            //вытаскиваем авторов и их книги, вышедшие после 2015
            var list = _context.Authors.Include(a => a.Books.Where(b => b.PublishDate > date));
            
            var resultList = new List<Author>();
            
            //составляем список авторов с четным количеством книг, вышедших после 2015
            foreach (var auth in list)
            {
                if(auth.Books.Count != 0 && auth.Books.Count % 2 == 0)
                {
                    resultList.Add(auth);
                }
            }

            return resultList;
        }
    }
}
