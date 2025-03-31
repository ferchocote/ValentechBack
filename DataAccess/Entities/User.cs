using System;
using System.Collections.Generic;

namespace PruebaAppApi.DataAccess.Entities;

public partial class User
{
    public Guid ID { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool State { get; set; }

    public string? Salt { get; set; }

    public virtual ICollection<Patient> PatientCreationUserNavigation { get; } = new List<Patient>();

    public virtual ICollection<Patient> PatientModificationUserNavigation { get; } = new List<Patient>();
}
