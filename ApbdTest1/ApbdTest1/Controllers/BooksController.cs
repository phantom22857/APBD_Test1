using ApbdTest1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApbdTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            if (!await _booksRepository.DoesBookExist(id))
                return NotFound($"Book with given ID - {id} doesn't exist");

            var book = await _booksRepository.GetBook(id);
            
            return Ok(book);
        }
    }
}