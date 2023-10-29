using ClientsAPI.Models.DTO;
using MediatR;

namespace ClientsAPI.CQRS.Queries
{
    public class GetClientByPhoneQuery : IRequest<List<ClientDTO>>
    {
        public string PhoneNumber { get; set; }
        public GetClientByPhoneQuery(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
