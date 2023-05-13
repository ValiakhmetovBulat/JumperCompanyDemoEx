using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class MaterialStockHistory
{
    public int MaterialStockHistoryId { get; set; }

    public int MaterialId { get; set; }

    public int StockBefore { get; set; }

    public int StockAfter { get; set; }

    public virtual Material Material { get; set; } = null!;
}
