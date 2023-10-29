using ClientsAPI.CQRS.Commands;
using ClientsAPI.Data;
using ClientsAPI.Models;
using MediatR;

namespace ClientsAPI.CQRS.Handlers
{
    public class DeleteClientHandler : IRequestHandler<DeleteClientCommand, bool>
    {
        private readonly ClientsApiContext _context;
        public DeleteClientHandler(ClientsApiContext context)
        {
            _context = context;
        }
        public Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            Client? client = _context.Clients.First(c => c.Id == request.Id);
            if (client == null) return Task.FromResult(false);
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return Task.FromResult(true);            
        }
    }
}
