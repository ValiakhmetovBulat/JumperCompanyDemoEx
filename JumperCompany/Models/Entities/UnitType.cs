using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class UnitType
{
    public int UnitTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
