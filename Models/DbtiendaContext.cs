using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APITIENDA.Models;

public partial class DbtiendaContext : DbContext
{
    public DbtiendaContext()
    {
    }

    public DbtiendaContext(DbContextOptions<DbtiendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Almacen> Almacens { get; set; }

    public virtual DbSet<Carrito> Carritos { get; set; }

    public virtual DbSet<CarritoDetalle> CarritoDetalles { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidoDetalle> PedidoDetalles { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoImagen> ProductoImagens { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Subcategorium> Subcategoria { get; set; }

    public virtual DbSet<Tipoentradum> Tipoentrada { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DANIELROJAS; Database=DBTIENDA; Trusted_Connection=True;  Encrypt=False ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Almacen>(entity =>
        {
            entity.HasKey(e => e.IdAlmacen).HasName("PK_Almacen");

            entity.ToTable("ALMACEN");

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Almacens)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_Almacen_Producto");

            entity.HasOne(d => d.IdTipoEntradaNavigation).WithMany(p => p.Almacens)
                .HasForeignKey(d => d.IdTipoEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_Almacen_TipoEntrada");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK_Carrito");

            entity.ToTable("CARRITO");

            entity.Property(e => e.FechaPedido).HasColumnType("datetime");
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Carritos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carrito_Usuario");
        });

        modelBuilder.Entity<CarritoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdCarritoDetalle).HasName("PK_CarritoDetalle");

            entity.ToTable("CARRITO_DETALLE");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.CarritoDetalles)
                .HasForeignKey(d => d.IdCarrito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarritoDetalle_Carrito");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.CarritoDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CarritoDetalle_Producto");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK_IdCategoria");

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.Descripcion).HasMaxLength(80);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK_Pedido");

            entity.ToTable("PEDIDO");

            entity.Property(e => e.FechaPedido).HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pedido_Usuario");
        });

        modelBuilder.Entity<PedidoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdPedidoDetalle).HasName("PK_PedidoDetalle");

            entity.ToTable("PEDIDO_DETALLE");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidoDetalle_Carrito");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.PedidoDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PedidoDetalle_Producto");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK_IdUsuario");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.Descripcion).HasMaxLength(280);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(80);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdCategoria_Producto");
        });

        modelBuilder.Entity<ProductoImagen>(entity =>
        {
            entity.HasKey(e => e.IdProductoImagen).HasName("PK_IdProductoImagen");

            entity.ToTable("PRODUCTO_IMAGEN");

            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoImagens)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IdProductoImagen_Producto");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK_Rol");

            entity.ToTable("ROL");

            entity.Property(e => e.Descripcion).HasMaxLength(20);
        });

        modelBuilder.Entity<Subcategorium>(entity =>
        {
            entity.HasKey(e => e.IdSubCategoria).HasName("PK_IdSubCategoria");

            entity.ToTable("SUBCATEGORIA");

            entity.Property(e => e.Descripcion).HasMaxLength(80);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Subcategoria)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategoria_Categoria");
        });

        modelBuilder.Entity<Tipoentradum>(entity =>
        {
            entity.HasKey(e => e.IdTipoEntrada).HasName("PK_IdTipoEntrada");

            entity.ToTable("TIPOENTRADA");

            entity.Property(e => e.Descripcion).HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_Usuario");

            entity.ToTable("USUARIO");

            entity.Property(e => e.AMaterno)
                .HasMaxLength(80)
                .HasColumnName("A_Materno");
            entity.Property(e => e.APaterno)
                .HasMaxLength(80)
                .HasColumnName("A_Paterno");
            entity.Property(e => e.Contrasena).HasMaxLength(80);
            entity.Property(e => e.Correo).HasMaxLength(150);
            entity.Property(e => e.FechaRegistro).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(80);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
