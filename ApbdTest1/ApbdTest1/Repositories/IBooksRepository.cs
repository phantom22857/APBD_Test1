using ApbdTest1.Models.DTOs;
namespace ApbdTest1.Repositories;


public interface IBooksRepository 
{
    Task<bool> DoesBookExist(int id);
    Task<bool> DoesAuthorExist(int id);
    Task<bool> DoesPublishingHouseExist(int id);
    Task<bool> DoesGenreExist(int id);
    Task<BooksDto> GetBook(int id);
    
    
}