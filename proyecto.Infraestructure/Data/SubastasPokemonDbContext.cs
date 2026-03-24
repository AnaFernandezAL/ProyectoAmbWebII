using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using proyecto.Infraestructure.Models;

namespace proyecto.Infraestructure.Data;

public partial class SubastasPokemonDbContext : DbContext
{
    public SubastasPokemonDbContext(DbContextOptions<SubastasPokemonDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cartas> Cartas { get; set; }
    public virtual DbSet<CartaCategoria> CartaCategoria { get; set; }

    public virtual DbSet<Categorias> Categorias { get; set; }

    public virtual DbSet<EstadosCarta> EstadosCarta { get; set; }

    public virtual DbSet<EstadosPago> EstadosPago { get; set; }

    public virtual DbSet<EstadosSubasta> EstadosSubasta { get; set; }

    public virtual DbSet<EstadosUsuario> EstadosUsuario { get; set; }

    public virtual DbSet<Ganadores> Ganadores { get; set; }

    public virtual DbSet<ImagenesCarta> ImagenesCarta { get; set; }

    public virtual DbSet<Pagos> Pagos { get; set; }

    public virtual DbSet<Pujas> Pujas { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Subastas> Subastas { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cartas>(entity =>
        {
            entity.HasKey(e => e.CartaId).HasName("PK__Cartas__D703278B8E4D5E93");

            entity.Property(e => e.CartaId).HasColumnName("CartaID");
            entity.Property(e => e.Condicion)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion).IsUnicode(false);
            entity.Property(e => e.Edicion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoCartaId).HasColumnName("EstadoCartaID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreCarta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rareza)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VendedorId).HasColumnName("VendedorID");

            entity.HasOne(d => d.EstadoCarta).WithMany(p => p.Cartas)
                .HasForeignKey(d => d.EstadoCartaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cartas__EstadoCa__4F7CD00D");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.Cartas)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cartas__Vendedor__5070F446");

            entity.HasMany(d => d.Categoria)
                .WithMany(p => p.Carta)
                .UsingEntity<CartaCategoria>(
                    j => j.HasOne(cc => cc.Categoria)
                          .WithMany(c => c.CartaCategoria)
                          .HasForeignKey(cc => cc.CategoriaId)
                          .OnDelete(DeleteBehavior.ClientSetNull)
                          .HasConstraintName("FK__CartaCate__Categ__5812160E"),
                    j => j.HasOne(cc => cc.Carta)
                          .WithMany(c => c.CartaCategoria)
                          .HasForeignKey(cc => cc.CartaId)
                          .OnDelete(DeleteBehavior.ClientSetNull)
                          .HasConstraintName("FK__CartaCate__Carta__571DF1D5"),
                    j =>
                    {
                        j.HasKey(cc => new { cc.CartaId, cc.CategoriaId })
                         .HasName("PK__CartaCat__98361B9758A7492F");

                        j.ToTable("CartaCategoria");

                        j.Property(cc => cc.CartaId).HasColumnName("CartaID");
                        j.Property(cc => cc.CategoriaId).HasColumnName("CategoriaID");
                    });
        });


        modelBuilder.Entity<Categorias>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1C522F12EB0");

            entity.HasIndex(e => e.NombreCategoria, "UQ__Categori__A21FBE9F9B5A49C1").IsUnique();

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadosCarta>(entity =>
        {
            entity.HasKey(e => e.EstadoCartaId).HasName("PK__EstadosC__A6090D857E21E6CC");

            entity.HasIndex(e => e.NombreEstado, "UQ__EstadosC__6CE506150C8820AF").IsUnique();

            entity.Property(e => e.EstadoCartaId).HasColumnName("EstadoCartaID");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadosPago>(entity =>
        {
            entity.HasKey(e => e.EstadoPagoId).HasName("PK__EstadosP__63AD30BD49A37E20");

            entity.HasIndex(e => e.NombreEstado, "UQ__EstadosP__6CE50615FF634EC0").IsUnique();

            entity.Property(e => e.EstadoPagoId).HasColumnName("EstadoPagoID");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadosSubasta>(entity =>
        {
            entity.HasKey(e => e.EstadoSubastaId).HasName("PK__EstadosS__6F18333C287CB7BD");

            entity.HasIndex(e => e.NombreEstado, "UQ__EstadosS__6CE506153125151D").IsUnique();

            entity.Property(e => e.EstadoSubastaId).HasColumnName("EstadoSubastaID");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadosUsuario>(entity =>
        {
            entity.HasKey(e => e.EstadoUsuarioId).HasName("PK__EstadosU__BAA0F88264717428");

            entity.HasIndex(e => e.NombreEstado, "UQ__EstadosU__6CE50615F7BF8941").IsUnique();

            entity.Property(e => e.EstadoUsuarioId).HasColumnName("EstadoUsuarioID");
            entity.Property(e => e.NombreEstado)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ganadores>(entity =>
        {
            entity.HasKey(e => e.GanadorId).HasName("PK__Ganadore__D0C022780D07924E");

            entity.HasIndex(e => e.SubastaId, "UQ__Ganadore__46C5CE7BEA19172A").IsUnique();

            entity.Property(e => e.GanadorId).HasColumnName("GanadorID");
            entity.Property(e => e.FechaCierre).HasColumnType("datetime");
            entity.Property(e => e.MontoFinal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubastaId).HasColumnName("SubastaID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Subasta).WithOne(p => p.Ganadores)
                .HasForeignKey<Ganadores>(d => d.SubastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ganadores__Subas__693CA210");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Ganadores)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ganadores__Usuar__6A30C649");
        });

        modelBuilder.Entity<ImagenesCarta>(entity =>
        {
            entity.HasKey(e => e.ImagenId).HasName("PK__Imagenes__0C7D20D721AD54C5");

            entity.Property(e => e.ImagenId).HasColumnName("ImagenID");
            entity.Property(e => e.CartaId).HasColumnName("CartaID");
            entity.Property(e => e.Urlimagen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("URLImagen");

            entity.HasOne(d => d.Carta).WithMany(p => p.ImagenesCarta)
                .HasForeignKey(d => d.CartaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenesC__Carta__5441852A");
        });

        modelBuilder.Entity<Pagos>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__F00B615847623D1A");

            entity.HasIndex(e => e.SubastaId, "UQ__Pagos__46C5CE7B80C91F36").IsUnique();

            entity.Property(e => e.PagoId).HasColumnName("PagoID");
            entity.Property(e => e.CompradorId).HasColumnName("CompradorID");
            entity.Property(e => e.EstadoPagoId).HasColumnName("EstadoPagoID");
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.SubastaId).HasColumnName("SubastaID");

            entity.HasOne(d => d.Comprador).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.CompradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__Comprador__6EF57B66");

            entity.HasOne(d => d.EstadoPago).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.EstadoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__EstadoPag__6FE99F9F");

            entity.HasOne(d => d.Subasta).WithOne(p => p.Pagos)
                .HasForeignKey<Pagos>(d => d.SubastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pagos__SubastaID__6E01572D");
        });

        modelBuilder.Entity<Pujas>(entity =>
        {
            entity.HasKey(e => e.PujaId).HasName("PK__Pujas__0F67A0FCE665D94D");

            entity.Property(e => e.PujaId).HasColumnName("PujaID");
            entity.Property(e => e.CompradorId).HasColumnName("CompradorID");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoOfertado).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubastaId).HasColumnName("SubastaID");

            entity.HasOne(d => d.Comprador).WithMany(p => p.Pujas)
                .HasForeignKey(d => d.CompradorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pujas__Comprador__6477ECF3");

            entity.HasOne(d => d.Subasta).WithMany(p => p.Pujas)
                .HasForeignKey(d => d.SubastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pujas__SubastaID__6383C8BA");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302D1C8744D6C");

            entity.HasIndex(e => e.NombreRol, "UQ__Roles__4F0B537F1FF02757").IsUnique();

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subastas>(entity =>
        {
            entity.HasKey(e => e.SubastaId).HasName("PK__Subastas__46C5CE7A0616B953");

            entity.Property(e => e.SubastaId).HasColumnName("SubastaID");
            entity.Property(e => e.CartaId).HasColumnName("CartaID");
            entity.Property(e => e.EstadoSubastaId).HasColumnName("EstadoSubastaID");
            entity.Property(e => e.FechaCierre).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.IncrementoMinimo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MontoActual).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioBase).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VendedorId).HasColumnName("VendedorID");

            entity.HasOne(d => d.Carta).WithMany(p => p.Subastas)
                .HasForeignKey(d => d.CartaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subastas__CartaI__5AEE82B9");

            entity.HasOne(d => d.EstadoSubasta).WithMany(p => p.Subastas)
                .HasForeignKey(d => d.EstadoSubastaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subastas__Estado__5CD6CB2B");

            entity.HasOne(d => d.Vendedor).WithMany(p => p.Subastas)
                .HasForeignKey(d => d.VendedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subastas__Vended__5BE2A6F2");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798B7933A37");

            entity.HasIndex(e => e.CorreoElectronico, "UQ__Usuarios__531402F3795A4F6E").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EstadoUsuarioId).HasColumnName("EstadoUsuarioID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Notificado)
                .HasDefaultValue(false);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TokenRecuperacion)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.EstadoUsuario).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EstadoUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__Estado__49C3F6B7");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuarios__RolID__4AB81AF0");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
