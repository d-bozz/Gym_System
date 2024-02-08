using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaVentaBlazor.Server.Models;

public partial class GymSystemContext : DbContext
{
    public GymSystemContext()
    {
    }

    public GymSystemContext(DbContextOptions<GymSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencia { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<HorarioActividad> HorarioActividads { get; set; }

    public virtual DbSet<NumeroDocumento> NumeroDocumentos { get; set; }

    public virtual DbSet<PagoMensual> PagoMensuales { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Socio> Socios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK__Asistenc__4E1AB8945138024D");

            entity.Property(e => e.IdAsistencia).HasColumnName("idAsistencia");
            entity.Property(e => e.FechaAsistencia)
                .HasColumnType("datetime")
                .HasColumnName("fechaAsistencia");
            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.IdSocio).HasColumnName("idSocio");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdHorario)
                .HasConstraintName("FK__Asistenci__idHor__5535A963");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.Asistencia)
                .HasForeignKey(d => d.IdSocio)
                .HasConstraintName("FK__Asistenci__idSoc__5441852A");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240C43C29861");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F7A4E5913");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__DetalleVe__idPro__4AB81AF0");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__idVen__49C3F6B7");
        });

        modelBuilder.Entity<HorarioActividad>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__HorarioA__DE60F33A35B605FB");

            entity.ToTable("HorarioActividad");

            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.Actividad)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("actividad");
            entity.Property(e => e.Encargado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("encargado");
            entity.Property(e => e.DiaSemana)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("diaSemana");
            entity.Property(e => e.HoraFin).HasColumnName("horaFin");
            entity.Property(e => e.HoraInicio).HasColumnName("horaInicio");
        });

        modelBuilder.Entity<NumeroDocumento>(entity =>
        {
            entity.HasKey(e => e.IdNumeroDocumento).HasName("PK__NumeroDo__471E421AC67BCFA0");

            entity.ToTable("NumeroDocumento");

            entity.Property(e => e.IdNumeroDocumento).HasColumnName("idNumeroDocumento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.UltimoNumero).HasColumnName("ultimo_Numero");
        });

        modelBuilder.Entity<PagoMensual>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__PagoMens__BD2295AD37C93F54");

            entity.ToTable("PagoMensual");

            entity.Property(e => e.IdPago).HasColumnName("idPago");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("fechaPago");
            entity.Property(e => e.IdSocio).HasColumnName("idSocio");
            entity.Property(e => e.Mes).HasColumnName("mes");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("monto");

            entity.HasOne(d => d.IdSocioNavigation).WithMany(p => p.PagoMensuales)
                .HasForeignKey(d => d.IdSocio)
                .HasConstraintName("FK__PagoMensu__idSoc__4F7CD00D");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A13240D73834");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Foto)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Marca)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Peso)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("peso");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__idCate__403A8C7D");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__3C872F76DE98E9B4");

            entity.ToTable("Rol");

            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
        });

        modelBuilder.Entity<Socio>(entity =>
        {
            entity.HasKey(e => e.IdSocio).HasName("PK__Socio__E19769C12AE5F727");
            entity.HasKey(e => e.IdSocio).HasName("UQ__Socio__415B7BE58E58EF10");

            entity.ToTable("Socio");

            entity.Property(e => e.IdSocio).HasColumnName("idSocio");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Cedula)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.FechaInicioMembresia)
                .HasColumnType("datetime")
                .HasColumnName("fechaInicioMembresia");
            entity.Property(e => e.Mutualista)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mutualista");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Restricciones)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("restricciones");
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefonoContacto");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {

            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A615FCC783");
            entity.HasKey(e => e.IdUsuario).HasName("UQ__Usuario__415B7BE5F3F54B12");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Cedula)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.Horario)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("horario");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.NombreApellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreApellidos");
            entity.Property(e => e.Foto)
                .IsUnicode(false)
                .HasColumnName("foto");
            entity.Property(e => e.Telefono)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__idRol__3A81B327");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D56146481535B");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("numeroDocumento");
            entity.Property(e => e.TipoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoPago");
            entity.Property(e => e.Cliente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliente");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
