using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiDemo.Data;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuidelineValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public GuidelineValuesController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.GuidelineValues
                .Join(_context.Contaminants, g => g.ContaminantId, c => c.Id, (g, c) => new { g, c.Name })
                .Join(_context.SoilTypes, gc => gc.g.SoilTypeId, s => s.Id, (gc, s) => new { gc.g, gc.Name, SoilName = s.Name })
                .Join(_context.Pathways, gcs => gcs.g.PathwayId, p => p.Id, (gcs, p) => new {
                    gcs.g.Id,
                    Contaminant = gcs.Name,
                    SoilType = gcs.SoilName,
                    Pathway = p.Name,
                    gcs.g.Guideline_Value,
                    gcs.g.CreatedAt,
                    gcs.g.UpdatedAt
                })
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _context.GuidelineValues
                .Where(g => g.Id == id)
                .Join(_context.Contaminants, g => g.ContaminantId, c => c.Id, (g, c) => new { g, c.Name })
                .Join(_context.SoilTypes, gc => gc.g.SoilTypeId, s => s.Id, (gc, s) => new { gc.g, gc.Name, SoilName = s.Name })
                .Join(_context.Pathways, gcs => gcs.g.PathwayId, p => p.Id, (gcs, p) => new {
                    gcs.g.Id,
                    Contaminant = gcs.Name,
                    SoilType = gcs.SoilName,
                    Pathway = p.Name,
                    gcs.g.Guideline_Value,
                    gcs.g.CreatedAt,
                    gcs.g.UpdatedAt
                })
                .FirstOrDefaultAsync();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GuidelineValue g)
        {
            try
            {
                bool exists = await _context.GuidelineValues.AnyAsync(x =>
                    x.ContaminantId == g.ContaminantId &&
                    x.SoilTypeId == g.SoilTypeId &&
                    x.PathwayId == g.PathwayId
                );
                if (exists)
                {
                    return Conflict(new { message = "A guideline value for this contaminant, soil type, and pathway already exists." });
                }
                g.CreatedAt = DateTime.UtcNow;
                g.UpdatedAt = DateTime.UtcNow;
                _context.GuidelineValues.Add(g);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(Get), new { id = g.Id }, g);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GuidelineValue g)
        {
            if (id != g.Id) return BadRequest(new { message = "ID mismatch." });
            _context.Entry(g).State = EntityState.Modified;
            g.UpdatedAt = DateTime.UtcNow;
            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.GuidelineValues.Any(e => e.Id == id))
                    return NotFound(new { message = "Guideline value not found." });
                else
                    return StatusCode(500, new { message = "A concurrency error occurred." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var item = await _context.GuidelineValues.FindAsync(id);
                if (item == null) return NotFound(new { message = "Guideline value not found." });
                _context.GuidelineValues.Remove(item);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}