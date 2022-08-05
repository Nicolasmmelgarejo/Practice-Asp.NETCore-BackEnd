using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.data;
using backend.model;
using Microsoft.AspNetCore.Authorization;
using backend.Services;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public UserController(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
          List<User> UserList =  await _context.Users.ToListAsync();
          List<User_Role> User_Roles = await _context.User_Roles.ToListAsync();
            for (int i=0;i>UserList.Count;i++)
            {
                for(int j = 0; j > User_Roles.Count; j++)
                {
                    if (User_Roles[j].UserId == UserList[i].Id)
                    {
                        UserList[i].User_Roles.Add(User_Roles[j]);
                    }   
                }
            }
            return UserList;

        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);
            List<User_Role> User_Roles = await _context.User_Roles.ToListAsync();
            if (user == null)
            {
                return NotFound();
            }
            for (int j = 0; j > User_Roles.Count; j++)
            {
                if (User_Roles[j].UserId == user.Id)
                {
                    user.User_Roles.Add(User_Roles[j]);
                }
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            string name = "";
            List<User_Role> userR = await _context.User_Roles.ToListAsync();
            List<User> oLsUser = await _context.Users.ToListAsync();


            foreach (var oUserR in oLsUser.AsEnumerable())
            {
                if (user.Id == oUserR.Id)
                {
                    name = oUserR.UserName;
                }
            }
            for (int i = 0; i < userR.Count; i++)
            {

                if (userR[i].UserId == user.Id)
                {
                    _context.User_Roles.Remove(userR[i]);
                    _context.SaveChanges();
                }
            }
            List<User> oLsUserList = await _context.Users.ToListAsync();
            if (oLsUserList != null)
            {
                for (int i = 0; i < oLsUserList.Count; i++)
                {
                    if (name == oLsUserList[i].UserName)
                    {
                        _context.Remove(oLsUserList[i]);
                        _context.SaveChanges();
                    }
                }


            }


            if (oLsUserList != null)
            {
                for (int i = 0; i < oLsUserList.Count; i++)
                {
                    if (user.UserName == oLsUserList[i].UserName)
                    {
                        return Conflict("Fail");
                    }
                }
            }
            user.Id = 0;
            _context.Add(user);

            try
            {
                _context.SaveChanges();
                return Ok("Success");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'DataContext.Users'  is null.");
          }
            int bandera = 0;
            List<User> userList = await _context.Users.ToListAsync();
            for (int i = 0; i < userList.Count; i++)
            {
                if (user.UserName == userList[i].UserName)
                {
                    bandera = 1;
                }
            }
            if (bandera == 0)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                CreatedAtAction("GetUser", new { id = user.Id }, user);
                return Ok("Success");
            }
            else
            {

                return NotFound("Fail");
                bandera = 0;
            }
        }

        [AllowAnonymous]
        [HttpPost("validar")]
        public async Task<ActionResult<User>> ValidationUser(User user)
        {
            int bandera = 0;
            List<User> userList = await _context.Users.ToListAsync();
            List<User_Role> userR = await _context.User_Roles.ToListAsync();
            for (int i = 0; i < userList.Count; i++)
            {
                for (int j = 0; j < userR.Count; j++)
                {
                    if (userR[j].UserId == userList[i].Id)
                    {
                        userList[i].User_Roles.Add(userR[j]);
                    }

                }
            }
            for (int i = 0; i < userList.Count; i++)
            {
                if (user.UserName == userList[i].UserName && user.UserPassword == userList[i].UserPassword)
                {
                    bandera = 1;
                }
            }
            if (bandera == 0)
            {
                return NotFound("Fail");
            }
            else
            {
                bandera = 0;
                return Ok(new JwtService(_config).GenerateToken(
                        user.UserName,
                        user.UserPassword
                    )
                );
            }

        }



        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            List<User_Role> userR = await _context.User_Roles.ToListAsync();

            if (user == null)
            {
                return NotFound();
            }

            for (int i = 0; i > userR.Count; i++)
            {

                if (userR[i].UserId == user.Id)
                {
                    user.User_Roles.Add(userR[i]);
                }
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }



        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
