using System.Net;
using Domain.DTOs.BookDTO;
using Domain.DTOs.LoanDTO;
using Domain.Entities;
using Domain.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class LoanService(DataContext context) : ILoanService
{
   public async Task<Response<List<GetLoanDTO>>> GetLoans()
{
    try
    {
        var loans = await context.Loans.ToListAsync();
        var list = new List<GetLoanDTO>();
        foreach (var loan in loans)
        {
            var loanDto = new GetLoanDTO()
            {
                BookId = loan.BookId,
                MemberId = loan.MemberId,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate,
                Member = loan.Member
            };
            list.Add(loanDto);
        }
        return new Response<List<GetLoanDTO>>(list);
    }
    catch (Exception e)
    {
        return new Response<List<GetLoanDTO>>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<GetLoanDTO>> GetLoanById(int id)
{
    try
    {
        var loan = await context.Loans.FirstOrDefaultAsync(x => x.Id == id);
        if (loan == null) return new Response<GetLoanDTO>(HttpStatusCode.BadRequest, "Not found");
        var response = new GetLoanDTO()
        {
            BookId = loan.BookId,
            MemberId = loan.MemberId,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            Member = loan.Member
        };
        return new Response<GetLoanDTO>(response);
    }
    catch (Exception e)
    {
        return new Response<GetLoanDTO>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<string>> AddLoan(AddLoanDTO loan)
{
    try
    {
        var newLoan = new Loan()
        {
            BookId = loan.BookId,
            MemberId = loan.MemberId,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            Member = loan.Member
        };
        await context.Loans.AddAsync(newLoan);
        var res = await context.SaveChangesAsync();
        if (res > 0) return new Response<string>("Successfully added");
        return new Response<string>(HttpStatusCode.BadRequest, "Failed to add");
    }
    catch (Exception e)
    {
        return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<string>> UpdateLoan(UpdateLoanDTO loan)
{
    try
    {
        var updateLoan = await context.Loans.FirstOrDefaultAsync(x => x.Id == loan.Id);
        if (updateLoan == null) return new Response<string>("Not found");
        updateLoan.BookId = loan.BookId;
        updateLoan.MemberId = loan.MemberId;
        updateLoan.LoanDate = loan.LoanDate;
        updateLoan.ReturnDate = loan.ReturnDate;
        updateLoan.Member = loan.Member;
        var res = await context.SaveChangesAsync();
        if (res > 0) return new Response<string>("Successfully updated");
        return new Response<string>("Failed to update");
    }
    catch (Exception e)
    {
        return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
    }
}

public async Task<Response<bool>> DeleteLoan(int id)
{
    try
    {
        var response = await context.Loans.FindAsync(id);
        if (response == null) return new Response<bool>(HttpStatusCode.BadRequest, "Not found");
        context.Loans.Remove(response);
        var res = await context.SaveChangesAsync();
        return new Response<bool>(true);
    }
    catch (Exception e)
    {
        return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
    }
}

}