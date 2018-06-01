using Microsoft.EntityFrameworkCore;
using ProductCatalog.DataAccess.EntityFramework;
using ProductCatalog.DataAccess.Interface;
using ProductCatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.DataAccess
{
    public class ProductDataAccess : IProductDataAccess
    {
        private readonly ProductDBContext _context;
        public ProductDataAccess(ProductDBContext context)
        {
            _context = context;
        }
        public int CreateProduct(ProductModel product)
        {
            _context.Products.Add(product);
            return _context.SaveChanges();
        }

        public void DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public ProductModel GetProduct(int Id)
        {
            return _context.Products.First(p => p.Id == Id);
            //var product = ProductList().First(p => p.Id == Id);
            //return  product;
        }

        public IList<ProductModel> GetProducts()
        {
            var products = _context.Products.Select(p => p).ToList<ProductModel>();
            return products;
            //return ProductList();
        }

        public int UpdateProduct(ProductModel product)
        {
            using (var db = _context)
            {
                db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                
                try
                {
                    return db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
            }
        }

        private IList<ProductModel> ProductList()
        {
            List<ProductModel> productList = new List<ProductModel>
            {
                new ProductModel { Id = 1, Name = "Product1", Price = 45.67, Type = 1 },
                new ProductModel { Id = 2, Name = "Product2", Price = 46, Type = 1 },
                new ProductModel { Id = 3, Name = "Product3", Price = 47.67, Type = 2 },
                new ProductModel { Id = 4, Name = "Product4", Price = 48, Type = 3 }
            };

            return productList;
        }
    }
}
