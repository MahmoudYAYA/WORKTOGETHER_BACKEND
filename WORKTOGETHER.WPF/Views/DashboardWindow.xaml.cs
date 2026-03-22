using System.Windows;
using System.Windows.Controls;
using System.Linq;
using WORKTOGETHER.DATA.Repositories;

namespace WORKTOGETHER.WPF.Views
{
    public partial class DashboardPage : Page
    {
        private readonly UserRepository _userRepo = new UserRepository();
        private readonly BaieRepository _baieRepo = new BaieRepository();
        private readonly CommandeRepository _commandeRepo = new CommandeRepository();
        private readonly TicketSupportRepository _ticketRepo = new TicketSupportRepository();

        public DashboardPage()
        {
            InitializeComponent();
            ChargerStatistiques();
        }

        private void ChargerStatistiques()
        {
            // Cartes
            TxtNbClients.Text = _userRepo.FindAll()
                .Count(u => u.Roles.Contains("ROLE_CLIENT")).ToString();

            TxtNbBaies.Text = _baieRepo.FindAll().Count.ToString();

            TxtNbCommandes.Text = _commandeRepo.FindAll()
                .Count(c => c.StatutPaiement == "paye").ToString();

            TxtNbTickets.Text = _ticketRepo.FindAll()
                .Count(t => t.DateFermeture == null).ToString();

            // Dernières commandes
            DgDernieresCommandes.ItemsSource = _commandeRepo
                .FindAllWithDetails()
                .OrderByDescending(c => c.DateCommande)
                .Take(5)
                .ToList();

            // Tickets ouverts
            DgTicketsOuverts.ItemsSource = _ticketRepo
                .FindAllWithDetails()
                .Where(t => t.DateFermeture == null)
                .Take(5)
                .ToList();
        }
    }
}