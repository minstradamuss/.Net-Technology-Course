using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

using Serilog;


namespace Products.Models;

public partial class ProductsContext : DbContext
{
    public ProductsContext()
    {
    }

    public ProductsContext(DbContextOptions<ProductsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<order> orders { get; set; }

    public virtual DbSet<orderitem> orderitems { get; set; }

    public virtual DbSet<product> products { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder
    //            .UseLazyLoadingProxies()
    //            .UseSqlite("Data Source=C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\Products\\Products\\Products.db")
    //            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    //    }
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string dbPath = "C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\Products\\Products\\Products.db";
            string logFilePath = Path.Combine(Path.GetDirectoryName(dbPath), "ef-logs.txt");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Infinite)
                .CreateLogger();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog();
            });

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlite($"Data Source={dbPath}");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<order>(entity =>
        {
            entity.HasKey(e => e.o_id);

            entity.ToTable("order");

            entity.Property(e => e.o_id).ValueGeneratedNever();
            entity.Property(e => e.order_date).HasColumnType("DATE");
        });

        modelBuilder.Entity<orderitem>(entity =>
        {
            entity.HasKey(e => new { e.order_id, e.product_id });

            entity.ToTable("orderitem");

            entity.Property(e => e.amount).HasColumnType("NUMERIC(7,2)");
            entity.Property(e => e.total)
                  .HasColumnName("total")
                  .HasComputedColumnSql("amount * price", stored: true);

            entity.HasOne(d => d.order).WithMany(p => p.orderitems).HasForeignKey(d => d.order_id);
            entity.HasOne(d => d.product).WithMany(p => p.orderitems).HasForeignKey(d => d.product_id);
        });

        modelBuilder.Entity<product>(entity =>
        {
            entity.HasKey(e => e.p_id);

            entity.ToTable("product");

            entity.Property(e => e.p_id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
