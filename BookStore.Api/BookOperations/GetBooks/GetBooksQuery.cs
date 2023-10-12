using BookStore.Api.Common;
using BookStore.Api.DbOperations;

namespace BookStore.Api.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly Context _dbContext;
        public GetBooksQuery(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BooksViewModel> vm = new List<BooksViewModel>();
            foreach (var book in bookList)
            {
                vm.Add(new BooksViewModel
                {
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishDate = book.PublishDate.ToString("dd/MM/yyyy"),
                    Title = book.Title
                });
            }
            return vm;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
