using ClientsAPI.Data;
using ClientsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.Controllers
{
    public class HomeController : ControllerBase
    {
        private ClientsApiContext Context;
        public HomeController(ClientsApiContext context) 
        {
            Context = context;
        }

        [HttpGet]
        [Route("selected-order")]
        public IActionResult Index(Client client)
        {
            var result = Context.Clients.Include(c => c.Card);
            return Ok(result);
        }
    }
}
