using System.Windows;
using System.Windows.Controls;
using WORKTOGETHER.DATA.Entities;
using WORKTOGETHER.DATA.Repositories;
using WORKTOGETHER.WPF.Baies;

namespace WORKTOGETHER.WPF.Unites
{
    public partial class UnitePage : Page
    {
        private readonly UniteRepository _uniteRepo = new UniteRepository();

        // Unité sélectionnée
        private Unite _uniteSelectionnee = null;

        public UnitePage()
        {
            InitializeComponent();
            ChargerDonnees();
        }

        // ── Charge toutes les unités avec leur baie ──
        private void ChargerDonnees()
        {
            DgUnites.ItemsSource = _uniteRepo.FindAllWithDetails();
        }

        // ── Quand on clique sur une ligne → affiche le détail ──
        private void DgUnites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _uniteSelectionnee = DgUnites.SelectedItem as Unite;
            if (_uniteSelectionnee != null)
            {
                AfficherDetail(_uniteSelectionnee);
            }
        }

        // ── Affiche le détail de l'unité ──
        private void AfficherDetail(Unite unite)
        {
            TxtNumero.Text = unite.NumeroUnite;
            TxtNom.Text = unite.NomUnite;
            TxtBaie.Text = unite.Baie?.NumeroBaie ?? "Non définie";
            TxtStatut.Text = unite.Statut;

            // Affiche le client si l'unité est occupée
            TxtClient.Text = unite.Reservation?.Client?.Prenom + " " +
                             unite.Reservation?.Client?.Nom ?? "Disponible";

            // Sélectionne le bon état dans le ComboBox
            foreach (ComboBoxItem item in CmbEtat.Items)
            {
                if (item.Tag.ToString() == unite.Etat)
                {
                    CmbEtat.SelectedItem = item;
                    break;
                }
            }
        }

        // ── Bouton ENREGISTRER → modifie seulement l'état ──
        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (_uniteSelectionnee == null)
            {
                MessageBox.Show("Veuillez sélectionner une unité !", "Erreur",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CmbEtat.SelectedItem == null)
            {
                TxtErreur.Text = "Veuillez choisir un état !";
                TxtErreur.Visibility = Visibility.Visible;
                return;
            }

            var selectedEtat = CmbEtat.SelectedItem as ComboBoxItem;
            _uniteSelectionnee.Etat = selectedEtat.Tag.ToString();
            _uniteRepo.Update(_uniteSelectionnee);

            ChargerDonnees();

            MessageBox.Show("État mis à jour !", "Succès",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}