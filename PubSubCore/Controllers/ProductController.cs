using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.DataAccess.Interface;
using ProductCatalog.Model;

namespace PubSubCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private IProductDataAccess productDataAccess;
        public ProductController(IProductDataAccess ProductDataAccess)
        {
            productDataAccess = ProductDataAccess;
        }

        [HttpGet]
        [Route("GetProducts")]
        public IList<Product> GetProducts()
        {
            return productDataAccess.GetProducts();
        }

        [HttpGet]
        [Route("GetProduct/{Id}")]
        public Product GetProduct(int Id)
        {
            return productDataAccess.GetProduct(Id);
        }
    }
}