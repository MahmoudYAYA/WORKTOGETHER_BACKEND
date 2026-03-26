using System.Windows;
using System.Windows.Controls;
using WORKTOGETHER.DATA.Entities;
using WORKTOGETHER.DATA.Repositories;

namespace WORKTOGETHER.WPF.Views
{
    public partial class ModifierUserWindow : Window
    {
        private readonly UserRepository _userRepo = new UserRepository();
        private readonly User _user;

        // ← Reçoit l'utilisateur à modifier
        public ModifierUserWindow(User user)
        {
            InitializeComponent();
            _user = user;
            RemplirFormulaire();
        }

        private void RemplirFormulaire()
        {
            // Pré-remplit les champs avec les données existantes
            TxtNom.Text = _user.Nom;
            TxtPrenom.Text = _user.Prenom;
            TxtEmail.Text = _user.Email;

            // Sélectionne le bon rôle dans le ComboBox
            foreach (ComboBoxItem item in CmbRole.Items)
            {
                if (_user.Roles.Contains(item.Tag.ToString()))
                {
                    CmbRole.SelectedItem = item;
                    break;
                }
            }
        }

        private bool Valider()
        {
            var erreurs = new System.Collections.Generic.List<string>();

            if (string.IsNullOrEmpty(TxtNom.Text)) erreurs.Add("Le nom est obligatoire");
            if (string.IsNullOrEmpty(TxtPrenom.Text)) erreurs.Add("Le prénom est obligatoire");
            if (string.IsNullOrEmpty(TxtEmail.Text)) erreurs.Add("L'email est obligatoire");
            if (CmbRole.SelectedItem == null) erreurs.Add("Veuillez choisir un rôle");

            if (erreurs.Count > 0)
            {
                MessageBox.Show(string.Join("\n", erreurs), "Erreurs",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void BtnSauvegarder_Click(object sender, RoutedEventArgs e)
        {
            if (!Valider()) return;

            var selectedRole = CmbRole.SelectedItem as ComboBoxItem;

            _user.Nom = TxtNom.Text;
            _user.Prenom = TxtPrenom.Text;
            _user.Email = TxtEmail.Text;
            _user.Roles = $"[\"{selectedRole.Tag}\"]";

            // Change le mot de passe seulement si renseigné
            if (!string.IsNullOrEmpty(Password.Password))
            {
                _user.Password = BCrypt.Net.BCrypt.HashPassword(Password.Password);
            }

            _userRepo.Update(_user);

            MessageBox.Show("Utilisateur modifié !", "Succès",
                            MessageBoxButton.OK, MessageBoxImage.Information);

            this.DialogResult = true;
            this.Close();
        }

        private void BtnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}