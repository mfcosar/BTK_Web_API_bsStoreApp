﻿using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
   
        public class BooksController : ControllerBase
        {
            //Servis katmanı ekleniyor
            private readonly IServiceManager _manager;
            public BooksController(IServiceManager manager)
            {
                _manager = manager;
            }


            /* private readonly IRepositoryManager _manager;  //RepositoryContext _context; ==> artık manager kullanılsın istiyoruz.

            public BooksController(IRepositoryManager manager)
            {
                _manager = manager; //_manager üzerinden repolara erişim sağlanır
            }*/


            /* public BooksController(RepositoryContext context)
            {//register yapılmış olan context burda resolve edilir

                _context = context;
            }*/


            [HttpGet]
            public IActionResult GetAllBooks()
            {
                try
                {
                    //var books = _manager.BookRepo.GetAllBooks(false);   //_context.Books.ToList();
                    var books = _manager.BookService.GetAllBooks(false);
                    return Ok(books);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            [HttpGet("{id:int}")]
            public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
            {
                try
                {
                    var book = _manager.BookService.GetOneBookById(id, false);        //BookRepo.GetOneBookById(id,false);
                                                                                      //_context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

                    if (book is null)
                        return NotFound();
                    return Ok(book);

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            [HttpPost]
            public IActionResult FormOneBook([FromBody] Book book)
            {
                try
                {
                    if (book is null)
                        return BadRequest(); //400

                    //_manager.BookRepo.CreateOneBook(book);  //_context.Books.Add(book);
                    _manager.BookService.CreateOneBook(book);
                    //_manager.Save();    //_context.SaveChanges();
                    return StatusCode(201, book);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            [HttpPut("{id:int}")]
            public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
            {
                try
                {
                    if (book is null)
                        return BadRequest(); //400

                    _manager.BookService.UpDdateOneBook(id, book, true);
                    return NoContent(); //204
                                        //var entity = _manager.BookRepo.GetOneBookById(id, true);
                                        //_context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();


                    //check id: update edilecek book ile route'dan gelen id eşit değilse BadRequest
                    /*if (id != book.Id)
                        return BadRequest(); // 400

                    //map işlemine ihtiyaç var:
                    entity.Title = book.Title;
                    entity.Price = book.Price;
                    _manager.Save();  //_context.SaveChanges();
                    return Ok(book);*/
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }


            [HttpDelete("{id:int}")]
            public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
            {
                try
                {
                    /*//var entity = _manager.BookRepo.GetOneBookById(id, false);
                    var entity = _manager.BookService.GetOneBookById(id, false);
                        //_context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();

                    if (entity is null)
                        return NotFound(new
                        {
                            statusCode = 404,
                            message = $"Book with id:{id} could not be found."
                        }); // 404
                    */
                    /*_manager.BookRepo.DeleteOneBook(entity);    //_context.Books.Remove(entity);
                    _manager.Save();                            //_context.SaveChanges();*/
                    _manager.BookService.DeleteOneBook(id, false);

                    return NoContent();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            [HttpPatch("{id:int}")]
            public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id,
        [FromBody] JsonPatchDocument<Book> bookPatch)
            {

                try
                {
                    //check entity, if book exists
                    //var entity = _manager.BookRepo.GetOneBookById(id, true);
                    //_context.Books.Where(b => b.Id.Equals(id)).SingleOrDefault();
                    var entity = _manager.BookService.GetOneBookById(id, true);
                    if (entity is null)
                        return NotFound();//404

                    bookPatch.ApplyTo(entity);
                    //_manager.BookRepo.Update(entity);    //_context.SaveChanges();
                    _manager.BookService.UpDdateOneBook(id, entity, true);

                    return NoContent(); //204
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
