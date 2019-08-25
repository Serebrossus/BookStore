using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Models
{
  public class BasketContext : DbContext
  {
    public BasketContext(DbContextOptions<BasketContext> options)
        : base(options)
    {
    }

    public DbSet<BookStoreApp.Models.Basket> Basket { get; set; }
  }
}
