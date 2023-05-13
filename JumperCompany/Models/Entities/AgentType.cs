using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class AgentType
{
    public int AgentTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public override string ToString()
    {
        return TypeName;
    }
}
