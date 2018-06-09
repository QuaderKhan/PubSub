using EventBus.Abstractions;
using ProductCatalog.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.EventHandling.EventHandlers
{
    public class ProductPriceChangedHandler : IEventHandler<ProductPriceChangedEventArgs>
    {
        public ProductPriceChangedHandler()
        {

        }

        public async Task Handle(ProductPriceChangedEventArgs @event)
        {
            //Once receiving the product price changed event, update the product price in basket database.
            //Extract the values from event args
            var productId = @event.ProductId;
            var oldPrice = @event.OldPrice;
            var newPrice = @event.NewPrice;

            //update the basket DB
            await UpdateProductPriceInBasket(productId, oldPrice, newPrice);
        }

        private async Task UpdateProductPriceInBasket(int productId,double oldPrice, double newPrice)
        {
            //TODO : Call Basket DataAccessLayer to update the Basket
        }
    }
}
