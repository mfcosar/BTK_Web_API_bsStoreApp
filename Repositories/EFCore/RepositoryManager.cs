using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        //save işlemi context üzerinde yapılacağı için bir injection gerekiyor
        private readonly RepositoryContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;

        public RepositoryManager(RepositoryContext context, IBookRepository bookRepository, 
               ICategoryRepository categoryRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
        }
        //Buraya kadar DI çerçevesi denir


        /*private readonly Lazy<IBookRepository> _bookRepository;  //Lazy ifadesine gerek yok çünkü IoC kaydı yapıldı
private readonly Lazy<ICategoryRepository> _categoryRepository;*/


        /*public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_context));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
        }*/

        public IBookRepository BookRepo => _bookRepository;
        public ICategoryRepository CategoryRepo => _categoryRepository;

        /* public IBookRepository BookRepo => _bookRepository.Value;    //new BookRepository(_context); 
        public ICategoryRepository CategoryRepo => _categoryRepository.Value; */

        //normalde bir class içinde başka bir class new'lenmez; sıkı bağlı bir uygulama olur. Ama;
        //IBookRepository için ayrı bir IoC kaydı yapmak istemiyoruz. O sebeple burda BookRepo new'lenecek

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
