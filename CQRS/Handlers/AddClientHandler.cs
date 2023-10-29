using ClientsAPI.CQRS.Commands;
using ClientsAPI.Data;
using ClientsAPI.Models;
using ClientsAPI.Models.DTO;
using MediatR;

namespace ClientsAPI.CQRS.Handlers
{
    public class AddClientHandler : IRequestHandler<AddClientCommand, Response>
    {
        private readonly ClientsApiContext _context;
        public AddClientHandler(ClientsApiContext context)
        {
            _context = context;
        }

        public Task<Response> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            Response response = CheckClientFields(request.NewClient).Result;
            if (!response.IsValid) return Task.FromResult(response);
            Client result = new()
            {
                Id = request.NewClient.Id,
                Name = request.NewClient.Name,
                PhoneNumber = request.NewClient.PhoneNumber,
                Email = request.NewClient.Email,
                Card = new Card
                {
                    CardNumber = request.NewClient.Card.CardNumber,
                    Validity = request.NewClient.Card.Validity,
                    CardOwnerName = request.NewClient.Card.CardOwnerName,
                    SecureCode = request.NewClient.Card.SecureCode
                }
            };
            _context.Clients.Add(result);
            _context.SaveChanges();
            return Task.FromResult(response);
        }

        /// <summary>
        /// Метод для проверки корректонсти заполнения полей во входных данных
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private Task<Response> CheckClientFields(ClientDTO client)
        {
            if (string.IsNullOrEmpty(client.Name))
                return Task.FromResult(new Response(nameof(client.Name)));

            if (string.IsNullOrEmpty(client.PhoneNumber))
                return Task.FromResult(new Response(nameof(client.PhoneNumber)));

            if (string.IsNullOrEmpty(client.Email))
                return Task.FromResult(new Response(nameof(client.Email)));

            if (string.IsNullOrEmpty(client.Card.Validity))
                return Task.FromResult(new Response(nameof(client.Card.Validity)));

            if (string.IsNullOrEmpty(client.Card.CardOwnerName))
                return Task.FromResult(new Response(nameof(client.Card.CardOwnerName)));

            if (client.Card.SecureCode < 100 || client.Card.SecureCode > 999)
                return Task.FromResult(new Response(nameof(client.Card.SecureCode)));

            if (client.Card.CardNumber > 9999999999999999 || client.Card.CardNumber < 1000000000000000)
                return Task.FromResult(new Response(nameof(client.Card.CardNumber)));
            return Task.FromResult(new Response("All Fields are Correct", true));
        }


    }
}
