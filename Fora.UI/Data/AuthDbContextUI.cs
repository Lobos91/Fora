using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fora.UI.Data
{
    public class AuthDbContextUI : IdentityDbContext
    {
        public AuthDbContextUI(DbContextOptions<AuthDbContextUI> options) : base(options)
        {

        }
    }
}
