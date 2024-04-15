using Domain.DTOs.LoanDTO;
using Domain.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

    [Route("/api/Loan")]
    [ApiController]
    public class LoanController(ILoanService service) : ControllerBase
    {
        [HttpGet("Get-All-Loans")]
        public async Task<Response<List<GetLoanDTO>>> GetLoans()
        {
            return await service.GetLoans();
        }

        [HttpGet("LoanId:int")]
        public async Task<Response<GetLoanDTO>> GetLoanById(int LoanId)
        {
            return await service.GetLoanById(LoanId);
        }

        [HttpPost("Add-Loan")]
        public async Task<Response<string>> AddLoan(AddLoanDTO add)
        {
            return await service.AddLoan(add);
        }

        [HttpPut("Update-Loan")]
        public async Task<Response<string>> UpdateLoan(UpdateLoanDTO update)
        {
            return await service.UpdateLoan(update);
        }

        [HttpDelete("LoanId:int")]
        public async Task<Response<bool>> DeleteLoan(int LoanId)
        {
            return await service.DeleteLoan(LoanId);
        }
    }

