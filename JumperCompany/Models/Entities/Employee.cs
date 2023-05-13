using System;
using System.Collections.Generic;

namespace JumperCompany.Models.Entities;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public int PassportSeries { get; set; }

    public int PassportNum { get; set; }

    public bool IsMarried { get; set; }

    public string? HealthProblems { get; set; }
}
