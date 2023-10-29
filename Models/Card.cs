using System;
using System.Collections.Generic;

namespace ClientsAPI.Models;

public partial class Card
{
    public long CardNumber { get; set; }

    public string Validity { get; set; } = null!;

    public string CardOwnerName { get; set; } = null!;

    public int SecureCode { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
