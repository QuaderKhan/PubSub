using ProductCatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.DataAccess.Interface
{
    public interface IProductDataAccess
    {
        IList<ProductModel> GetProducts();

        ProductModel GetProduct(int Id);

        int CreateProduct(ProductModel product);

        int UpdateProduct(ProductModel product);

        void DeleteProduct(int Id);
    }
}
