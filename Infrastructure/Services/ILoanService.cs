using Domain.DTOs.BookDTO;
using Domain.DTOs.LoanDTO;
using Domain.Entities;
using Domain.Responses;

namespace Infrastructure.Services;

public interface ILoanService
{
    Task<Response<List<GetLoanDTO>>> GetLoans();
    Task<Response<GetLoanDTO>> GetLoanById(int id);
    Task<Response<string>> AddLoan(AddLoanDTO loan);
    Task<Response<string>> UpdateLoan(UpdateLoanDTO loan);
    Task<Response<bool>> DeleteLoan(int id);
}