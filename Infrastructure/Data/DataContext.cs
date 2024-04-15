using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options):base(options)
    {
        
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Loan>().HasKey(x => x.Id);
        modelBuilder.Entity<Loan>().HasOne(x => x.Member).WithMany(x => x.Loans).HasForeignKey(x=>x.MemberId);
        modelBuilder.Entity<Loan>().HasMany(x => x.Books).WithOne(x => x.Loan).HasForeignKey(x=>x.Id);
        modelBuilder.Entity<Loan>().Property(x => x.LoanDate).HasColumnType("date");
        modelBuilder.Entity<Loan>().Property(x => x.ReturnDate).HasColumnType("date");
        base.OnModelCreating(modelBuilder);
    }
}