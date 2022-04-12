using Fora.API.Data;
using Fora.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fora.API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : Controller
    {
        private readonly AppDbContext _context;
        public MessageController(AppDbContext context) { _context = context; }

        [HttpGet]

        public async Task<IActionResult> GetAllMesages()
        {
            var messages = _context.Messages.Include(u => u.User).ToList();
            return Json(new { data = messages });
        }

        [HttpGet("{Id}")]
        public IActionResult GetMessage(int id)
        {
            var message = _context.Messages.Include(u => u.User).FirstOrDefault(x => x.Id == id);

            if (message == null)
            {
                return BadRequest();
            }

            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(MessageModel message)
        {
            _context.Messages.Add(message);
        
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var messageToDelete = _context.Messages.FirstOrDefault(x => x.Id == id);

            if (messageToDelete == null)
            {
              return NotFound();
            }
           
            _context.Messages.Remove(messageToDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }

   
    }
}
