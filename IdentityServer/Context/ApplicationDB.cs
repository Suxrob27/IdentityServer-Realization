using IdentityModel;
using IdentityServer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Context
{
    public class ApplicationDB : IdentityDbContext
    {
        public  DbSet<ApplicationUser> ApplicationUser {  get; set; }   
        public ApplicationDB(DbContextOptions<ApplicationDB> options) : base(options)
        {
                
        }

    }
}
