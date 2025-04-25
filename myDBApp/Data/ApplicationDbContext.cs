using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using myDBApp.Models;

namespace myDBApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<MyCourse> Course { get; set; }
        public DbSet<MyStudent> Student { get; set; }

        public DbSet<MyProfessor> Professor { get; set; }
        public DbSet<MyEnrollment> Enrollment { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder) //i this function yourself in situations where you want to build a keyless
                                                                           //model but if so, it won't be able to carry out actions like update or delete the
                                                                           //data associated with that model using the Entity Framework Core (EF Core).
        {

        }*/
    }
}
