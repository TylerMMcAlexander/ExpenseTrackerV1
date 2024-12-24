using ExpenseTrackerv1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerv1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Expense> Expenses { get; set; }

public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



        }
    }
}
