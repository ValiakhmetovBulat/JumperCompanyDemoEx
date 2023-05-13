using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class Director
{
    public int DirectorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Surname { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
}
