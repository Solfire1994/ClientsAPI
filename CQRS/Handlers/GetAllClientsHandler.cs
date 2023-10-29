using ClientsAPI.CQRS.Queries;
using ClientsAPI.Data;
using ClientsAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClientsAPI.CQRS.Handlers
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsQuery, List<Client>>
    {
        private readonly ClientsApiContext _context;
        public GetAllClientsHandler(ClientsApiContext context) 
        {
            _context = context;
        }
        public Task<List<Client>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var response = _context.Clients.Include(c => c.Card).ToList();
            return Task.FromResult(response);
        }
    }
}
