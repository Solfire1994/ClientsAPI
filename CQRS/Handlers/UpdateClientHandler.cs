using ClientsAPI.CQRS.Commands;
using ClientsAPI.Data;
using MediatR;

namespace ClientsAPI.CQRS.Handlers
{
    public class UpdateClientHandler : IRequestHandler<UpdateClientCommand, bool>
    {
        private readonly ClientsApiContext _context;
        public UpdateClientHandler(ClientsApiContext context)
        {
            _context = context;
        }
        public Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = _context.Clients.Where(c => c.Id == request.Id).First();
            if (client == null) return Task.FromResult(false);
            if (!string.IsNullOrEmpty(request.Name)) client.Name = request.Name;
            if (!string.IsNullOrEmpty(request.PhoneNumber)) client.PhoneNumber = request.PhoneNumber;
            if (!string.IsNullOrEmpty(request.Email)) client.Email = request.Email;
            _context.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
