using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager 
    {
        //unit of work pattern'i uygulanır. Yani bütün repo'lara manager üzerinden erişim verilir

        IBookRepository BookRepo { get; }

        void Save();
        
    }
}
