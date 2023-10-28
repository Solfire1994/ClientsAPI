using ClientsAPI.Data;
using ClientsAPI.Models;
using ClientsAPI.Models.DTO;
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
        [Route("First Client")]
        public IActionResult Index()
        {
            var res = Context.Clients.Include(c => c.Card).ToList();
            var result = new ClientDTO(res.First());            
            return Ok(result);
        }
    }
}
