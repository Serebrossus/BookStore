using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.Models;
using Microsoft.AspNetCore.Authorization;
using BookStore.Classes;
using BookStore.Data;

namespace BookStoreApp.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class BooksController : ControllerBase
  {
    private readonly BookStoreContext _context;

    public BooksController(BookStoreContext context)
    {
      _context = context;
    }

    // GET: api/Books
    [HttpGet, Authorize(Roles = "Manager")]
    public IEnumerable<Book> GetBook()
    {
      return _context.Book;
    }

    // GET: api/Books/5
    [HttpGet("{id}"), Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetBook([FromRoute] string id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var book = await _context.Book.FindAsync(id);

      if (book == null)
      {
        return NotFound();
      }

      return Ok(book);
    }

    [HttpGet, Authorize(Roles = "Operator")]
    public async Task<IActionResult> GetBook(FilteringParams filteringParams)
    {
      var filterBy = filteringParams.FilterBy.Trim().ToLowerInvariant();
      if (string.IsNullOrEmpty(filterBy))
      {
        return BadRequest();
      }
      var books = await _context.Book.Where(x => x.Author.ToLowerInvariant().Contains(filterBy)
        || x.Name.ToLowerInvariant().Contains(filterBy)).ToListAsync();
      if (books.Count() == 0)
      {
        return NotFound();
      }
      return Ok(books);
    }

    [HttpGet, Authorize(Roles = "Operator")]
    public async Task<IActionResult> GetBook(SortingParams sortingParams)
    {
      var sortingBy = sortingParams.FieldName.Trim().ToLowerInvariant();
      if (string.IsNullOrEmpty(sortingBy))
      {
        return BadRequest();
      }
      var field = typeof(Book).GetProperty(sortingBy);
      List<Book> books;
      if (sortingParams.Ascending)
      {
        books = await _context.Book.OrderBy(x => field.GetValue(x, null)).ToListAsync();
      }
      else
      {
        books = await _context.Book.OrderByDescending(x => field.GetValue(x, null)).ToListAsync();
      }
      if (books.Count() == 0)
      {
        return NotFound();
      }
      return Ok(books);
    }

    // PUT: api/Books/5
    [HttpPut("{id}"), Authorize(Roles = "Operator")]
    public async Task<IActionResult> PutBook([FromRoute] int id, [FromBody] Book book)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != book.Id)
      {
        return BadRequest();
      }

      _context.Entry(book).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BookExists(id))
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

    // POST: api/Books
    [HttpPost, Authorize(Roles = "Operator")]
    public async Task<IActionResult> PostBook([FromBody] Book book)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Book.Add(book);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetBook", new { id = book.Id }, book);
    }

    // DELETE: api/Books/5
    [HttpDelete("{id}"), Authorize(Roles = "Operator")]
    public async Task<IActionResult> DeleteBook([FromRoute] string id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var book = await _context.Book.FindAsync(id);
      if (book == null)
      {
        return NotFound();
      }

      _context.Book.Remove(book);
      await _context.SaveChangesAsync();

      return Ok(book);
    }

    private bool BookExists(int id)
    {
      return _context.Book.Any(e => e.Id == id);
    }
  }
}
