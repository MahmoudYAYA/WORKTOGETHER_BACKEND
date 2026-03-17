using System;
using System.Collections.Generic;

namespace WORKTOGETHER.DATA.Entities;

public partial class Offre
{
    public int Id { get; set; }

    public string NomOffre { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int NombreUnites { get; set; }

    public decimal? PrixMensuel { get; set; }

    public decimal? PrixAnnuelle { get; set; }

    public int? ReductionAnnuelle { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
