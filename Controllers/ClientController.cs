using ClientsAPI.CQRS.Commands;
using ClientsAPI.CQRS.Queries;
using ClientsAPI.Data;
using ClientsAPI.Models;
using ClientsAPI.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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

        /// <summary>
        /// Получение списка всех клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllClient")]
        public async Task<IActionResult> GetAllClient()
        {
            var query = new GetAllClientsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Получение клиента по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClientById")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var query = new GetClientByIdQuery(id);
            var response = await _mediator.Send(query);
            if (response.Id == 0) return NotFound();
            return Ok(response);
        }

        /// <summary>
        /// Получение клиента по номеру телефона
        /// </summary>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetClientByPhone")]
        public async Task<IActionResult> GetClientByPhone(string phoneNumber)
        {
            var query = new GetClientByPhoneQuery(phoneNumber);
            var response = await _mediator.Send(query);
            if (response.Count == 0) return NotFound();
            return Ok(response);
        }
                       
        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="id">ID клиента</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("DeleteClient")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var command = new DeleteClientCommand(id);
            var response = await _mediator.Send(command);
            if (response) return Ok();
            //Client? client = Context.Clients.FirstOrDefault(c => c.Id == id);
            //    if (client != null)
            //    {
            //        Context.Clients.Remove(client);
            //        Context.SaveChanges();
            //        return Ok();
            //    }
            return BadRequest("Incorrect ID");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("AddClient")]
        public IActionResult AddClient(ClientDTO client)
        {
            Client result = new()
            {
                Id = client.Id,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                Card = new Card
                {
                    CardNumber = client.Card.CardNumber,
                    Validity = client.Card.Validity,
                    CardOwnerName = client.Card.CardOwnerName,
                    SecureCode = client.Card.SecureCode
                }
            };
                Context.Clients.Add(result);
                Context.SaveChanges();
                return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("UpdateClient")]
        public IActionResult UpdateClient(int id, string? name = null, string? phoneNumber = null, string? email = null)
        {
            var client = Context.Clients.Where(c => c.Id == id).First();
            if (client == null) return BadRequest(id);
            if (!string.IsNullOrEmpty(name)) client.Name = name;
            if (!string.IsNullOrEmpty(phoneNumber)) client.PhoneNumber = phoneNumber;
            if (!string.IsNullOrEmpty(email)) client.Email = email;
            Context.SaveChanges();
            return Ok();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerID"></param>
        /// <param name="secureCode"></param>
        /// <param name="validity"></param>
        /// <param name="cardOwnerName"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("UpdateCard")]
        public IActionResult UpdateCard(int ownerID, int secureCode, string? validity = null, string? cardOwnerName = null)
        {
            var card = Context.Cards.Where(c => c.ClientId == ownerID).First();
            if (card == null) return BadRequest(ownerID);
            if (!string.IsNullOrEmpty(validity)) card.Validity = validity;
            if (!string.IsNullOrEmpty(cardOwnerName)) card.CardOwnerName = cardOwnerName;
            if (secureCode > 99 && secureCode < 1000) card.SecureCode = secureCode;
            Context.SaveChanges();
            return Ok();
        }
    }
}
