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
        public int CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int Id)
        {
            var product = ProductList().First(p => p.Id == Id);
            return  product;
        }

        public IList<Product> GetProducts()
        {
            return ProductList();
        }

        public int UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        private IList<Product> ProductList()
        {
            List<Product> productList = new List<Product>
            {
                new Product { Id = 1, Name = "Product1", Price = 45.67, Type = 1 },
                new Product { Id = 2, Name = "Product2", Price = 46, Type = 1 },
                new Product { Id = 3, Name = "Product3", Price = 47.67, Type = 2 },
                new Product { Id = 4, Name = "Product4", Price = 48, Type = 3 }
            };

            return productList;
        }
    }
}
