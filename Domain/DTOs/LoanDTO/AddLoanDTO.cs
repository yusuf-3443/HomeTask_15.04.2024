using Domain.Entities;

namespace Domain.DTOs.LoanDTO;

public class AddLoanDTO
{
    public int BookId { get; set; }
    public int MemberId { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public Member Member { get; set; }
}