using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
  public class LoginContext: DbContext
  {
    public LoginContext(DbContextOptions<LoginContext> options)
        : base(options)
    {
    }

    public DbSet<Login> Login { get; set; }
  }
}
