using EventBus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalog.EventHandling
{
    public class ProductPriceChangedEventArgs : EventArguments
    {
        public int ProductId { get; private set; }

        public double NewPrice { get; private set; }

        public double OldPrice { get; private set; }

        public ProductPriceChangedEventArgs(int productId, double newPrice, double oldPrice)
        {
            ProductId = productId;
            NewPrice = newPrice;
            OldPrice = oldPrice;
        }
    }
}
