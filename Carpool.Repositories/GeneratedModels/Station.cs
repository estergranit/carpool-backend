﻿using System;
using System.Collections.Generic;

namespace Carpool.Repositories.GeneratedModels;

public partial class Station
{
    public int StationId { get; set; }

    public int? StationTravelId { get; set; }

    public int? StationPassengerId { get; set; }

    public DateTimeOffset? StationTime { get; set; }

    public string? StationLocation { get; set; }

    public virtual User? StationPassenger { get; set; }

    public virtual Travel? StationTravel { get; set; }
}
