using IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Context
{
    public class ApplicationDB : IdentityDbContext
    {
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options)
        {
                
        }

    }
}
