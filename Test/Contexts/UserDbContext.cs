using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Test.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test.Contexts
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("users");
                u.HasKey(u => u.Id);
                u.Property(u => u.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("NEWID()");
                u.Property(u => u.UserName).IsRequired();
                u.Property(u => u.PasswordHash).IsRequired();
                u.Property(u => u.IsAdmin).HasDefaultValue(false);
            });
        }
    }
}
