using Domain.DTOs.BookDTO;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[Route("/api/Book")]
[ApiController]
public class BookController(IBookService service) :ControllerBase
{
    [HttpGet("Get-All-Books")]
    public async Task<Response<List<GetBookDTO>>> GetBooks()
    {
        return await service.GetBooks();
    }

    [HttpGet("BookId:int")]
    public async Task<Response<GetBookDTO>> GetBookById(int BookId)
    {
        return await service.GetBookById(BookId);
    }

    [HttpPost("Add-Book")]
    public async Task<Response<string>> AddBook(AddBookDTO add)
    {
        return await service.AddBook(add);
    }

    [HttpPut("Update-Book")]

    public async Task<Response<string>> UpdateBook(UpdateBookDTO update)
    {
        return await service.UpdateBook(update);
    }

    [HttpDelete("bookId:int")]
    public async Task<Response<bool>> DeleteBook(int BookId)
    {
        return await service.DeleteBook(BookId);
    }


}