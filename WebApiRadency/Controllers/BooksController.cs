using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRadency.Models;
using WebApiRadency.Models.DTO;

namespace WebApiRadency.Controllers
{
    [Route("api")]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksDbContext _context;
        private readonly IMapper _mapper;
        public BooksController(BooksDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/recommended
        [HttpGet("recommended")]
       
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetTopBooks(string? order)
        {
            return GetBooksItems(order).Result.Value.Where(x => x.Reviews > 10).OrderByDescending(x=>x.Rating).Take(10).ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksItems(string? order)
        {
            await Console.Out.WriteLineAsync(Request.Path.ToString());
            switch (order)
            {
                case "title":
                    return await _context.BooksItems.OrderBy(x => x.Title).Select(x => _mapper.Map<BookDTO>(x))
                        .ToListAsync();

                case "author":
                    return await _context.BooksItems.OrderBy(x => x.Author).Select(x => _mapper.Map<BookDTO>(x))
                         .ToListAsync();

                default:
                    return await _context.BooksItems.Select(x => _mapper.Map<BookDTO>(x)).ToListAsync();
            }

        }
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDTO>> GetBookDetails(int id)
        {
            var book = await _context.BooksItems.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return _mapper.Map<BookDetailsDTO>(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("save")]
        public async Task<ActionResult<Book>> PostBook([FromBody] Book book)
        {
            if (book.Id != 0 && BookExists(book.Id))
            {
                await PutBook(book.Id, book);
                return Ok(new { id = book.Id });
            }
            _context.BooksItems.Add(book);
            await _context.SaveChangesAsync();

            return Ok(new { id = book.Id });
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.BooksItems.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.BooksItems.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/rate")]
        public async Task<IActionResult> RateBook([FromBody] RatingInputDTO rate)

        {
            var id = Convert.ToInt32(Request.Path.ToString().Split('/')[3]);
            var book = await _context.BooksItems.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var rateItem = _mapper.Map<Rating>(rate);
            rateItem.BookId = id;
            rateItem.Book = book;

            _context.RatingItems.Add(rateItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}/review")]
        public async Task<IActionResult> ReviewBook([FromBody] ReviewInputDTO review)

        {
            var id = Convert.ToInt32(Request.Path.ToString().Split('/')[3]);
            var book = await _context.BooksItems.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var reviewItem = _mapper.Map<Review>(review);
            reviewItem.BookId = id;
            reviewItem.Book = book;

            _context.ReviewItems.Add(reviewItem);
            await _context.SaveChangesAsync();

            return Ok(review);
        }



        private bool BookExists(int id)
        {
            return _context.BooksItems.Any(e => e.Id == id);
        }
    }
}
