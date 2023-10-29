using ClientsAPI.CQRS.Commands;
using ClientsAPI.Data;
using MediatR;

namespace ClientsAPI.CQRS.Handlers
{
    public class UpdateCardHandler : IRequestHandler<UpdateCardCommand, bool>
    {
        private readonly ClientsApiContext _context;
        public UpdateCardHandler(ClientsApiContext context)
        {
            _context = context;
        }
        public Task<bool> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
        {
            var card = _context.Cards.Where(c => c.ClientId == request.OwnerId).First();
            if (card == null) return Task.FromResult(false);
            if (!string.IsNullOrEmpty(request.Validity)) card.Validity = request.Validity;
            if (!string.IsNullOrEmpty(request.CardOwnerName)) card.CardOwnerName = request.CardOwnerName;
            if (request.SecureCode > 99 && request.SecureCode < 1000) card.SecureCode = request.SecureCode;
            _context.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
