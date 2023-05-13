using System;
using System.Collections.Generic;
using JumperCompany.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JumperCompany.Models;

public partial class JumperDbContext : DbContext
{
    private static JumperDbContext _context;

    public static JumperDbContext GetContext()
    {
        if (_context == null)
            _context = new JumperDbContext();
        return _context;
    }

    public JumperDbContext()
    {
    }

    public JumperDbContext(DbContextOptions<JumperDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<AgentType> AgentTypes { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialStockHistory> MaterialStockHistories { get; set; }

    public virtual DbSet<MaterialSupplier> MaterialSuppliers { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<MinAgentPriceHistory> MinAgentPriceHistories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductSale> ProductSales { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<SupplierType> SupplierTypes { get; set; }

    public virtual DbSet<SupplyHistory> SupplyHistories { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=JumperDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        optionsBuilder.UseLazyLoadingProxies();
    }

        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.ToTable("Agent");

            entity.Property(e => e.AgentId).ValueGeneratedNever();
            entity.Property(e => e.AgentName).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Kpp)
                .HasMaxLength(50)
                .HasColumnName("KPP");
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.Tin)
                .HasMaxLength(50)
                .HasColumnName("TIN");

            entity.HasOne(d => d.AgentType).WithMany(p => p.Agents)
                .HasForeignKey(d => d.AgentTypeId)
                .HasConstraintName("FK_Agent_AgentType");

            entity.HasOne(d => d.Director).WithMany(p => p.Agents)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("FK_Agent_Director");
        });

        modelBuilder.Entity<AgentType>(entity =>
        {
            entity.ToTable("AgentType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.ToTable("Director");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Surname).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("Material");

            entity.Property(e => e.MaterialName).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.MaterialType).WithMany(p => p.Materials)
                .HasForeignKey(d => d.MaterialTypeId)
                .HasConstraintName("FK_Material_MaterialType");

            entity.HasOne(d => d.UnitType).WithMany(p => p.Materials)
                .HasForeignKey(d => d.UnitTypeId)
                .HasConstraintName("FK_Material_UnitType");
        });

        modelBuilder.Entity<MaterialStockHistory>(entity =>
        {
            entity.ToTable("MaterialStockHistory");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialStockHistories)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MaterialStockHistory_Material");
        });

        modelBuilder.Entity<MaterialSupplier>(entity =>
        {
            entity.ToTable("MaterialSupplier");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialSuppliers)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK_MaterialSupplier_Material");

            entity.HasOne(d => d.Supplier).WithMany(p => p.MaterialSuppliers)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_MaterialSupplier_Supplier");
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.ToTable("MaterialType");
        });

        modelBuilder.Entity<MinAgentPriceHistory>(entity =>
        {
            entity.ToTable("MinAgentPriceHistory");

            entity.Property(e => e.PriceAfter).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PriceBefore).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.Agent).WithMany(p => p.MinAgentPriceHistories)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK_MinAgentPriceHistory_Agent");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.MinPriceForAgent).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ProductArticle).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("FK_Product_ProductType");
        });

        modelBuilder.Entity<ProductSale>(entity =>
        {
            entity.ToTable("ProductSale");

            entity.Property(e => e.RealizeDate).HasColumnType("date");

            entity.HasOne(d => d.Agent).WithMany(p => p.ProductSales)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK_ProductSale_Agent");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductSale_Product");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.ToTable("ProductType");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Tin)
                .HasMaxLength(50)
                .HasColumnName("TIN");

            entity.HasOne(d => d.SupplierType).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.SupplierTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supplier_SupplierType");
        });

        modelBuilder.Entity<SupplierType>(entity =>
        {
            entity.ToTable("SupplierType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<SupplyHistory>(entity =>
        {
            entity.ToTable("SupplyHistory");

            entity.HasOne(d => d.Material).WithMany(p => p.SupplyHistories)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("FK_SupplyHistory_Material");

            entity.HasOne(d => d.Supplier).WithMany(p => p.SupplyHistories)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_SupplyHistory_Supplier");
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.ToTable("UnitType");

            entity.Property(e => e.TypeName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
        
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
