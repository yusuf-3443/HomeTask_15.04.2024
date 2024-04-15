using System.Net;
using Domain.DTOs.BookDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BookService(DataContext context) : IBookService
{
    public async Task<Response<List<GetBookDTO>>> GetBooks()
    {
        try
        {
            var books = await context.Books.Where(x => x.Id > 0).ToListAsync();
            var list = new List<GetBookDTO>();
            foreach (var b in books)
            {
                var book = new GetBookDTO()
                {
                    Title = b.Title,
                    Author = b.Author,
                    PublishedYear = b.PublishedYear,
                    Genre = b.Genre
                };
                list.Add(book);
            }

            return new Response<List<GetBookDTO>>(list);
        }
        catch (Exception e)
        {
            return new Response<List<GetBookDTO>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetBookDTO>> GetBookById(int id)
    {
        try
        {
        var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
        if (book == null) return new Response<GetBookDTO>(HttpStatusCode.BadRequest, "Not found");
        var response = new GetBookDTO()
        {
        Title = book.Title,
        Author = book.Author,
        PublishedYear = book.PublishedYear,
        Genre = book.Genre
        };
        return new Response<GetBookDTO>(response);
        }
        catch (Exception e)
        {
            return new Response<GetBookDTO>(HttpStatusCode.InternalServerError, e.Message);
        }
        }

    public async Task<Response<string>> AddBook(AddBookDTO book)
    {
        try
        {
            var newBook = new Book()
            {
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear,
                Genre = book.Genre
            };
            await context.Books.AddAsync(newBook);
            var res = await context.SaveChangesAsync();
            if (res > 0) return new Response<string>("Successfully added");
            return new Response<string>(HttpStatusCode.BadRequest, "Failed to add");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> UpdateBook(UpdateBookDTO book)
    {
        try
        {

        var updatebook = await context.Books.FirstOrDefaultAsync(x => x.Id == book.Id);
        if (updatebook == null) return new Response<string>("Not found");
        updatebook.Title = book.Title;
        updatebook.Author = book.Author;
        updatebook.PublishedYear = book.PublishedYear;
        updatebook.Genre = book.Genre;
        var res = await context.SaveChangesAsync();
        if (res > 0) return new Response<string>("Successfully updated");
        return new Response<string>("Failed to update");
        
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
        }

    public async Task<Response<bool>> DeleteBook(int id)
    {
        try
        {
            var response = await context.Books.FindAsync(id);
            if (response == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not found");
            context.Books.Remove(response);
            var res = await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}