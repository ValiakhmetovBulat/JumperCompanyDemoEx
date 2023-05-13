using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class MinAgentPriceHistory
{
    public int Id { get; set; }

    public int AgentId { get; set; }

    public decimal PriceBefore { get; set; }

    public decimal PriceAfter { get; set; }

    public virtual Agent Agent { get; set; } = null!;
}
