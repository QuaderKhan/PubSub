using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.DataAccess.Interface;
using ProductCatalog.Model;

namespace ProductCatalog.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductDataAccess productDataAccess;
        public ProductBusiness(IProductDataAccess ProductDataAccess)
        {
            productDataAccess = ProductDataAccess;
        }
        public void UpdateProduct(ProductModel product)
        {
            if (IsPriceChanged(product))
            {
                //Raise the event to the queue
            }
        }

        private bool IsPriceChanged(ProductModel product)
        {
            var productFromDB = productDataAccess.GetProduct(product.Id);

            return (productFromDB.Price != product.Price);
        }
    }
}
