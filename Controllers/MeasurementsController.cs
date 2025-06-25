using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDemo.Data;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MeasurementsController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.Measurements
                .Join(_context.Contaminants, m => m.ContaminantId, c => c.Id, (m, c) => new { m, Contaminant = c.Name })
                .Join(_context.SoilTypes, mc => mc.m.SoilTypeId, s => s.Id, (mc, s) => new {
                    id = mc.m.Id,
                    contaminant = mc.Contaminant,
                    measuredValue = mc.m.MeasuredValue,
                    soilType = s.Name,
                    createdAt = mc.m.CreatedAt,
                    updatedAt = mc.m.UpdatedAt
                })
                .OrderByDescending(x => x.createdAt)
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _context.Measurements
                .Where(m => m.Id == id)
                .Join(_context.Contaminants, m => m.ContaminantId, c => c.Id, (m, c) => new { m, Contaminant = c.Name })
                .Join(_context.SoilTypes, mc => mc.m.SoilTypeId, s => s.Id, (mc, s) => new {
                    id = mc.m.Id,
                    contaminant = mc.Contaminant,
                    measuredValue = mc.m.MeasuredValue,
                    soilType = s.Name,
                    createdAt = mc.m.CreatedAt,
                    updatedAt = mc.m.UpdatedAt
                })
                .FirstOrDefaultAsync();
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Measurement m)
        {
            m.CreatedAt = DateTime.UtcNow;
            m.UpdatedAt = DateTime.UtcNow;
            _context.Measurements.Add(m);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = m.Id }, m);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Measurement m)
        {
            if (id != m.Id) return BadRequest();
            m.UpdatedAt = DateTime.UtcNow;
            _context.Entry(m).State = EntityState.Modified;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) { if (!_context.Measurements.Any(e => e.Id == id)) return NotFound(); else throw; }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Measurements.FindAsync(id);
            if (item == null) return NotFound();
            _context.Measurements.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}