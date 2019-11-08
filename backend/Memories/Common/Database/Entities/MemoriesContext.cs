using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Boundless_Memories.Common.Database.Entities
{
    public partial class MemoriesContext : DbContext
    {
        public MemoriesContext()
        {
        }

        public MemoriesContext(DbContextOptions<MemoriesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ImageAssociations> ImageAssociations { get; set; }
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Memories;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ImageAssociations>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ImageId })
                    .HasName("ImageAssociations_PK");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ImageAssociations)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageAssociations_Images");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ImageAssociations)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageAssociations_Users");
            });

            modelBuilder.Entity<Images>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.Uploaded)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Pw)
                    .HasColumnName("PW")
                    .HasMaxLength(50);

                entity.Property(e => e.Salt).HasMaxLength(255);

                entity.Property(e => e.Username).HasMaxLength(50);
            });
        }
    }
}
