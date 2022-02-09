using IdentityAgentApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAgentApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetProducts()
        {
            var productList = new List<Product>
            {
                new Product
                {
                    Id =1,
                    Name = "Keyboard",
                    Price = 54,
                    Stock = 5
                },
                new Product
                {
                    Id =2,
                    Name = "Mouse",
                    Price = 54,
                    Stock = 5
                },

            };

            return Ok(productList);
        }
    }
}
