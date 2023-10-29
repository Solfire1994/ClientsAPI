using ClientsAPI.CQRS.Queries;
using ClientsAPI.Data;
using ClientsAPI.Models;
using ClientsAPI.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientsApiContext Context;
        private readonly IMediator _mediator;
        public ClientController(ClientsApiContext context, IMediator mediator)
        {
            Context = context;
            _mediator = mediator;
        }

        #region HttpGet
        [HttpGet]
        [Route("GetAllClient")]
        public async Task<IActionResult> GetAllClient()
        {
            var query = new GetAllClientsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetClientById")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var query = new GetClientByIdQuery(id);
            var response = await _mediator.Send(query);
            if (response.Id == 0) return NotFound();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetClientByPhone")]
        public async Task<IActionResult> GetClientByPhone(string phoneNumber)
        {
            var query = new GetClientByPhoneQuery(phoneNumber);
            var response = await _mediator.Send(query);
            if (response.Count == 0) return NotFound();
            return Ok(response);
        }

        #endregion

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("First-Client")]
        public IActionResult Index2()
        {
            var res = Context.Clients.Include(c => c.Card).ToList();
            var result = new ClientDTO(res.First());
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("DeleteClient")]
        public IActionResult DeleteClient()
        {
            var res = Context.Clients.Include(c => c.Card).ToList();
            var result = new ClientDTO(res.First());
            return Ok(result);
        }
    }
}
