using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookLinks : IBookLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<BookDto> _dataShaper; //IoC kaydı extensions'da yapılmıştı

        public BookLinks(LinkGenerator linkGenerator, IDataShaper<BookDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<BookDto> booksDto, string fields, HttpContext httpContext)
        {
            var shapedBooks = ShapeData(booksDto, fields);

            if (ShouldGenereateLinks(httpContext))
                return ReturnLinkedBooks(booksDto, fields, httpContext, shapedBooks);

            return ReturnShapedBooks(shapedBooks);
        }

        private LinkResponse ReturnLinkedBooks(IEnumerable<BookDto> booksDto, string fields, HttpContext httpContext, List<Entity> shapedBooks)
        {
            //booksDto üzerinde dolaşıp herbir kaynak için link üretmeye çalışacağız:
            var bookDtoList = booksDto.ToList();
            for (int index=0; index < bookDtoList.Count(); index++)
            {
                var bookLinks = GenerateForBook(httpContext, bookDtoList[index],fields);
                shapedBooks[index].Add("Links", bookLinks);
            }

            var bookCollection = new LinkCollectionWrapper<Entity>(shapedBooks);
            return new LinkResponse() { HasLinks = true, LinkedEntities = bookCollection };
        }

        private List<Link> GenerateForBook(HttpContext httpContext, BookDto bookDto, string fields)
        {
            var links = new List<Link>()
            {
                new Link("a1","b1", "c1"),
                new Link("a2","b2", "c2")
            };
            return links;
        }

        private LinkResponse ReturnShapedBooks(List<Entity> shapedBooks)
        {
            return new LinkResponse() { HasLinks= false,
                                        ShapedEntities= shapedBooks
            };
        }

        private bool ShouldGenereateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private List<Entity> ShapeData(IEnumerable<BookDto> booksDto, string fields)
        {
            return _dataShaper.ShapeData(booksDto, fields).Select(b=> b.Entity).ToList();
        }
    }
}
