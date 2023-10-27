using System;
using System.Collections.Generic;

namespace ClientsAPI.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long CardNumber { get; set; }

    public virtual Card Card { get; set; } = null!;

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();
}
