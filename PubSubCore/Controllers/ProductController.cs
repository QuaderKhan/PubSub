using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PubSubCore.Controllers
{
    [Produces("application/json")]
    [Route("api/Home")]
    public class ProductController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "Hello welcome to Product catalog service";
        }
    }
}