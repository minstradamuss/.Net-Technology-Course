using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Products.Models;
using System.Linq;

public class ProductTests
{
    private ProductsContext GetInMemoryContext()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ProductsContext>()
            .UseSqlite(connection)
            .Options;

        var context = new ProductsContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public void CanAddProduct()
    {
        using var context = GetInMemoryContext();

        var product = new product { p_id = 1, p_name = "TestProduct", price = 100 };
        context.products.Add(product);
        context.SaveChanges();

        var saved = context.products.FirstOrDefault(p => p.p_id == 1);
        Assert.NotNull(saved);
        Assert.Equal("TestProduct", saved.p_name);
    }

    [Fact]
    public void CanUpdateProduct()
    {
        using var context = GetInMemoryContext();

        var product = new product { p_id = 1, p_name = "Initial", price = 50 };
        context.products.Add(product);
        context.SaveChanges();

        product.p_name = "Updated";
        context.products.Update(product);
        context.SaveChanges();

        var updated = context.products.First(p => p.p_id == 1);
        Assert.Equal("Updated", updated.p_name);
    }

    [Fact]
    public void CanDeleteProduct()
    {
        using var context = GetInMemoryContext();

        var product = new product { p_id = 1, p_name = "ToDelete", price = 50 };
        context.products.Add(product);
        context.SaveChanges();

        context.products.Remove(product);
        context.SaveChanges();

        var exists = context.products.Any(p => p.p_id == 1);
        Assert.False(exists);
    }
}
