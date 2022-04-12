using Fora.API.Data;
using Fora.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fora.API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadModelController : Controller
    {
        private readonly AppDbContext _context;
        public ThreadModelController(AppDbContext context) { _context = context; }

        [HttpGet]

        public async Task<IActionResult> GetAllThreads()
        {
            var threadList = _context.Threads.ToList();
            return Json(new { data = threadList });
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetThread(int threadId)
        {
            var requestedThread =  _context.Threads.FirstOrDefault(x => x.Id == threadId);

            if (requestedThread == null)
            {
                return BadRequest();
            }

            return Ok(requestedThread);
        }

        [HttpPost]
        public async Task<IActionResult> PostThread(ThreadModel thread)
        {
            _context.Threads.Add(thread);
        
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteThread(int threadId)
        {
            var threadToDelete = _context.Threads.FirstOrDefault(x => x.Id == threadId);

            if (threadToDelete == null)
            {
              return NotFound();
            }
           
            _context.Threads.Remove(threadToDelete);
            await _context.SaveChangesAsync();
            return Ok();
        }

   
    }
}
