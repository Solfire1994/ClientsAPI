using ClientsAPI.CQRS.Queries;
using ClientsAPI.Data;
using ClientsAPI.Models;
using ClientsAPI.Models.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.CQRS.Handlers
{
    public class GetClientByPhoneHandler : IRequestHandler<GetClientByPhoneQuery, List<ClientDTO>>
    {
        private readonly ClientsApiContext _context;
        public GetClientByPhoneHandler(ClientsApiContext context)
        {
            _context = context;
        }

        public Task<List<ClientDTO>> Handle(GetClientByPhoneQuery request, CancellationToken cancellationToken)
        {
            var clients = _context.Clients.Include(c => c.Card).Where(c => c.PhoneNumber == request.PhoneNumber);
            var response = new List<ClientDTO>();
            if (!clients.Any()) return Task.FromResult(response);            
            foreach (Client client in clients)
            {
                response.Add(new ClientDTO(client));
            }
            return Task.FromResult(response);
        }
    }
}
