using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaCineMVC.Models;

public partial class BdCine2025Context : DbContext
{
    public BdCine2025Context()
    {
    }

    public BdCine2025Context(DbContextOptions<BdCine2025Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Asiento> Asientos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Entradum> Entrada { get; set; }

    public virtual DbSet<Funcion> Funcions { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1UF32KJ\\SQLEXPRESS;Database=BD_CINE2025;User Id=usr_xsnk;Password=pwd_blazsql;TrustServerCertificate=False;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asiento>(entity =>
        {
            entity.HasKey(e => e.IdAsiento).HasName("pk_asiento");

            entity.ToTable("Asiento");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdSala)
                .HasConstraintName("fk_asisal");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("pk_cliente");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Email, "uq_email").IsUnique();

            entity.HasIndex(e => e.Telefono, "uq_telefono").IsUnique();

            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("pk_detalle");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdEntradaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdEntrada)
                .HasConstraintName("fk_detentr");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("fk_detvent");
        });

        modelBuilder.Entity<Entradum>(entity =>
        {
            entity.HasKey(e => e.IdEntrada).HasName("pk_entrada");

            entity.HasIndex(e => e.IdEntrada, "uq_entrada_unica").IsUnique();

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdAsiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_entasie");

            entity.HasOne(d => d.IdFuncionNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdFuncion)
                .HasConstraintName("fk_entfunc");
        });

        modelBuilder.Entity<Funcion>(entity =>
        {
            entity.HasKey(e => e.IdFuncion).HasName("pk_funcion");

            entity.ToTable("Funcion");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Funcions)
                .HasForeignKey(d => d.IdPelicula)
                .HasConstraintName("fk_funcpel");

            entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Funcions)
                .HasForeignKey(d => d.IdSala)
                .HasConstraintName("fk_funsal");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("pk_pelicula");

            entity.ToTable("Pelicula");

            entity.HasIndex(e => e.Titulo, "uq_titulo").IsUnique();

            entity.Property(e => e.Clasificacion)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sinopsis)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("pk_sala");

            entity.ToTable("Sala");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("pk_venta");

            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("fk_vencli");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
