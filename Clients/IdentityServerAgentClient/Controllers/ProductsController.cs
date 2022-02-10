using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerAgentClient.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetProducts()
        {
            var productList = await HttpClient.GetStringAsync("https://localhost:5006/api/products/getproducts");
            
            return Json(productList);
        }

        public async Task<IActionResult> IdentityServer()
        {
            var claims = User.Claims.ToList();
            var uid = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).ToList();


            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            var idToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
            var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            return View();
        }
    }
}
