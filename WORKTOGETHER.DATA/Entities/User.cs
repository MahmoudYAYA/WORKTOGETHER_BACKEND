using System;
using System.Collections.Generic;

namespace WORKTOGETHER.DATA.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Roles { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public DateTime DateCreation { get; set; }

    public sbyte Actif { get; set; }

    public sbyte IsVerified { get; set; }

    public string? Societe { get; set; }

    public string? Telephone { get; set; }

    public string? Adresse { get; set; }

    public string? Ville { get; set; }

    public string? Pays { get; set; }

    //public DateTime? DateNaissance { get; set; }

    //public string? Photo { get; set; }

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual ICollection<TicketSupport> TicketSupports { get; set; } = new List<TicketSupport>();

    // inactif ou pas 
    public string ActifLabel => Actif == 1 ? "✅ Actif" : "❌ Inactif";

    // Methode pour afficher les roles 
    public string RoleLabel
    {
        get
        {
            if (Roles.Contains("ROLE_ADMIN")) return "Admin";
            if (Roles.Contains("ROLE_COMPTABLE")) return "Comptable";
            if (Roles.Contains("ROLE_CLIENT")) return "Cleint";
            return "Inconnu";
        }

    }
}
