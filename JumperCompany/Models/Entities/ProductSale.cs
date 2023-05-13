using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class ProductSale
{
    public int ProductSaleId { get; set; }

    public int AgentId { get; set; }

    public DateTime RealizeDate { get; set; }

    public int AmountOfProducts { get; set; }

    public int ProductId { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
