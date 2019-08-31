using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
  public class BookStoreContext : DbContext
  {
    public DbSet<BookStoreApp.Models.Book> Book { get; set; }
    public DbSet<BookStoreApp.Models.Basket> Basket { get; set; }
    public BookStoreContext(DbContextOptions<BookStoreContext> options)
             : base(options)
    {
      Database.EnsureCreated();
    }
  }
}
