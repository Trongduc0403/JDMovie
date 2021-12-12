using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JDMovie.Models
{
    public partial class dbDACNContext : DbContext
    {
        public dbDACNContext()
        {
        }

        public dbDACNContext(DbContextOptions<dbDACNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banner> Banners { get; set; } = null!;
        public virtual DbSet<CttapPhim> CttapPhims { get; set; } = null!;
        public virtual DbSet<DsphimBo> DsphimBos { get; set; } = null!;
        public virtual DbSet<DsphimLe> DsphimLes { get; set; } = null!;
        public virtual DbSet<Gioithieu> Gioithieus { get; set; } = null!;
        public virtual DbSet<HopPhim> HopPhims { get; set; } = null!;
        public virtual DbSet<LichSu> LichSus { get; set; } = null!;
        public virtual DbSet<Nam> Nams { get; set; } = null!;
        public virtual DbSet<QuocGium> QuocGia { get; set; } = null!;
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; } = null!;
        public virtual DbSet<TheLoai> TheLoais { get; set; } = null!;
        public virtual DbSet<TheLoaiPhimBo> TheLoaiPhimBos { get; set; } = null!;
        public virtual DbSet<TheLoaiPhimLe> TheLoaiPhimLes { get; set; } = null!;
        public virtual DbSet<Tintucphim> Tintucphims { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("server = .\\SQLEXPRESS;Database = dbDACN;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("Banner");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Idphim).HasColumnName("IDPhim");
            });

            modelBuilder.Entity<CttapPhim>(entity =>
            {
                entity.HasKey(e => e.Idphim);

                entity.ToTable("CTTapPhim");

                entity.Property(e => e.Idphim).HasColumnName("IDPhim");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.CttapPhims)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK_CTTapPhim_DSPhimBo");
            });

            modelBuilder.Entity<DsphimBo>(entity =>
            {
                entity.ToTable("DSPhimBo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaQg).HasColumnName("MaQG");

                entity.Property(e => e.ThoiLuong).HasMaxLength(15);

                entity.HasOne(d => d.MaQgNavigation)
                    .WithMany(p => p.DsphimBos)
                    .HasForeignKey(d => d.MaQg)
                    .HasConstraintName("FK_DSPhimBo_QuocGia");

                entity.HasOne(d => d.NamPhatHanhNavigation)
                    .WithMany(p => p.DsphimBos)
                    .HasForeignKey(d => d.NamPhatHanh)
                    .HasConstraintName("FK_DSPhimBo_Nam");
            });

            modelBuilder.Entity<DsphimLe>(entity =>
            {
                entity.ToTable("DSPhimLe");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.MaQg).HasColumnName("MaQG");

                entity.Property(e => e.ThoiLuong).HasMaxLength(15);

                entity.HasOne(d => d.MaQgNavigation)
                    .WithMany(p => p.DsphimLes)
                    .HasForeignKey(d => d.MaQg)
                    .HasConstraintName("FK_DSPhimLe_QuocGia");

                entity.HasOne(d => d.NamPhatHanhNavigation)
                    .WithMany(p => p.DsphimLes)
                    .HasForeignKey(d => d.NamPhatHanh)
                    .HasConstraintName("FK_DSPhimLe_Nam");
            });

            modelBuilder.Entity<Gioithieu>(entity =>
            {
                entity.HasKey(e => e.Idgioitin);

                entity.ToTable("gioithieu");

                entity.Property(e => e.Idgioitin).HasColumnName("idgioitin");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Sdtlien).HasColumnName("sdtlien");
            });

            modelBuilder.Entity<HopPhim>(entity =>
            {
                entity.HasKey(e => new { e.Idtk, e.Idphim })
                    .HasName("PK_HopPhim1");

                entity.ToTable("HopPhim");

                entity.Property(e => e.Idphim).HasColumnName("IDPhim");

                entity.Property(e => e.K)
                    .HasMaxLength(10)
                    .HasColumnName("k")
                    .IsFixedLength();

                entity.HasOne(d => d.IdphimNavigation)
                    .WithMany(p => p.HopPhims)
                    .HasForeignKey(d => d.Idphim)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HopPhim_DSPhimBo");

                entity.HasOne(d => d.IdtkNavigation)
                    .WithMany(p => p.HopPhims)
                    .HasForeignKey(d => d.Idtk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HopPhim_TaiKhoan");
            });

            modelBuilder.Entity<LichSu>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("LichSu");

                entity.Property(e => e.Idphim).HasColumnName("IDPhim");

                entity.HasOne(d => d.IdphimNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idphim)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LichSu__IDPhim__412EB0B6");

                entity.HasOne(d => d.IdtkNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Idtk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LichSu__Idtk__4222D4EF");
            });

            modelBuilder.Entity<Nam>(entity =>
            {
                entity.HasKey(e => e.MaNam);

                entity.ToTable("Nam");
            });

            modelBuilder.Entity<QuocGium>(entity =>
            {
                entity.HasKey(e => e.MaQg)
                    .HasName("PK_Quốc Gia");

                entity.Property(e => e.MaQg).HasColumnName("MaQG");

                entity.Property(e => e.TenQg)
                    .HasMaxLength(50)
                    .HasColumnName("TenQG");
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.HasKey(e => e.Idtk);

                entity.ToTable("TaiKhoan");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.HoTen).HasMaxLength(60);

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TheLoai>(entity =>
            {
                entity.HasKey(e => e.IdtheLoai);

                entity.ToTable("TheLoai");

                entity.Property(e => e.IdtheLoai).HasColumnName("IDTheLoai");

                entity.Property(e => e.TenTheLoai).HasMaxLength(50);
            });

            modelBuilder.Entity<TheLoaiPhimBo>(entity =>
            {
                entity.HasKey(e => new { e.IdphimBo, e.IdtheLoai });

                entity.ToTable("TheLoaiPhimBo");

                entity.Property(e => e.IdphimBo).HasColumnName("IDPhimBo");

                entity.Property(e => e.IdtheLoai).HasColumnName("IDTheLoai");

                entity.Property(e => e.K)
                    .HasMaxLength(10)
                    .HasColumnName("k")
                    .IsFixedLength();

                entity.HasOne(d => d.IdphimBoNavigation)
                    .WithMany(p => p.TheLoaiPhimBos)
                    .HasForeignKey(d => d.IdphimBo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TheLoaiPhimBo_DSPhimBo");

                entity.HasOne(d => d.IdtheLoaiNavigation)
                    .WithMany(p => p.TheLoaiPhimBos)
                    .HasForeignKey(d => d.IdtheLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TheLoaiPhimBo_TheLoai");
            });

            modelBuilder.Entity<TheLoaiPhimLe>(entity =>
            {
                entity.HasKey(e => new { e.IdphimLe, e.IdtheLoai });

                entity.ToTable("TheLoaiPhimLe");

                entity.Property(e => e.IdphimLe).HasColumnName("IDPhimLe");

                entity.Property(e => e.IdtheLoai).HasColumnName("IDTheLoai");

                entity.Property(e => e.K)
                    .HasMaxLength(10)
                    .HasColumnName("k")
                    .IsFixedLength();

                entity.HasOne(d => d.IdphimLeNavigation)
                    .WithMany(p => p.TheLoaiPhimLes)
                    .HasForeignKey(d => d.IdphimLe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TheLoaiPhimLe_DSPhimLe");

                entity.HasOne(d => d.IdtheLoaiNavigation)
                    .WithMany(p => p.TheLoaiPhimLes)
                    .HasForeignKey(d => d.IdtheLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TheLoaiPhimLe_TheLoai");
            });

            modelBuilder.Entity<Tintucphim>(entity =>
            {
                entity.HasKey(e => e.Idtintuc)
                    .HasName("PK__tintucph__D9C06EC26CCE173A");

                entity.ToTable("tintucphim");

                entity.Property(e => e.Idtintuc).HasColumnName("idtintuc");

                entity.Property(e => e.Hinhanh).HasColumnName("hinhanh");

                entity.Property(e => e.Luotxem).HasColumnName("luotxem");

                entity.Property(e => e.Ngaycapnhat)
                    .HasColumnType("date")
                    .HasColumnName("ngaycapnhat");

                entity.Property(e => e.Noidung).HasColumnName("noidung");

                entity.Property(e => e.Tieude).HasColumnName("tieude");

                entity.Property(e => e.Tomtat).HasColumnName("tomtat");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
