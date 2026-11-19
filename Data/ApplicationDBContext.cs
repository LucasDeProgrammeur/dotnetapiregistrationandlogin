using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using auth_api.Models;

namespace auth_api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fixed IDs, so EF doesn't delete and re-insert the roles in every new migration
            List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Id = "8d04dce2-969a-435d-bba4-df3f325983dc",
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },
                new IdentityRole()
                {
                    Id = "c7b013f0-5201-4317-abd8-c211f91b7330",
                    Name = "Member",
                    NormalizedName = "Member".ToUpper()
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
