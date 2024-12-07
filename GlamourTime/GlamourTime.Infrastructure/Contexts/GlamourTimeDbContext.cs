using System;
using System.Collections.Generic;
using GlamourTime.Infrastructure;
using GlamourTime.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlamourTime.Infrastructure.Contexts;

public partial class GlamourTimeDbContext : DbContext
{
    public GlamourTimeDbContext()
    {
    }

    public GlamourTimeDbContext(DbContextOptions<GlamourTimeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<SalonService> SalonServices { get; set; }

    public virtual DbSet<Stylist> Stylists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-SVR6Q0R;Database=GlamourTimeDB;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA2C3BBB493");

            entity.HasOne(d => d.Client).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Clien__3D5E1FD2");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Servi__3F466844");

            entity.HasOne(d => d.Stylist).WithMany(p => p.Appointments).HasConstraintName("FK__Appointme__Styli__3E52440B");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A04961D3412");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58D00EEE16");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Payments).HasConstraintName("FK__Payments__Appoin__4222D4EF");
        });

        modelBuilder.Entity<SalonService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__SalonSer__C51BB0EAE45D806B");
        });

        modelBuilder.Entity<Stylist>(entity =>
        {
            entity.HasKey(e => e.StylistId).HasName("PK__Stylists__A822EAC1E49111F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
