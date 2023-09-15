using billing.Models.Account;
using Microsoft.EntityFrameworkCore;

namespace billing.Context;

public class BillDbContext: DbContext
{
    public BillDbContext(DbContextOptions<BillDbContext> options): base(options)
    {
        
    }

    public DbSet<Card> Cards { get; set; }
}