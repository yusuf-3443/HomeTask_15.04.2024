﻿namespace Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
    public int PublishedYear { get; set; }
    public string Genre { get; set; }
    public Loan Loan { get; set; }
    public int LoanId { get; set; }
}