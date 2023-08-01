using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DAL.Models;

namespace DAL.DbContexts
{
    public partial class DefaultContext : DbContext
    {
        public DefaultContext()
        {
        }

        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agreement> Agreements { get; set; } = null!;
        public virtual DbSet<Bank> Banks { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseLazyLoadingProxies().UseMySql("server=localhost;database=pdc;uid=root;pwd=P@ssw0rd", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Agreement>(entity =>
            {
                entity.ToTable("agreements");

                entity.HasIndex(e => e.CompanyId, "company_agreements_fk_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Branch)
                    .HasMaxLength(45)
                    .HasColumnName("branch");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.Floor)
                    .HasMaxLength(45)
                    .HasColumnName("floor");

                entity.Property(e => e.OfficeNumber)
                    .HasMaxLength(45)
                    .HasColumnName("office_number");

                entity.Property(e => e.Section)
                    .HasMaxLength(45)
                    .HasColumnName("section");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.WorkStation)
                    .HasMaxLength(45)
                    .HasColumnName("work_station");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Agreements)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("company_agreements_fk");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("banks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Branch)
                    .HasMaxLength(45)
                    .HasColumnName("branch");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email");

                entity.Property(e => e.Fax)
                    .HasMaxLength(45)
                    .HasColumnName("fax");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(45)
                    .HasColumnName("phone");

                entity.Property(e => e.PoBox)
                    .HasMaxLength(45)
                    .HasColumnName("po_box");

                entity.Property(e => e.Website)
                    .HasMaxLength(45)
                    .HasColumnName("website");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.HasIndex(e => e.Id, "id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(25)
                    .HasColumnName("phone");

                entity.Property(e => e.Website)
                    .HasMaxLength(100)
                    .HasColumnName("website");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");

                entity.HasIndex(e => e.ReceiverBankId, "collection_bank_fk_idx");

                entity.HasIndex(e => e.AgreementId, "payment_agreement_fk_idx");

                entity.HasIndex(e => e.SenderBankId, "payment_cheque_bank_fk_idx");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgreementId).HasColumnName("agreement_id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.ChequeDueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("cheque_due_date");

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(45)
                    .HasColumnName("cheque_no");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.PaymentClearanceDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_clearance_date");

                entity.Property(e => e.PaymentDueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("payment_due_date");

                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");

                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");

                entity.Property(e => e.ReceiverBankId).HasColumnName("receiver_bank_id");

                entity.Property(e => e.SenderBankId).HasColumnName("sender_bank_id");

                entity.HasOne(d => d.Agreement)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AgreementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("payment_agreement_fk");

                entity.HasOne(d => d.ReceiverBank)
                    .WithMany(p => p.PaymentReceiverBanks)
                    .HasForeignKey(d => d.ReceiverBankId)
                    .HasConstraintName("collection_bank_fk");

                entity.HasOne(d => d.SenderBank)
                    .WithMany(p => p.PaymentSenderBanks)
                    .HasForeignKey(d => d.SenderBankId)
                    .HasConstraintName("payment_cheque_bank_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
