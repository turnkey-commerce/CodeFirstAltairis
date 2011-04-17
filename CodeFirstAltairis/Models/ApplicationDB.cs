using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeFirstAltairis.Models;

namespace CodeFirstAltairis.Models
{
    public class ApplicationDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            // Maps to the expected many-to-many join table name for roles to users.
            modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .Map(m =>
            {
                m.ToTable("RoleMemberships");
                m.MapLeftKey("UserName");
                m.MapRightKey("RoleName");
            });
        }
    }

    // Change the base class as follows if you want to drop and create the database during development:
    public class DBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDB> 
    //public class DBInitializer : CreateDatabaseIfNotExists<ApplicationDB> 
    {
        protected override void Seed(ApplicationDB context) 
        {
            var roles = new List<Role>{
                new Role{RoleName = "Administrator"},
                new Role{RoleName = "User"},
                new Role{RoleName = "PowerUser"}
            };

            roles.ForEach(r => context.Roles.Add(r));
        }
    }
}