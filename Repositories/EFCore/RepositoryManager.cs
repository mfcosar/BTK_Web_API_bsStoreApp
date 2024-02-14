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

        private readonly Lazy<IBookRepository> _bookRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _bookRepository = new Lazy<IBookRepository>(() => new BookRepository(_context));
        }
        //Buraya kadar DI çerçevesi denir
        public IBookRepository BookRepo => _bookRepository.Value;    //new BookRepository(_context); 
        //normalde bir class içinde başka bir class new'lenmez; sıkı bağlı bir uygulama olur. Ama;
        //IBookRepository için ayrı bir IoC kaydı yapmak istemiyoruz. O sebeple burda BookRepo new'lenecek

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
