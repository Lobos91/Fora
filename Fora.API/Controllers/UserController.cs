using Fora.API.Data;
using Fora.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fora.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private readonly AppDbContext _context;
        public UserController(AppDbContext context) { _context = context; }

        [HttpGet]

        public async Task<IActionResult> GetUsers()
        {
            var threadList = _context.Users.ToList();

            return Ok(threadList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var requestedUser = _context.Users.FirstOrDefault(x => x.Id == id);

            if (requestedUser == null)
            {
                return null;
            }

            return Ok(requestedUser);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(x => x.Id == id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }
    
    }
}
