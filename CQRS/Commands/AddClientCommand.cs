using ClientsAPI.Models;
using ClientsAPI.Models.DTO;
using MediatR;

namespace ClientsAPI.CQRS.Commands
{
    public class AddClientCommand : IRequest<Response>
    {
        public ClientDTO NewClient { get; set; }
        public AddClientCommand(ClientDTO client)
        {
            NewClient = client;
        }
    }
}
