using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeFirstAltairis.Models;
using System.Web.Security;
using System.Data;

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
            // Create some roles.
            var roles = new List<Role>{
                new Role{RoleName = "Administrator"},
                new Role{RoleName = "User"},
                new Role{RoleName = "PowerUser"}
            };
            roles.ForEach(r => context.Roles.Add(r));

            // Create some users.
            MembershipCreateStatus status = new MembershipCreateStatus();
            Membership.CreateUser("admin", "password", "admin@user.com");
            if (status == MembershipCreateStatus.Success) {
                // Add the role.
                User admin = context.Users.Find("admin");
                Role adminRole = context.Roles.Find("Administrator");
                admin.Roles = new List<Role>();
                admin.Roles.Add(adminRole);
            }
            Membership.CreateUser("user", "password", "user@user.com");
            if (status == MembershipCreateStatus.Success) {
                // Add the role.
                User user = context.Users.Find("user");
                Role userRole = context.Roles.Find("User");
                user.Roles = new List<Role>();
                user.Roles.Add(userRole);
            }
            
        }
    }
}