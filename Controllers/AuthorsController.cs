using Lab_4.DB;
using Lab_4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context) => _context = context;

        // Listing authors
        [HttpGet]
        public async Task<IEnumerable<Author>> Get()
        {
            return await _context.Authors.ToArrayAsync();
        }

        [HttpGet("(name)")]
        public async Task<IEnumerable<Author>> GetSongs(string name)
        {
             return _context.Authors.Where(x => x.Name == name);
        }

        // Creating new Author
        [HttpPost("{name}/{title}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(string name, string title)
        {
            if (_context.Authors.Where(x => x.SongTitle == title).Count() != 0) return Conflict();
            Author item = new (){ Name = name, SongTitle = title };
            await _context.Authors.AddAsync(item);
            await _context.SaveChangesAsync();

            return Ok(item);
        }

        // Deleting
        [HttpDelete("(name)/(title)")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string name, string title)
        {
            var itemToDelete = _context.Authors.Where(x => x.Name == name && x.SongTitle == title).First();
            
            if (itemToDelete == null) return NotFound();
            _context.Authors.Remove(itemToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
