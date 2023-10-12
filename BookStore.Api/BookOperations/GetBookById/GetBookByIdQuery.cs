using BookStore.Api.BookOperations.GetBooks;
using BookStore.Api.Common;
using BookStore.Api.DbOperations;

namespace BookStore.Api.BookOperations.GetBookById
{
    public class GetBookByIdQuery
    {
        private readonly Context _dbContext;
        public GetBookByIdQuery(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public BooksViewModel Handle(int id)
        {
            var book = _dbContext.Books.Where(x => x.Id == id).SingleOrDefault();
            var vm = new BooksViewModel
            {
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PageCount = book.PageCount,
                PublishDate = book.PublishDate.ToString("dd/MM/yyyy"),
                Title = book.Title
            };
            return vm;
        }
    }
}
