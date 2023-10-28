using ClientsAPI.Data;
using ClientsAPI.Models.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientsApiContext Context;
        public ClientController(ClientsApiContext context)
        {
            Context = context;
        }

        [HttpGet]
        [Route("Third-Client")]
        public IActionResult Index()
        {
            var res = Context.Clients.Include(c => c.Card).ToList();
            var result = new ClientDTO(res.First());
            return Ok(result);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("First-Client")]
        public IActionResult Index2()
        {
            var res = Context.Clients.Include(c => c.Card).ToList();
            var result = new ClientDTO(res.First());
            return Ok(result);
        }
    }
}
