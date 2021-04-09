using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BillsEntity.Models
{
    public partial class DTSAssignmentContext : DbContext
    {
        public DTSAssignmentContext()
        {
        }

        public DTSAssignmentContext(DbContextOptions<DTSAssignmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BILDTL> BILDTLs { get; set; }
        public virtual DbSet<BILHDR> BILHDRs { get; set; }
        public virtual DbSet<ITMDTL> ITMDTLs { get; set; }
        public virtual DbSet<VNDDTL> VNDDTLs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=DTSAssignment;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BILDTL>(entity =>
            {
                entity.HasKey(e => e.DTLCOD);

                entity.ToTable("BILDTL");

                entity.Property(e => e.DTLCOD).HasComment("Detail Code");

                entity.Property(e => e.BILCOD).HasComment("Bill Code");

                entity.Property(e => e.ITMCOD).HasComment("Item Code");

                entity.Property(e => e.ITMPRC)
                    .HasColumnType("decimal(10, 2)")
                    .HasComment("Item Price");

                entity.Property(e => e.ITMQTY).HasComment(" Item Quantity");

                entity.HasOne(d => d.BILCODNavigation)
                    .WithMany(p => p.BILDTLs)
                    .HasForeignKey(d => d.BILCOD)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILDTL_BILHDR");

                entity.HasOne(d => d.ITMCODNavigation)
                    .WithMany(p => p.BILDTLs)
                    .HasForeignKey(d => d.ITMCOD)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILDTL_ITMDTL");
            });

            modelBuilder.Entity<BILHDR>(entity =>
            {
                entity.HasKey(e => e.BILCOD);

                entity.ToTable("BILHDR");

                entity.Property(e => e.BILCOD).HasComment("Bill Code");

                entity.Property(e => e.BILDAT)
                    .HasColumnType("date")
                    .HasComment("Bill Date");

                entity.Property(e => e.BILIMG)
                    .HasMaxLength(250)
                    .HasComment("Bill Image Path");

                entity.Property(e => e.BILPRC)
                    .HasColumnType("decimal(10, 2)")
                    .HasComment("Bill Price");

                entity.Property(e => e.VNDCOD).HasComment("Vendor Code");

                entity.HasOne(d => d.VNDCODNavigation)
                    .WithMany(p => p.BILHDRs)
                    .HasForeignKey(d => d.VNDCOD)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BILHDR_VNDDTL");
            });

            modelBuilder.Entity<ITMDTL>(entity =>
            {
                entity.HasKey(e => e.ITMCOD);

                entity.ToTable("ITMDTL");

                entity.Property(e => e.ITMCOD).HasComment("Item Code");

                entity.Property(e => e.ITMNAM)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment(" Item Name");

                entity.Property(e => e.ITMPRC)
                    .HasColumnType("decimal(10, 2)")
                    .HasComment(" Item Price");
            });

            modelBuilder.Entity<VNDDTL>(entity =>
            {
                entity.HasKey(e => e.VNDCOD);

                entity.ToTable("VNDDTL");

                entity.Property(e => e.VNDCOD).HasComment(" Vendor Code");

                entity.Property(e => e.VNDNAM)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasComment("Vendor Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
