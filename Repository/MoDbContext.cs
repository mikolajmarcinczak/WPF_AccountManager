using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AccountManager.Models;

namespace AccountManager.Repository
{
    public partial class MoDbContext : DbContext
    {
        public MoDbContext()
        {
        }

        public MoDbContext(DbContextOptions<MoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Information> Informations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=ESPER\\SQLEXPRESSLOCAL;Initial Catalog=AccountManagerDBFirst;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Bills__UserId__267ABA7A");
            });

            modelBuilder.Entity<Information>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Information)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Informati__UserI__29572725");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.IsPaid).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
