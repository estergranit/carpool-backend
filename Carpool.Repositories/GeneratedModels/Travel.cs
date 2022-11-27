using System;
using System.Collections.Generic;

namespace Carpool.Repositories.GeneratedModels;

public partial class Travel
{
    public int TravelId { get; set; }

    public int? TravelDriverId { get; set; }

    public string? TravelOrigin { get; set; }

    public string? TravelDestination { get; set; }

    public DateOnly? TravelDepartureTime { get; set; }

    public int? TravelAvailable { get; set; }

    public string? TravelMap { get; set; }

    public virtual ICollection<Station> Stations { get; } = new List<Station>();

    public virtual User? TravelDriver { get; set; }
}
