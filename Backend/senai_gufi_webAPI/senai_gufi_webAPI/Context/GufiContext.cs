using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using senai_gufi_webAPI.Domains;

#nullable disable

namespace senai_gufi_webAPI.Context
{
    public partial class GufiContext : DbContext
    {
        public GufiContext()
        {
        }

        public GufiContext(DbContextOptions<GufiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Inscricao> Inscricaos { get; set; }
        public virtual DbSet<Instituicao> Instituicaos { get; set; }
        public virtual DbSet<Situacao> Situacaos { get; set; }
        public virtual DbSet<TipoEvento> TipoEventos { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=NOTE0113I2\\SQLEXPRESS; Initial Catalog=Gufi; user id=sa; pwd=Senai@132;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento)
                    .HasName("PK__EVENTO__034EFC046A46DA99");

                entity.ToTable("Evento");

                entity.Property(e => e.AcessoLivre).HasDefaultValueSql("((1))");

                entity.Property(e => e.DataEvento).HasColumnType("datetime");

                entity.Property(e => e.DescricaoEvento)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.NomeEvento)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdInstituicaoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdInstituicao)
                    .HasConstraintName("FK__EVENTO__IdInstit__47DBAE45");

                entity.HasOne(d => d.IdTipoEventoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdTipoEvento)
                    .HasConstraintName("FK__EVENTO__IdTipoEv__46E78A0C");
            });

            modelBuilder.Entity<Inscricao>(entity =>
            {
                entity.HasKey(e => e.IdInscricao)
                    .HasName("PK__Inscrica__6209444B3B81F274");

                entity.ToTable("Inscricao");

                entity.Property(e => e.IdSituacao).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Inscricaos)
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK__Inscricao__IdEve__571DF1D5");

                entity.HasOne(d => d.IdSituacaoNavigation)
                    .WithMany(p => p.Inscricaos)
                    .HasForeignKey(d => d.IdSituacao)
                    .HasConstraintName("FK__Inscricao__IdSit__5812160E");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Inscricaos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Inscricao__IdUsu__5629CD9C");
            });

            modelBuilder.Entity<Instituicao>(entity =>
            {
                entity.HasKey(e => e.IdInstituicao)
                    .HasName("PK__Institui__B771C0D8352312F8");

                entity.ToTable("Instituicao");

                entity.HasIndex(e => e.Endereco, "UQ__Institui__4DF3E1FF2B5645E8")
                    .IsUnique();

                entity.HasIndex(e => e.Cnpj, "UQ__Institui__AA57D6B4A5460E5E")
                    .IsUnique();

                entity.HasIndex(e => e.NomeFantasia, "UQ__Institui__F5389F316C83FEF6")
                    .IsUnique();

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CNPJ")
                    .IsFixedLength(true);

                entity.Property(e => e.Endereco)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NomeFantasia)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Situacao>(entity =>
            {
                entity.HasKey(e => e.IdSituacao)
                    .HasName("PK__Situacao__810BCE3AE716B130");

                entity.ToTable("Situacao");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoEvento>(entity =>
            {
                entity.HasKey(e => e.IdTipoEvento)
                    .HasName("PK__TipoEven__CDB3A3BE7105C572");

                entity.ToTable("TipoEvento");

                entity.HasIndex(e => e.TituloTipoEvento, "UQ__TipoEven__40023AD28647A7F8")
                    .IsUnique();

                entity.Property(e => e.TituloTipoEvento)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TipoUsua__CA04062BA057809D");

                entity.ToTable("TipoUsuario");

                entity.HasIndex(e => e.TituloTipoUsuario, "UQ__TipoUsua__37BBE07E138042A9")
                    .IsUnique();

                entity.Property(e => e.TituloTipoUsuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF9793D32E2C");

                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Email, "UQ__Usuario__A9D10534D9896318")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NomeUsuario)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Usuario__IdTipoU__440B1D61");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
