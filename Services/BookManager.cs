using Entities.Models;
using Services.Contracts;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public BookManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateOneBook(Book book)
        {
            _manager.BookRepo.CreateOneBook(book); //kitap oluşturuldu ama kaydedilmedi
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            //check entity: kitap varsa silinecek
            var entity = _manager.BookRepo.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFoundException(id);
                /*string message = $"Book with id:{id} could not be found";
                _logger.LogInfo(message);
                throw new Exception(message);*/
            }

            _manager.BookRepo.DeleteOneBook(entity);
            _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            return _manager.BookRepo.GetAllBooks(trackChanges);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book= _manager.BookRepo.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            return book;
        }

        public void UpDdateOneBook(int id, Book book, bool trackChanges)
        {
            //check entity: kitap varsa update edilecek
            var entity = _manager.BookRepo.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFoundException(id);
                /*string message = $"Book with id:{id} could not be found";
                _logger.LogInfo(message);
                throw new Exception(message);*/
            }

            /*if (book is null)
                throw new ArgumentNullException(nameof(book));*/

            entity.Title = book.Title;
            entity.Price = book.Price;
            _manager.BookRepo.Update(entity); // EFCore izlediği için update metodu çağrılmasa da update eder, direkt save edilebilir
            _manager.Save();

        }
    }
}
