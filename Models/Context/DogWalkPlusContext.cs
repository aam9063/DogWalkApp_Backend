using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Models.Context;

public partial class DogWalkPlusContext : DbContext
{
    public DogWalkPlusContext()
    {
    }

    public DogWalkPlusContext(DbContextOptions<DogWalkPlusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Foto> Fotos { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Opinione> Opiniones { get; set; }

    public virtual DbSet<Paseador> Paseadors { get; set; }

    public virtual DbSet<Perro> Perros { get; set; }

    public virtual DbSet<Precio> Precios { get; set; }

    public virtual DbSet<Ranking> Rankings { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Albert\\SQLEXPRESS;Database=DOG_WALK_PLUS;Trust Server Certificate=true;User Id=;Password=;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Foto>(entity =>
        {
            entity.HasKey(e => e.IdFoto).HasName("PK__Fotos__620EA3A53500BEB4");

            entity.Property(e => e.IdFoto).HasColumnName("id_foto");
            entity.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdPerro).HasColumnName("id_perro");
            entity.Property(e => e.UrlFoto)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("url_foto");

            entity.HasOne(d => d.IdPerroNavigation).WithMany(p => p.Fotos)
                .HasForeignKey(d => d.IdPerro)
                .HasConstraintName("FK__Fotos__id_perro__6A30C649");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__Horario__C5836D6939308B65");

            entity.ToTable("Horario");

            entity.Property(e => e.IdHorario).HasColumnName("id_horario");
            entity.Property(e => e.Disponibilidad)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("disponibilidad");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
        });

        modelBuilder.Entity<Opinione>(entity =>
        {
            entity.HasKey(e => e.IdOpinion).HasName("PK__Opinione__04DDBD78B95BF317");

            entity.Property(e => e.IdOpinion).HasColumnName("id_opinion");
            entity.Property(e => e.Comentario)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comentario");
            entity.Property(e => e.IdPaseador).HasColumnName("id_paseador");
            entity.Property(e => e.IdPerro).HasColumnName("id_perro");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");

            entity.HasOne(d => d.IdPaseadorNavigation).WithMany(p => p.Opiniones)
                .HasForeignKey(d => d.IdPaseador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Opiniones__id_pa__6EF57B66");

            entity.HasOne(d => d.IdPerroNavigation).WithMany(p => p.Opiniones)
                .HasForeignKey(d => d.IdPerro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Opiniones__id_pe__6E01572D");
        });

        modelBuilder.Entity<Paseador>(entity =>
        {
            entity.HasKey(e => e.IdPaseador).HasName("PK__Paseador__C0836292BE2D876F");

            entity.ToTable("Paseador");

            entity.HasIndex(e => e.Email, "UQ__Paseador__AB6E6164FEA0799A").IsUnique();

            entity.Property(e => e.IdPaseador).HasColumnName("id_paseador");
            entity.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dirección");
            entity.Property(e => e.DniPaseador)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.TelefonoPaseador)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Perro>(entity =>
        {
            entity.HasKey(e => e.IdPerro).HasName("PK__Perro__93FAA747011F9CB2");

            entity.ToTable("Perro");

            entity.Property(e => e.IdPerro).HasColumnName("id_perro");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Instagram)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("instagram");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Raza)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("raza");
            entity.Property(e => e.Tiktok)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tiktok");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Perros)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Perro__id_usuari__59063A47");
        });

        modelBuilder.Entity<Precio>(entity =>
        {
            entity.HasKey(e => new { e.IdPaseador, e.IdServicio }).HasName("PK__Precios__167E656FF392DA88");

            entity.Property(e => e.IdPaseador).HasColumnName("id_paseador");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Precio1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdPaseadorNavigation).WithMany(p => p.Precios)
                .HasForeignKey(d => d.IdPaseador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Precios__id_pase__5535A963");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Precios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Precios__id_serv__5629CD9C");
        });

        modelBuilder.Entity<Ranking>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdPaseador }).HasName("PK__Ranking__72363284540DF759");

            entity.ToTable("Ranking");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IdPaseador).HasColumnName("id_paseador");
            entity.Property(e => e.Comentario)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comentario");
            entity.Property(e => e.Valoracion).HasColumnName("valoracion");

            entity.HasOne(d => d.IdPaseadorNavigation).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.IdPaseador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ranking__id_pase__6754599E");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Rankings)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ranking__id_usua__66603565");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__423CBE5D68BD8FFB");

            entity.Property(e => e.IdReserva).HasColumnName("id_reserva");
            entity.Property(e => e.EstadoReserva)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado_reserva");
            entity.Property(e => e.FechaReserva)
                .HasColumnType("datetime")
                .HasColumnName("fecha_reserva");
            entity.Property(e => e.IdHorario).HasColumnName("id_horario");
            entity.Property(e => e.IdPaseador).HasColumnName("id_paseador");
            entity.Property(e => e.IdPerro).HasColumnName("id_perro");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_hor__628FA481");

            entity.HasOne(d => d.IdPaseadorNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPaseador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_pas__5FB337D6");

            entity.HasOne(d => d.IdPerroNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdPerro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_per__619B8048");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_ser__60A75C0F");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__id_usu__5EBF139D");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__6FD07FDC242612FA");

            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.DescripcionServicio)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion_servicio");
            entity.Property(e => e.NombreServicio)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_servicio");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__4E3E04ADF1FABD7D");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Email, "UQ__Usuario__AB6E61643F74DBF3").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dirección");
            entity.Property(e => e.DniUsuario)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.TelefonoUsuario)
                .IsRequired()
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
