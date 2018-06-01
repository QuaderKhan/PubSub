using ProductCatalog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.Business
{
    public interface IProductBusiness
    {
        void UpdateProduct(ProductModel product);
    }
}
