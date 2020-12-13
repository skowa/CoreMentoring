using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Data.EF
{
    public class UserStoreContext : IdentityDbContext<IdentityUser>
    {
        public UserStoreContext(DbContextOptions<UserStoreContext> options)
            : base(options)
        {
        }
    }
}
