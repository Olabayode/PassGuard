using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PASSGUARD.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PASSGUARD.DAL
{
    public class PASSGUARDContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Pass> Passes { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }

        public PASSGUARDContext(DbContextOptions<PASSGUARDContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Visitor → Pass (1 to many)
            modelBuilder.Entity<Pass>()
                .HasOne(p => p.Visitor)
                .WithMany(v => v.Passes)
                .HasForeignKey(p => p.VisitorId)
                .OnDelete(DeleteBehavior.Cascade);

            // CheckIn → Pass
            modelBuilder.Entity<CheckIn>()
                .HasOne(c => c.Pass)
                .WithMany()
                .HasForeignKey(c => c.PassId);

            // CheckIn → Security
            modelBuilder.Entity<CheckIn>()
                .HasOne(c => c.Security)
                .WithMany()
                .HasForeignKey(c => c.SecurityId);
        }
    }
}