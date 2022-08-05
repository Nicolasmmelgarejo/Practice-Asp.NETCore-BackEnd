using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.data;
using backend.model;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectsController : ControllerBase
    {
        private readonly DataContext _context;

        public ObjectsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Objects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Objects>>> GetObjects()
        {
          if (_context.Objects == null)
          {
              return NotFound();
          }
            return await _context.Objects.ToListAsync();
        }

        // GET: api/Objects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Objects>> GetObjects(int id)
        {
          if (_context.Objects == null)
          {
              return NotFound();
          }
            var objects = await _context.Objects.FindAsync(id);

            if (objects == null)
            {
                return NotFound();
            }

            return objects;
        }

        // PUT: api/Objects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObjects(int id, Objects objects)
        {
            if (id != objects.Id)
            {
                return BadRequest();
            }

            _context.Entry(objects).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectsExists(id))
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

        // POST: api/Objects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Objects>> PostObjects(Objects objects)
        {
          if (_context.Objects == null)
          {
              return Problem("Entity set 'DataContext.Objects'  is null.");
          }
            _context.Objects.Add(objects);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetObjects", new { id = objects.Id }, objects);
        }

        // DELETE: api/Objects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObjects(int id)
        {
            if (_context.Objects == null)
            {
                return NotFound();
            }
            var objects = await _context.Objects.FindAsync(id);
            if (objects == null)
            {
                return NotFound();
            }

            _context.Objects.Remove(objects);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObjectsExists(int id)
        {
            return (_context.Objects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
