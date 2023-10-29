namespace ClientsAPI.Models.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public CardDTO Card { get; set; } = null!;

        public ClientDTO () { Id = 0; }
        public ClientDTO (Client client)
        {
            Id = client.Id;
            Name = client.Name;
            PhoneNumber = client.PhoneNumber;
            Email = client.Email;
            Card = new CardDTO
            {
                    CardNumber = client.Card.CardNumber,
                    Validity = client.Card.Validity,
                    CardOwnerName = client.Card.CardOwnerName,
                    SecureCode = client.Card.SecureCode
            };
        }
    }
}
