using Fora.API.Data;
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
            if (threadId == null)
            {
                return BadRequest();
            }

            return Ok(_context.Threads.FirstOrDefault(x => x.Id == threadId));
        }

        [HttpPost]
        public async Task<IActionResult> PostThread(Thread thread)
        {
           // _context.Threads.Add(thread);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
