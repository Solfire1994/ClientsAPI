using System.ComponentModel.DataAnnotations;

namespace ClientsAPI.Models.DTO
{
    public class CardDTO
    {
        public long CardNumber { get; set; }
        public string Validity { get; set; } = null!;
        public string CardOwnerName { get; set; } = null!;
        public int SecureCode { get; set; }
    }
}
