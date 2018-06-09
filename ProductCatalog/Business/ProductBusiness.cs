using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.DataAccess.Interface;
using ProductCatalog.EventHandling;
using ProductCatalog.Model;

namespace ProductCatalog.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductDataAccess productDataAccess;
        private readonly IEventPublishService eventPublishService;
        public ProductBusiness(IProductDataAccess ProductDataAccess, IEventPublishService EventPublishService)
        {
            productDataAccess = ProductDataAccess;
            eventPublishService = EventPublishService;
        }
        public void UpdateProduct(ProductModel product)
        {
            var oldPriceFromDB=0.0;
            if (IsPriceChanged(product,out oldPriceFromDB))
            {
                //Raise the event to the queue
                var productPriceChangedEvent = new ProductPriceChangedEventArgs(product.Id, product.Price, oldPriceFromDB);
                eventPublishService.PublishThroughEventBusAsync(productPriceChangedEvent);
            }
        }

        private bool IsPriceChanged(ProductModel product, out double oldPriceFromDB)
        {
            var productFromDB = productDataAccess.GetProduct(product.Id);
            oldPriceFromDB = productFromDB.Price;
            return (productFromDB.Price != product.Price);
        }
    }
}
