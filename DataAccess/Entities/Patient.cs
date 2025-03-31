using System;
using System.Collections.Generic;

namespace PruebaAppApi.DataAccess.Entities;

public partial class Patient
{
    public Guid ID { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string DocumentNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public bool State { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid CreationUser { get; set; }

    public DateTime? ModificationDate { get; set; }

    public Guid? ModificationUser { get; set; }

    public string Disease { get; set; } = null!;

    public virtual User CreationUserNavigation { get; set; } = null!;

    public virtual User? ModificationUserNavigation { get; set; }
}
