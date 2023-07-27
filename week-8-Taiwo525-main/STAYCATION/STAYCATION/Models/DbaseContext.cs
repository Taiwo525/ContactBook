using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace STAYCATION.Models;

public partial class DbaseContext : DbContext
{
    public DbaseContext()
    {
    }

    public DbaseContext(DbContextOptions<DbaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<PictureDb> PictureDbs { get; set; }

    public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-3IITL2F;database=DBase;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Command Timeout=0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PictureDb>(entity =>
        {
            entity.HasKey(e => e.HotelImageUrl).HasName("PK__PictureD__876DD7015BC2511C");

            entity.ToTable("PictureDb");

            entity.Property(e => e.HotelImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("hotelImageUrl");
            entity.Property(e => e.HotelDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("hotelDescription");
            entity.Property(e => e.HotelGroup)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("hotelGroup");
            entity.Property(e => e.HotelLocation)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("hotelLocation");
            entity.Property(e => e.HotelName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hotelName");
            entity.Property(e => e.HotelPopularity)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hotelPopularity");
            entity.Property(e => e.HotelPrice)
                .HasMaxLength(50)
                .HasColumnName("hotelPrice");
            entity.Property(e => e.IsPopular)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RegisteredUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Register__3214EC075744D63B");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PassWord)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
