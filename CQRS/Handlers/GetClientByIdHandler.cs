using ClientsAPI.CQRS.Queries;
using ClientsAPI.Data;
using ClientsAPI.Models.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.CQRS.Handlers
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdQuery, ClientDTO>
    {
        private readonly ClientsApiContext _context;
        public GetClientByIdHandler(ClientsApiContext context)
        {
            _context = context;
        }

        public Task<ClientDTO> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var clients = _context.Clients.Include(c => c.Card).Where(c => c.Id == request.Id);
            if (!clients.Any()) return Task.FromResult(new ClientDTO());            
            var response = new ClientDTO(clients.First());
            return Task.FromResult(response);
        }
    }
}
