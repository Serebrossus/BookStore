using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreApp.Models
{
  public class Basket
  {
    public int Id { get; set; }
    public int UserId { get; set; }
    public IEnumerable<BookInBasket> Books { get; set; }
    // Время формирования корзины
    public DateTime CreateTime { get; set; }
  }

  public class BookInBasket {
    public Book Book { get; set; }
    public int Count { get; set; }
  }
}
