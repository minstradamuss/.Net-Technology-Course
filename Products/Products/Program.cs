using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Products.Models;

class Program
{
    static void Main(string[] args)
    {
        using var db = new ProductsContext();

        // ---------- CREATE Product ----------
        var maxId = db.products.Any() ? db.products.Max(p => p.p_id) : 0;
        var newProduct = new product { p_id = maxId + 1, p_name = "TestProduct", price = 100 };
        db.products.Add(newProduct);
        db.SaveChanges();
        Console.WriteLine($"[CREATE] Added product with ID: {newProduct.p_id}");

        // ---------- READ ----------
        var allProducts = db.products.ToList();
        Console.WriteLine($"[READ] Total products: {allProducts.Count}");

        // ---------- UPDATE ----------
        newProduct.p_name = "UpdatedProduct";
        db.products.Update(newProduct);
        db.SaveChanges();
        Console.WriteLine("[UPDATE] Renamed product to: " + newProduct.p_name);

        // ---------- CREATE Order ----------
        long nextOrderId = db.orders.Any() ? db.orders.Max(o => o.o_id) + 1 : 1;
        var newOrder = new order
        {
            o_id = nextOrderId,
            order_date = BitConverter.GetBytes(DateTime.Now.Ticks)
        };
        db.orders.Add(newOrder);
        db.SaveChanges();
        Console.WriteLine($"[CREATE] Added order with ID: {newOrder.o_id}");

        // ---------- CREATE OrderItem ----------
        var newOrderItem = new orderitem
        {
            order_id = newOrder.o_id,
            product_id = newProduct.p_id,
            amount = 2.0,
            price = 100 // total потом вычисляется в БД
        };
        db.orderitems.Add(newOrderItem);
        db.SaveChanges();
        Console.WriteLine("[CREATE] Added orderitem with computed total");

        // ---------- AsNoTracking ----------
        var readOnlyProducts = db.products.AsNoTracking().ToList();
        Console.WriteLine("[AsNoTracking] Read-only products count: " + readOnlyProducts.Count);

        // ---------- Attach ----------
        db.Entry(newProduct).State = EntityState.Detached;
        var attachedProduct = new product { p_id = newProduct.p_id, p_name = "AttachUpdated" };
        db.products.Attach(attachedProduct);
        db.Entry(attachedProduct).Property(p => p.p_name).IsModified = true;
        db.SaveChanges();
        Console.WriteLine("[Attach] Product updated via Attach(): " + attachedProduct.p_name);

        // ---------- Eager Loading ----------
        var ordersWithItems = db.orders.Include(o => o.orderitems).ThenInclude(oi => oi.product).ToList();
        Console.WriteLine($"[Eager Loading] Orders loaded: {ordersWithItems.Count}");

        // ---------- Lazy Loading ----------
        var lazyOrder = db.orders.FirstOrDefault();
        if (lazyOrder != null)
        {
            var lazyOrderItems = lazyOrder.orderitems;
            Console.WriteLine($"[Lazy Loading] Lazy loaded orderitems count: {lazyOrderItems.Count}");
        }

        // ---------- SAFE DELETE ----------
        // удаляем сначала orderitems -> order -> product
        db.orderitems.RemoveRange(db.orderitems.Where(oi => oi.order_id == newOrder.o_id));
        db.orders.Remove(newOrder);
        db.products.Remove(attachedProduct);
        db.SaveChanges();
        Console.WriteLine("[DELETE] Safely deleted order, orderitems, and product.");
    }
}
