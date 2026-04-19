using System.Windows;
using System.Windows.Controls;
using WORKTOGETHER.DATA.Entities;
using WORKTOGETHER.DATA.Repositories;

namespace WORKTOGETHER.WPF.Commandes
{
    public partial class CommandePage : Page
    {
        private readonly CommandeController _controller = new CommandeController();
        private Commande _commandeSelectionnee = null;

        public CommandePage()
        {
            InitializeComponent();
            ChargerDonnees();
        }

        private void ChargerDonnees()
        {
            DgCommandes.ItemsSource = _controller.GetAll();
            ViderDetail();
        }

        private void DgCommandes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _commandeSelectionnee = DgCommandes.SelectedItem as Commande;
            if (_commandeSelectionnee != null)
                AfficherDetail(_commandeSelectionnee);
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (_commandeSelectionnee == null)
            {
                MessageBox.Show("Veuillez sélectionner une commande !", "Erreur",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Annuler la commande '{_commandeSelectionnee.NumeroCommande}' ?",
                "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            var (succes, message) = _controller.Annuler(_commandeSelectionnee.Id);
            MessageBox.Show(message, succes ? "Succès" : "Erreur",
                            MessageBoxButton.OK,
                            succes ? MessageBoxImage.Information : MessageBoxImage.Warning);

            if (succes) ChargerDonnees();
        }

        private void AfficherDetail(Commande commande)
        {
            TxtNumero.Text = commande.NumeroCommande;
            TxtClient.Text = commande.Client?.Prenom + " " + commande.Client?.Nom;
            TxtOffre.Text = commande.Offre?.NomOffre;
            TxtTypePaiement.Text = commande.TypePaiement ?? "Non défini";
            TxtMontantHT.Text = commande.MontantTotal + " €";
            TxtTVA.Text = commande.MontantTva + " €";
            TxtStatut.Text = commande.StatutPaiement;
            TxtDateDebut.Text = commande.DateDebutService.ToString("dd/MM/yyyy");
            TxtDateFin.Text = commande.DateFinService.ToString("dd/MM/yyyy");

            if (commande.StripeCardLast4 != null)
                TxtCarte.Text = commande.StripeCardBrand + " **** " + commande.StripeCardLast4;
            else
                TxtCarte.Text = "Non renseignée";

            if (commande.Reservation != null)
                DgUnites.ItemsSource = commande.Reservation.Unites;
            else
                DgUnites.ItemsSource = null;
        }

        private void ViderDetail()
        {
            TxtNumero.Text = "";
            TxtClient.Text = "";
            TxtOffre.Text = "";
            TxtTypePaiement.Text = "";
            TxtMontantHT.Text = "";
            TxtTVA.Text = "";
            TxtStatut.Text = "";
            TxtDateDebut.Text = "";
            TxtDateFin.Text = "";
            TxtCarte.Text = "";
            DgUnites.ItemsSource = null;
        }
    }
}