using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class SupplierType
{
    public int SupplierTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
