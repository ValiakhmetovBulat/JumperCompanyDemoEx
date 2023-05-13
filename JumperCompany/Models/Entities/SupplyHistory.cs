using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class SupplyHistory
{
    public int SupplyHistoryId { get; set; }

    public int MaterialId { get; set; }

    public int SupplierId { get; set; }

    public int AmountOfSupply { get; set; }

    public virtual Material Material { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
