using System.Windows;
using WORKTOGETHER.DATA.Repositories;
using BCrypt.Net;
using WORKTOGETHER.DATA.Entities;
using MySql.Data.MySqlClient;

namespace WORKTOGETHER.WPF
{
    public partial class LoginWindow : Window
    {
        private UserRepository _userRepo = new UserRepository();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnConnexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userRepo = new UserRepository();
                var user = userRepo.FindByEmail(TxtEmail.Text);

                if (user == null || !user.Roles.Contains("ROLE_ADMIN")
                                 && !user.Roles.Contains("ROLE_COMPTABLE"))
                {
                    TxtErreur.Text = "Email ou mot de passe incorrect !";
                    TxtErreur.Visibility = Visibility.Visible;
                    return;
                }

                new MainWindow(user).Show();
                this.Close();
            }
            catch (MySqlException)
            {
                // Catch spécifique pour les erreurs de connexion à MySQL
                MessageBox.Show(
                    "Impossible de se connecter à la base de données !\n" +
                    "Vérifiez que MySQL est bien démarré.",
                    "Erreur de connexion",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AfficherErreur(string message)
        {
            TxtErreur.Text = message;
            TxtErreur.Visibility = Visibility.Visible;
        }
    }
}