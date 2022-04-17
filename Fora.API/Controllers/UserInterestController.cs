using Fora.API.Data;
using Fora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Fora.API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class UserInterestController : Controller
    {
        private readonly AppDbContext _context;
        public UserInterestController(AppDbContext context) { _context = context; }

        [HttpGet]

        public async Task<IActionResult> GetAllUserInterests()
        {
            var interests = _context.UserInterests.Include(u => u.User).ToList();
                                      

            return Ok(interests);
        }

        [HttpGet("{Id}")]
        public IActionResult GetUserInterest(int id)
        {
            var interest = _context.UserInterests.FirstOrDefault(x => x.InterestId == id);

            if (interest == null)
            {
                return BadRequest();
            }

            return Ok(interest);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserInterest(UserInterestModel interest)
        {
            _context.UserInterests.Add(interest);
        
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInterest(int id)
        {
            var userInterestToDelete = _context.UserInterests.FirstOrDefault(x => x.InterestId == id);

            if (userInterestToDelete == null)
            {
              return NotFound();
            }
           
            _context.UserInterests.Remove(userInterestToDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }

   
    }
}
