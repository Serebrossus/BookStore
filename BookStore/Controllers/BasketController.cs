using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using BookStore.Data;

namespace BookStoreApp.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class BasketController : ControllerBase
  {

    private readonly BookStoreContext _context;

    public BasketController(BookStoreContext context)
    {
      _context = context;
    }
    // GET: api/Basket
    [HttpGet, Authorize(Roles = "Manager")]
    public IEnumerable<Basket> GetBasket()
    {
      return _context.Basket;
    }

    // PUT: api/Basket/5
    [HttpPut("{id}"), Authorize(Roles = "Manager")]
    public async Task<IActionResult> PutBasket([FromRoute] int id, [FromBody] Basket basket)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != basket.Id)
      {
        return BadRequest();
      }

      _context.Entry(basket).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BasketExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }


    [HttpPost, Authorize(Roles = "Manager")]
    public async Task<IActionResult> PostBasket([FromRoute] int id, [FromBody] Book book)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var basket = await _context.Basket.FindAsync(id);
      if (basket == null)
      {
        return BadRequest();
      }
      var findBook = await _context.Book.FindAsync(book.Id);
      if (findBook == null)
      {
        return BadRequest();
      }

      var bookExist = basket.Books.FirstOrDefault(x => x.Book.Id == book.Id);
      if (bookExist == null)
      {
        basket.Books.Append(new BookInBasket
        {
          Book = book,
          Count = 1
        });
      }
      else
      {
        bookExist.Count++;
      }
      await _context.SaveChangesAsync();

      findBook.TotalAmount--;
      await _context.SaveChangesAsync();

      return Ok(basket);
    }

    // POST: api/Basket
    [HttpPost, Authorize(Roles = "Operator")]
    public async Task<IActionResult> PostBasket([FromBody] Basket basket)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Basket.Add(basket);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetBasket", new { id = basket.Id }, basket);
    }

    // DELETE: api/Basket/5
    [HttpDelete("{id}"), Authorize(Roles = "Operator")]
    public async Task<IActionResult> DeleteBasket([FromRoute] string id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var basket = await _context.Basket.FindAsync(id);
      if (basket == null)
      {
        return NotFound();
      }

      _context.Basket.Remove(basket);
      await _context.SaveChangesAsync();

      return Ok(basket);
    }

    private bool BasketExists(int id)
    {
      return _context.Basket.Any(e => e.Id == id);
    }
  }
}
