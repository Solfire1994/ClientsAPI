using MediatR;

namespace ClientsAPI.CQRS.Commands
{
    public class DeleteClientCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteClientCommand(int id)
        {
            Id = id;
        }
    }
}
