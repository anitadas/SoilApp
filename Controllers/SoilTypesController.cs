using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDemo.Data;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SoilTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public SoilTypesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.SoilTypes.OrderByDescending(x => x.CreatedAt).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.SoilTypes.FindAsync(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SoilType s)
        {
            s.CreatedAt = DateTime.UtcNow;
            s.UpdatedAt = DateTime.UtcNow;
            _context.SoilTypes.Add(s);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = s.Id }, s);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SoilType s)
        {
            if (id != s.Id) return BadRequest();
            s.UpdatedAt = DateTime.UtcNow;
            _context.Entry(s).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.SoilTypes.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.SoilTypes.FindAsync(id);
            if (item == null) return NotFound();
            _context.SoilTypes.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}