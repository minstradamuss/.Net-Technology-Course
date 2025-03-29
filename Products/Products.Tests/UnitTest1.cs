//using Products.Models;
//using Microsoft.EntityFrameworkCore;
//using Xunit;
//using System.Linq;

//namespace Products.Tests
//{
//    public class UnitTest1
//    {
//        [Fact]
//        public void Test_Create_Product()
//        {
//            using var db = new ProductsContext();
//            var p = new Product { PName = "TestProduct", Price = 123.45 };
//            db.Products.Add(p);
//            db.SaveChanges();
//            Assert.True(db.Products.Any(x => x.PName == "TestProduct"));
//        }

//        [Fact]
//        public void Test_Create_Order()
//        {
//            using var db = new ProductsContext();
//            var o = new Order { OId = 2000, OrderDate = BitConverter.GetBytes(20250329) };
//            db.Orders.Add(o);
//            db.SaveChanges();
//            Assert.True(db.Orders.Any(x => x.OId == 2000));
//        }

//        [Fact]
//        public void Test_Create_OrderItem()
//        {
//            using var db = new ProductsContext();
//            var item = new Orderitem { OrderId = 2000, ProductId = 1, Amount = BitConverter.GetBytes(2), Price = 50 };
//            db.Orderitems.Add(item);
//            db.SaveChanges();
//            Assert.True(db.Orderitems.Any(oi => oi.OrderId == 2000 && oi.ProductId == 1));
//        }

//        [Fact]
//        public void Test_Product_NotNull()
//        {
//            var product = new Product();
//            Assert.NotNull(product);
//        }

//        [Fact]
//        public void Test_Orderitems_Default_Empty()
//        {
//            var order = new Order();
//            Assert.Empty(order.Orderitems);
//        }

//        [Fact]
//        public void Test_Product_Default_Price_Null()
//        {
//            var product = new Product();
//            Assert.Null(product.Price);
//        }

//        [Fact]
//        public void Test_Orderitem_ComputedTotal()
//        {
//            var item = new Orderitem { Amount = BitConverter.GetBytes(5), Price = 10 };
//            Assert.Equal(50, item.ComputedTotal);
//        }

//        [Fact]
//        public void Test_Adding_Duplicate_ProductId()
//        {
//            using var db = new ProductsContext();
//            var p1 = new Product { PId = 999, PName = "DuplicateProduct", Price = 10 };
//            var p2 = new Product { PId = 999, PName = "DuplicateProduct2", Price = 20 };
//            db.Products.Add(p1);
//            db.SaveChanges();
//            db.Products.Add(p2);
//            Assert.Throws<DbUpdateException>(() => db.SaveChanges());
//        }

//        [Fact]
//        public void Test_Read_Products_AsNoTracking()
//        {
//            using var db = new ProductsContext();
//            var list = db.Products.AsNoTracking().ToList();
//            Assert.NotNull(list);
//        }

//        [Fact]
//        public void Test_Update_Product()
//        {
//            using var db = new ProductsContext();
//            var product = db.Products.FirstOrDefault();
//            if (product != null)
//            {
//                product.PName = "UpdatedName";
//                db.SaveChanges();
//                Assert.True(db.Products.Any(p => p.PName == "UpdatedName"));
//            }
//        }
//    }
//}
