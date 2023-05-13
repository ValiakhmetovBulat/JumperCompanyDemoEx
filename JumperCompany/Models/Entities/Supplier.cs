using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string Tin { get; set; } = null!;

    public int SupplierTypeId { get; set; }

    public virtual ICollection<MaterialSupplier> MaterialSuppliers { get; set; } = new List<MaterialSupplier>();

    public virtual SupplierType SupplierType { get; set; } = null!;

    public virtual ICollection<SupplyHistory> SupplyHistories { get; set; } = new List<SupplyHistory>();
}
