using ClientsAPI.Models;
using MediatR;

namespace ClientsAPI.CQRS.Queries
{
    public class GetAllClientsQuery : IRequest<List<Client>>
    {
    }
}
