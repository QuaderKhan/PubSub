using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Business;
using ProductCatalog.DataAccess.Interface;
using ProductCatalog.Model;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PubSubCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private IProductDataAccess productDataAccess;
        private readonly IProductBusiness productBusiness;
        public ProductController(IProductDataAccess ProductDataAccess, IProductBusiness ProductBusiness)
        {
            productDataAccess = ProductDataAccess;
            productBusiness = ProductBusiness;
        }

        [HttpGet]
        [Route("GetAll")]
        public IList<ProductModel> GetProducts()
        {
            return productDataAccess.GetProducts();
        }

        [HttpGet]
        [Route("Get/{Id}")]
        public ProductModel GetProduct(int Id)
        {
            return productDataAccess.GetProduct(Id);
        }

        [HttpPost]
        [SwaggerResponse(204, Description = "No Content")]
        [Route("Save")]
        public void Save([FromBody] ProductModel product)
        {
            productDataAccess.CreateProduct(product);
        }

        [HttpPut]
        [Route("Update")]
        [SwaggerResponse(204, Description = "No Content")]
        public void Update([FromBody] ProductModel product)
        {
            productBusiness.UpdateProduct(product);
        }
    }
}