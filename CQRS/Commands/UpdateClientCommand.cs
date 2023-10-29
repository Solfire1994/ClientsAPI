using MediatR;

namespace ClientsAPI.CQRS.Commands
{
    public class UpdateClientCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public UpdateClientCommand(int id, string? name = null, string? phoneNumber = null, string? email = null)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;    
        }
    }
}
