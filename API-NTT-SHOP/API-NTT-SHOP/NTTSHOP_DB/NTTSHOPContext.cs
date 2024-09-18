using System;
using API_NTT_SHOP.DAC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_NTT_SHOP.NTTSHOP_DB
{
    public partial class NTTSHOPContext : DbContext
    {
        public NTTSHOPContext()
        {
        }

        public NTTSHOPContext(DbContextOptions<NTTSHOPContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Managementusers> Managementusers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(ConnectionManager.getConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Managementusers>(entity =>
            {
                entity.HasKey(e => e.PkManuser);

                entity.ToTable("MANAGEMENTUSERS");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__MANAGEME__A9D105349A773088")
                    .IsUnique();

                entity.HasIndex(e => e.Login)
                    .HasName("UQ__MANAGEME__5E55825BD1573EE0")
                    .IsUnique();

                entity.Property(e => e.PkManuser).HasColumnName("PK_MANUSER");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Language)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Surname2)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.PkUser);

                entity.ToTable("USERS");

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__USERS__A9D10534A0BE630F")
                    .IsUnique();

                entity.HasIndex(e => e.Login)
                    .HasName("UQ__USERS__5E55825B2E24B545")
                    .IsUnique();

                entity.Property(e => e.PkUser).HasColumnName("PK_USER");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Language)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Province)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Surname2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Town)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
