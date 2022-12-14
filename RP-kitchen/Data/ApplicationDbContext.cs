using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RP_kitchen.Models.Domain;

namespace RP_kitchen.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        internal static string applicationRootPath;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Delicacy> Delicacies { get; set; }
    }
}