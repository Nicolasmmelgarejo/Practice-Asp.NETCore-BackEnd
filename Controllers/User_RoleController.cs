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
    public class User_RoleController : ControllerBase
    {
        private readonly DataContext _context;

        public User_RoleController(DataContext context)
        {
            _context = context;
        }

        // GET: api/User_Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User_Role>>> GetUser_Roles()
        {
          if (_context.User_Roles == null)
          {
              return NotFound();
          }
            return await _context.User_Roles.ToListAsync();
        }

        // GET: api/User_Role/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User_Role>> GetUser_Role(int id)
        {
          if (_context.User_Roles == null)
          {
              return NotFound();
          }
            var user_Role = await _context.User_Roles.FindAsync(id);

            if (user_Role == null)
            {
                return NotFound();
            }

            return user_Role;
        }

        // PUT: api/User_Role/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser_Role(int id, User_Role user_Role)
        {
            if (id != user_Role.Id)
            {
                return BadRequest();
            }

            _context.Entry(user_Role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!User_RoleExists(id))
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

        // POST: api/User_Role
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User_Role>> PostUser_Role(User_Role user_Role)
        {
          if (_context.User_Roles == null)
          {
              return Problem("Entity set 'DataContext.User_Roles'  is null.");
          }
            _context.User_Roles.Add(user_Role);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser_Role", new { id = user_Role.Id }, user_Role);
        }

        // DELETE: api/User_Role/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser_Role(int id)
        {
            if (_context.User_Roles == null)
            {
                return NotFound();
            }
            var user_Role = await _context.User_Roles.FindAsync(id);
            if (user_Role == null)
            {
                return NotFound();
            }

            _context.User_Roles.Remove(user_Role);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool User_RoleExists(int id)
        {
            return (_context.User_Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
