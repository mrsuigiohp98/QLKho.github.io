using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory2.API.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Inventory>().ToTable("Inventories");
            builder.Entity<Inventory>().HasKey(p => p.Id);
            builder.Entity<Inventory>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Inventory>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Inventory>().Property(p => p.Soluong).IsRequired().HasMaxLength(30);
            builder.Entity<Inventory>().Property(p => p.NoiSX).IsRequired().HasMaxLength(30);
            builder.Entity<Inventory>().HasMany(p => p.Receipts).WithOne(p => p.Inventory).HasForeignKey(p => p.InventoryId);
            builder.Entity<Inventory>().HasMany(p => p.Deliveries).WithOne(p => p.Inventory).HasForeignKey(p => p.InventoryId);
            //builder.Entity<Inventory>().HasData
            //(
            //    new Inventory { Id = 100, Name = "Loa" }, // Id set manually due to in-memory provider
            //    new Inventory { Id = 101, Name = "Tủ lạnh" }
            //);

            builder.Entity<Stock>().ToTable("Stocks");
            builder.Entity<Stock>().HasKey(p => p.Id);
            builder.Entity<Stock>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Stock>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Stock>().HasMany(p => p.Inventories).WithOne(p => p.Stock).HasForeignKey(p => p.StockId);


            builder.Entity<Unit>().ToTable("Units");
            builder.Entity<Unit>().HasKey(p => p.Id);
            builder.Entity<Unit>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Unit>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Unit>().Property(p => p.Description).IsRequired().HasMaxLength(30);
            builder.Entity<Unit>().HasMany(p => p.Inventories).WithOne(p => p.Unit).HasForeignKey(p => p.UnitId);


            builder.Entity<Customer>().ToTable("Customers");
            builder.Entity<Customer>().HasKey(p => p.Id);
            builder.Entity<Customer>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Customer>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Customer>().Property(p => p.Diachi).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(p => p.Sdt).IsRequired().HasMaxLength(30);
            builder.Entity<Customer>().HasMany(p => p.Deliveries).WithOne(p => p.Customer).HasForeignKey(p => p.CustomerId);


            builder.Entity<Receipt>().ToTable("Receipts");
            builder.Entity<Receipt>().HasKey(p => p.Id);
            builder.Entity<Receipt>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Receipt>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Receipt>().Property(p => p.Ngaynhap).IsRequired().HasMaxLength(30);
            builder.Entity<Receipt>().Property(p => p.Soluong).IsRequired().HasMaxLength(30);
            builder.Entity<Receipt>().Property(p => p.Dongia).IsRequired().HasMaxLength(30);
            builder.Entity<Receipt>().Property(p => p.Thanhtien).IsRequired().HasMaxLength(30);
            

            builder.Entity<Delivery>().ToTable("Deliveries");
            builder.Entity<Delivery>().HasKey(p => p.Id);
            builder.Entity<Delivery>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Delivery>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Delivery>().Property(p => p.Ngayxuat).IsRequired().HasMaxLength(30);
            builder.Entity<Delivery>().Property(p => p.Soluong).IsRequired().HasMaxLength(30);
            builder.Entity<Delivery>().Property(p => p.Dongia).IsRequired().HasMaxLength(30);
            builder.Entity<Delivery>().Property(p => p.Thanhtien).IsRequired().HasMaxLength(30);

            builder.Entity<Supplier>().ToTable("Suppliers");
            builder.Entity<Supplier>().HasKey(p => p.Id);
            builder.Entity<Supplier>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Supplier>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Supplier>().Property(p => p.Diachi).IsRequired().HasMaxLength(50);
            builder.Entity<Supplier>().Property(p => p.Sdt).IsRequired().HasMaxLength(30);
            builder.Entity<Supplier>().HasMany(p => p.Receipts).WithOne(p => p.Supplier).HasForeignKey(p => p.SupplierId);
        }
    }
}
