using IdentityUserApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityUserApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetUsers()
        {
            var userList = new List<User>
            {
                new User
                {
                    Id=1,
                    Name = "Cingoz Recai"
                },
                new User
                {
                    Id=2,
                    Name = "Cino"
                },

            };

            return Ok(userList);
        }
    }
}
