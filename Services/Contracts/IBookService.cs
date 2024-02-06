using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks(bool trackChanges);
        Book GetOneBook(int id, bool trackChanges);
        Book CreateOneBook(Book book);
        void UpDdateOneBook(int id, Book book, bool trackChanges);
        void DeleteOneBook(int id, bool trackChanges);


    }
}
