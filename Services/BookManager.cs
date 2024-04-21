using Entities.Models;
using Services.Contracts;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Exceptions;
using AutoMapper;
using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using System.Dynamic;
using Entities.LinkModels;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly ICategoryService _categoryService;
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        //private readonly IDataShaper<BookDto> _shaper;
        private readonly IBookLinks _bookLinks;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper, IBookLinks bookLinks, ICategoryService categoryService)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _bookLinks = bookLinks;
            _categoryService = categoryService;
        }

        public async Task<BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            /*var category = await _manager.CategoryRepo.GetOneCategoryByIdAsync(bookDto.CategoryId, false);

            if (category is null)
                throw new CategoryNotFoundException(bookDto.CategoryId);
            //_categoryService injection'dan sonra           
             */
            var category = await _categoryService.GetOneCategoryByIdAsync(bookDto.CategoryId, false);
            var entity = _mapper.Map<Book>(bookDto);
            //entity.CategoryId = bookDto.CategoryId; maplemede yapılmış.

            _manager.BookRepo.CreateOneBook(entity); //kitap oluşturuldu ama kaydedilmedi
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            /*//check entity: kitap varsa silinecek
            var entity = await _manager.BookRepo.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null)
            {
                throw new BookNotFoundException(id);
                //string message = $"Book with id:{id} could not be found";
                //_logger.LogInfo(message);
                //throw new Exception(message);
            }*/
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges); //await!

            _manager.BookRepo.DeleteOneBook(entity);
            await _manager.SaveAsync();
        }

        public async Task<(LinkResponse linkResponse, MetaData metaData)> GetAllBooksAsync(LinkParameters linkParameters, bool trackChanges)
        {
            if (!linkParameters.BookParameters.ValidPriceRange)
                throw new PriceOutofRangeBadRequestException();

            var booksWithMetaData = await _manager.BookRepo.GetAllBooksAsync(linkParameters.BookParameters, trackChanges);
            var booksDto = _mapper.Map<IEnumerable<BookDto>>(booksWithMetaData);

            //Data shaping: artık link üzerinde gerçekleşecek.
            //var shapedData = _shaper.ShapeData(booksDto, bookParameters.Fields);

            var links = _bookLinks.TryGenerateLinks(booksDto, linkParameters.BookParameters.Fields, linkParameters.HttpContext);
            return (linkResponse: links, metadata: booksWithMetaData.MetaData);
        }

        public async  Task<BookDto> GetOneBookByIdAsync(int id, bool trackChanges)
        {
            /*var book= await _manager.BookRepo.GetOneBookByIdAsync(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);*/
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
            return _mapper.Map<BookDto>(book);
        }

        public async Task<(BookDtoForUpdate bookDtoForUpdate, Book book)> GetOneBookForPatchAsync(int id, bool trackChanges)
        {
            var book = await GetOneBookByIdAndCheckExists(id, trackChanges);
            var bookDtoForUpdate = _mapper.Map<BookDtoForUpdate>(book);
            return (bookDtoForUpdate, book);
        }

        public async Task saveChangesForPatchAsync(BookDtoForUpdate bookDtoForUpdate, Book book)
        {
            //save işlemi Entities üzerinde yapılıyor
            _mapper.Map(bookDtoForUpdate, book);
            await _manager.SaveAsync();

        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            //check entity: kitap varsa update edilecek
            var entity = await GetOneBookByIdAndCheckExists(id, trackChanges);

            /*if (book is null)
                throw new ArgumentNullException(nameof(book));*/

            //mapping
            /*entity.Title = book.Title;
            entity.Price = book.Price;*/
            var category = await _categoryService.GetOneCategoryByIdAsync(bookDto.CategoryId, false);
            entity = _mapper.Map<Book>(bookDto);

            _manager.BookRepo.Update(entity); // EFCore izlediği için update metodu çağrılmasa da update eder, direkt save edilebilir
            await _manager.SaveAsync();

        }

        //Refactoring: tekrardan kurtulmak için
        public async Task<Book> GetOneBookByIdAndCheckExists(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager.BookRepo.GetOneBookByIdAsync(id, trackChanges);
            if (entity is null)
                throw new BookNotFoundException(id);
            return entity;
        }

        public async Task<List<Book>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _manager.BookRepo.GetAllBooksAsync(trackChanges);
            return books;
        }

        public async Task<IEnumerable<Book>> GetAllBooksWithDetailsAsync(bool trackchanges)
        {
            return await _manager.BookRepo.GetAllBooksWithDetailsAsync(trackchanges);
            //throw new NotImplementedException();
        }
    }
}
