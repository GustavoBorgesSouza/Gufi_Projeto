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
        public virtual DbSet<ImagemUsuario> ImagemUsuarios { get; set; }
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
                optionsBuilder.UseSqlServer("Data Source=NOTE0113I5\\SQLEXPRESS; Initial Catalog=Gufi; user Id=sa; pwd=Senai@132");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.HasKey(e => e.IdEvento)
                    .HasName("PK__Evento__034EFC045ED46FD7");

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
                    .HasConstraintName("FK__Evento__IdInstit__47DBAE45");

                entity.HasOne(d => d.IdTipoEventoNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdTipoEvento)
                    .HasConstraintName("FK__Evento__IdTipoEv__46E78A0C");
            });

            modelBuilder.Entity<ImagemUsuario>(entity =>
            {
                entity.HasKey(e => e.IdImagemUsuario)
                    .HasName("PK__ImagemUs__6D677DEDE0A76613");

                entity.ToTable("ImagemUsuario");

                entity.HasIndex(e => e.IdUsuario, "UQ__ImagemUs__5B65BF966FDB01A2")
                    .IsUnique();

                entity.Property(e => e.Binario).IsRequired();

                entity.Property(e => e.DataInclusao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MimeType)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.NomeAquivo)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithOne(p => p.ImagemUsuario)
                    .HasForeignKey<ImagemUsuario>(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ImagemUsu__IdUsu__52593CB8");
            });

            modelBuilder.Entity<Inscricao>(entity =>
            {
                entity.HasKey(e => e.IdInscricao)
                    .HasName("PK__Inscrica__6209444BCC4CFA31");

                entity.ToTable("Inscricao");

                entity.Property(e => e.IdSituacao).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdEventoNavigation)
                    .WithMany(p => p.Inscricaos)
                    .HasForeignKey(d => d.IdEvento)
                    .HasConstraintName("FK__Inscricao__IdEve__4CA06362");

                entity.HasOne(d => d.IdSituacaoNavigation)
                    .WithMany(p => p.Inscricaos)
                    .HasForeignKey(d => d.IdSituacao)
                    .HasConstraintName("FK__Inscricao__IdSit__4D94879B");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Inscricaos)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Inscricao__IdUsu__4BAC3F29");
            });

            modelBuilder.Entity<Instituicao>(entity =>
            {
                entity.HasKey(e => e.IdInstituicao)
                    .HasName("PK__Institui__B771C0D84DE20798");

                entity.ToTable("Instituicao");

                entity.HasIndex(e => e.Endereco, "UQ__Institui__4DF3E1FFBD3416A8")
                    .IsUnique();

                entity.HasIndex(e => e.Cnpj, "UQ__Institui__AA57D6B44D2DE3E0")
                    .IsUnique();

                entity.HasIndex(e => e.NomeFantasia, "UQ__Institui__F5389F3170F09347")
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
                    .HasName("PK__Situacao__810BCE3AD9C4CCCF");

                entity.ToTable("Situacao");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoEvento>(entity =>
            {
                entity.HasKey(e => e.IdTipoEvento)
                    .HasName("PK__TipoEven__CDB3A3BE33832171");

                entity.ToTable("TipoEvento");

                entity.HasIndex(e => e.TituloTipoEvento, "UQ__TipoEven__40023AD2C613806A")
                    .IsUnique();

                entity.Property(e => e.TituloTipoEvento)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__TipoUsua__CA04062B0E4F0B7C");

                entity.ToTable("TipoUsuario");

                entity.HasIndex(e => e.TituloTipoUsuario, "UQ__TipoUsua__37BBE07EF2D2FEF9")
                    .IsUnique();

                entity.Property(e => e.TituloTipoUsuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF9707F1638D");

                entity.ToTable("Usuario");

                entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105348FAEF11E")
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
