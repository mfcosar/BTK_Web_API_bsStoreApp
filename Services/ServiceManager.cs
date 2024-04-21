using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        private readonly IAuthenticationService _authenticationService;

        public ServiceManager(IBookService bookService, ICategoryService categoryService, IAuthenticationService authenticationService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _authenticationService = authenticationService;
        }

        /*private readonly Lazy<IBookService> _bookService;
        private readonly Lazy<ICategoryService> _categoryService;
        private readonly Lazy<IAuthenticationService> _authenticationService;*/
        /*public ServiceManager(IRepositoryManager repositoryManager, ILoggerService logger, IMapper mapper, IConfiguration config,
            UserManager<User> userManager, IBookLinks bookLinks)
        {
            _categoryService = new Lazy<ICategoryService>(() =>
            new CategoryManager(repositoryManager));

            _bookService = new Lazy<IBookService>(() => 
            new BookManager(repositoryManager, logger, mapper, bookLinks, _categoryService.Value));



            _authenticationService = new Lazy<IAuthenticationService>(()=> 
            new AuthenticationManager(logger, mapper, userManager, config));
        }*/

        /*public IBookService BookService => _bookService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public ICategoryService CategoryService => _categoryService.Value;*/
        public IBookService BookService => _bookService;
        public IAuthenticationService AuthenticationService => _authenticationService;
        public ICategoryService CategoryService => _categoryService;

    }
}
