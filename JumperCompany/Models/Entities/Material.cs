using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class Material
{
    public int MaterialId { get; set; }

    public string MaterialName { get; set; } = null!;

    public int MaterialTypeId { get; set; }

    public int AmountInPackage { get; set; }

    public int UnitTypeId { get; set; }

    public int QuantityInStock { get; set; }

    public int MinQuantityInStock { get; set; }

    public string Description { get; set; } = null!;

    public byte[] MaterialImage { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<MaterialStockHistory> MaterialStockHistories { get; set; } = new List<MaterialStockHistory>();

    public virtual ICollection<MaterialSupplier> MaterialSuppliers { get; set; } = new List<MaterialSupplier>();

    public virtual MaterialType MaterialType { get; set; } = null!;

    public virtual ICollection<SupplyHistory> SupplyHistories { get; set; } = new List<SupplyHistory>();

    public virtual UnitType UnitType { get; set; } = null!;
}
