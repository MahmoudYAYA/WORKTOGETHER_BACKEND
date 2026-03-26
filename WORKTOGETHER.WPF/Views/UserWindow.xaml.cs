using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WORKTOGETHER.DATA.Repositories;
using System.Windows.Shapes;

namespace WORKTOGETHER.WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private readonly UserRepository _repository;

        public UserWindow()
        {
            InitializeComponent();
            _repository = new UserRepository();
            ChargerUsers();
        }

        private void ChargerUsers()
        {
            DgUtilisateurs.ItemsSource = _repository.FindAll();
        }
        private void BtnNouveau_Click(object sender, RoutedEventArgs e)
        {
            var fenetre = new NouvelleUserWindow();
            if (fenetre.ShowDialog() == true)
            {
                ChargerUsers();
            }
        }

        // methode pour surpprimer un utilisateur 
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            int userId = (int)btn.Tag;

            // Vérifie les tickets ouverts
            var ticketRepo = new TicketSupportRepository();
            var ticketsOuverts = ticketRepo.FindByClient(userId)
                .Where(t => t.DateFermeture == null)
                .ToList();

            // Vérifie les commandes en attente
            var commandeRepo = new CommandeRepository();
            var commandesEnAttente = commandeRepo.FindByClient(userId)
                .Where(c => c.StatutPaiement == "en_attente")
                .ToList();

            // Bloque si des éléments en cours
            if (ticketsOuverts.Count > 0 || commandesEnAttente.Count > 0)
            {
                string message = "Impossible de supprimer cet utilisateur !\n\n";

                if (ticketsOuverts.Count > 0)
                    message += $" {ticketsOuverts.Count} ticket(s) ouvert(s)\n";

                if (commandesEnAttente.Count > 0)
                    message += $" {commandesEnAttente.Count} commande(s) en attente\n";

                message += "\nRéglez ces éléments avant de supprimer.";

                MessageBox.Show(message, "Suppression impossible",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Confirmation
            var result = MessageBox.Show(
                "Voulez-vous vraiment supprimer cet utilisateur ?",
                "Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _repository.Delete(userId);
                ChargerUsers();
                MessageBox.Show("Utilisateur supprimé !", "Succès",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // une methode pour supprimer les relation d'abord si l'utilisateur à quelque chose en coure (commande, Ticket, ) pour pouvoir,  supprimer 
        //private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        //{
        //    var btn = sender as Button;
        //    int userId = (int)btn.Tag;

        //    var result = MessageBox.Show(
        //        "Voulez-vous vraiment supprimer cet utilisateur et toutes ses données ?",
        //        "Confirmation",
        //        MessageBoxButton.YesNo,
        //        MessageBoxImage.Question);

        //    if (result == MessageBoxResult.Yes)
        //    {
        //        // Supprime d'abord les tickets
        //        var ticketRepo = new TicketSupportRepository();
        //        var tickets = ticketRepo.FindByClient(userId);
        //        foreach (var ticket in tickets)
        //            ticketRepo.Delete(ticket.Id);

        //        // Supprime les commandes
        //        var commandeRepo = new CommandeRepository();
        //        var commandes = commandeRepo.FindByClient(userId);
        //        foreach (var commande in commandes)
        //            commandeRepo.Delete(commande.Id);

        //        // Supprime l'utilisateur
        //        _repository.Delete(userId);

        //        ChargerUsers();
        //        MessageBox.Show("Utilisateur supprimé !", "Succès",
        //                        MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //}
        // une methode pour acitver ou désactiver le utilisateur 
        private void BtnToggle_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            int userId = (int)btn.Tag;

            _repository.ToggleActif(userId);
            ChargerUsers();

            MessageBox.Show("Utilisateur modifié !", "Succès",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // une methode pour modifier 
        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            int userId = (int)btn.Tag;

            var user = _repository.FindById(userId);
            var fenetre = new ModifierUserWindow(user);

            if (fenetre.ShowDialog() == true)
            {
                ChargerUsers();
            }
        }
    }

}