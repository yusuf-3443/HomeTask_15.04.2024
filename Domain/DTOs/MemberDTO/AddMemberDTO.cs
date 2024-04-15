﻿using Domain.Entities;

namespace Domain.DTOs.MemberDTO;

public class AddMemberDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<Loan> Loans { get; set; }
}