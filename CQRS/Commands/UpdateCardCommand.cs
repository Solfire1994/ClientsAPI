using MediatR;

namespace ClientsAPI.CQRS.Commands
{
    public class UpdateCardCommand : IRequest<bool>
    {
        public int OwnerId { get; set; }
        public int SecureCode { get; set; }
        public string Validity { get; set; }
        public string CardOwnerName { get; set; }
        public UpdateCardCommand(int ownerID, int secureCode, string? validity = null, string? cardOwnerName = null)
        {
            OwnerId = ownerID;
            SecureCode = secureCode;
            Validity = validity;
            CardOwnerName = cardOwnerName;
        }
    }
}
