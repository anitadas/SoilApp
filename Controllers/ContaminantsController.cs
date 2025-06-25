using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDemo.Data;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaminantsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ContaminantsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Contaminants.OrderByDescending(x => x.CreatedAt).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Contaminants.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contaminant c)
        {
            c.CreatedAt = DateTime.UtcNow;
            c.UpdatedAt = DateTime.UtcNow;
            _context.Contaminants.Add(c);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = c.Id }, c);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Contaminant c)
        {
            if (id != c.Id) return BadRequest();
            c.UpdatedAt = DateTime.UtcNow;
            _context.Entry(c).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.Contaminants.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Contaminants.FindAsync(id);
            if (item == null) return NotFound();
            _context.Contaminants.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}