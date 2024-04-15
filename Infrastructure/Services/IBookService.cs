using Domain.DTOs.BookDTO;
using Domain.Responses;

namespace Infrastructure.Services;

public interface IBookService
{
    Task<Response<List<GetBookDTO>>> GetBooks();
    Task<Response<GetBookDTO>> GetBookById(int id);
    Task<Response<string>> AddBook(AddBookDTO book);
    Task<Response<string>> UpdateBook(UpdateBookDTO book);
    Task<Response<bool>> DeleteBook(int id);
}