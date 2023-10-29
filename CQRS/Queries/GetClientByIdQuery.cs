using ClientsAPI.Models.DTO;
using MediatR;

namespace ClientsAPI.CQRS.Queries
{
    public class GetClientByIdQuery : IRequest<ClientDTO>
    {
        public int Id { get; set; }
        public GetClientByIdQuery(int id)
        {
            Id = id;
        }
    }
}
