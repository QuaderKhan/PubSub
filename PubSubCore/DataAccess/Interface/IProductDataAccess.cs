using ProductCatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.DataAccess.Interface
{
    public interface IProductDataAccess
    {
        IList<Product> GetProducts();

        Product GetProduct(int Id);

        int CreateProduct(Product product);

        int UpdateProduct(Product product);

        void DeleteProduct(int Id);
    }
}
