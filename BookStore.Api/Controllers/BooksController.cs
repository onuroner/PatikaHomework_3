using BookStore.Api.BookOperations.CreateBook;
using BookStore.Api.BookOperations.GetBookById;
using BookStore.Api.BookOperations.GetBooks;
using BookStore.Api.BookOperations.UpdateBook;
using BookStore.Api.DbOperations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Context _dbContext;

        public BooksController(Context dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_dbContext);
            var result = query.Handle();
            return Ok(result);
        
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookByIdQuery query = new GetBookByIdQuery(_dbContext);
            var result = query.Handle(id);
            return Ok(result);
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var result = BookList.Where(x => x.Id == Convert.ToInt32(id)).SingleOrDefault();

        //    return result;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel request)
        {
            CreateBookCommand command = new CreateBookCommand(_dbContext);
            try
            {
                command.Model = request;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_dbContext);
            try
            {
                command.Model = updatedBook;
                command.Handle(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _dbContext.Books.Where(x => x.Id == id).SingleOrDefault();
            if (book is null)
            {
                return BadRequest();
            }
            else
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();
                return Ok(true);
            }
        }
    }
}
