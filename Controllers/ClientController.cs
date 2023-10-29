using ClientsAPI.CQRS.Commands;
using ClientsAPI.CQRS.Queries;
using ClientsAPI.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClientsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ClientController(IMediator mediator)
        {
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
            return BadRequest("Incorrect ID");
        }

        /// <summary>
        /// Добавление нового клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("AddClient")]
        public async Task<IActionResult> AddClient(ClientDTO client)
        {
            var command = new AddClientCommand(client);
            var response = await _mediator.Send(command);
            if (response.IsValid) return Ok();
            return BadRequest($"Entered incorrect values:{response.Value}");
        }

        /// <summary>
        /// Обновление данных клиента
        /// </summary>
        /// <param name="id">ID Клиента</param>
        /// <param name="name">Имя Клиента</param>
        /// <param name="phoneNumber">Номер телефона</param>
        /// <param name="email">Адрес электронной почты</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("UpdateClient")]
        public async Task<IActionResult> UpdateClient(int id, string? name = null, string? phoneNumber = null, string? email = null)
        {
            var command = new UpdateClientCommand(id, name, phoneNumber, email);
            var response = await _mediator.Send(command);
            if (response) return Ok();
            return BadRequest("Incorrect ID");
        }


        /// <summary>
        /// Обновление данных карты клиента
        /// </summary>
        /// <param name="ownerID">ID владельца карты</param>
        /// <param name="secureCode">Трехзначится защитный код</param>
        /// <param name="validity">Срок действия</param>
        /// <param name="cardOwnerName">Имя владельца карты</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("UpdateCard")]
        public async Task<IActionResult> UpdateCard(int ownerID, int secureCode, string? validity = null, string? cardOwnerName = null)
        {
            var command = new UpdateCardCommand(ownerID, secureCode, validity, cardOwnerName);
            var response = await _mediator.Send(command);
            if (response) return Ok();
            return BadRequest("Incorrect Owner ID");
        }
    }
}
