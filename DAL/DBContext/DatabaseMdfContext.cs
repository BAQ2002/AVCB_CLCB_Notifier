using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using MODEL;

namespace DAL.DBContext;

public partial class DatabaseMdfContext : DbContext
{
    public DatabaseMdfContext()
    {
    }

    public DatabaseMdfContext(DbContextOptions<DatabaseMdfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<DocProcess> DocProcesses { get; set; }

    public virtual DbSet<Establishment> Establishments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\anton\\source\\repos\\AVBC_CLCB_Notifier\\DAL\\database\\Database.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PK__tmp_ms_x__737584F798043969");

            entity.ToTable("Client");

            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CellphoneNumber)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<DocProcess>(entity =>
        {
            entity.HasKey(e => e.ProcessNumber).HasName("PK__DocProce__0E179E9F6089B146");

            entity.ToTable("DocProcess");

            entity.Property(e => e.ProcessNumber)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.EstablishmentFantasyName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Extra).HasColumnType("text");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(4)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Establishment>(entity =>
        {
            entity.HasKey(e => e.Cnpj).HasName("PK__Establis__A299CC93D0164311");

            entity.ToTable("Establishment");

            entity.Property(e => e.Cnpj)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.ClientName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.FantasyName)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ProcessNumber)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.SocialReason)
                .HasMaxLength(60)
                .IsUnicode(false);

          //  entity.HasOne(d => d.ClientNameNavigation).WithMany(p => p.Establishments)
          //      .HasForeignKey(d => d.ClientName)
          //      .HasConstraintName("FK__Establish__Clien__06CD04F7");

          //  entity.HasOne(d => d.ProcessNumberNavigation).WithMany(p => p.Establishments)
          //      .HasForeignKey(d => d.ProcessNumber)
          //      .HasConstraintName("FK__Establish__Proce__05D8E0BE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
