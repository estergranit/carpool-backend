using System;
using System.Collections.Generic;

namespace Carpool.Repositories.GeneratedModels;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPhone { get; set; }

    public virtual ICollection<Station> Stations { get; } = new List<Station>();

    public virtual ICollection<Travel> Travels { get; } = new List<Travel>();
}
