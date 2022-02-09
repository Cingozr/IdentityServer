using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServerAgentClient.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();

            var disco = await httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
            {
                //LOGS
            }

            var clientCrediantialsTokenRequest = new ClientCredentialsTokenRequest();
            clientCrediantialsTokenRequest.ClientId = _configuration["AgentClient:ClientId"];
            clientCrediantialsTokenRequest.ClientSecret = _configuration["AgentClient:ClientSecrets"];
            clientCrediantialsTokenRequest.Address = disco.TokenEndpoint;

            var token = await httpClient.RequestClientCredentialsTokenAsync(clientCrediantialsTokenRequest);

            if (token.IsError)
            {
                //LOGS
            }
            //https://localhost:5002

            httpClient.SetBearerToken(token.AccessToken);

            var response = await httpClient.GetAsync("https://localhost:5006/api/products/getproducts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Json(content);
            }

            return View();
        }
    }
}
