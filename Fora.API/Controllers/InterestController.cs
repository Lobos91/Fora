using Fora.API.Data;
using Fora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Fora.API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : Controller
    {
        private readonly AppDbContext _context;
        public InterestController(AppDbContext context) { _context = context; }

        [HttpGet]

        public async Task<IActionResult> GetAllInterests()
        {
            var interests = _context.Interests
                                        .Include(u => u.User)
                                        .ToList();

            return Ok(interests);
        }

        [HttpGet("{Id}")]
        public IActionResult GetInterest(int id)
        {
            var interest = _context.Interests.FirstOrDefault(x => x.Id == id);

            if (interest == null)
            {
                return BadRequest();
            }

            return Ok(interest);
        }

        [HttpPost]
        public async Task<IActionResult> AddInterest(InterestModel interest)
        {
            _context.Interests.Add(interest);
        
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id}")]

        public async Task DeleteInterest(int id)
        {
            var interestToDelete = _context.Interests.FirstOrDefault(x => x.Id == id);

            _context.Interests.Remove(interestToDelete);
            await _context.SaveChangesAsync();
        }


    }
}
