using System.Windows;
using WORKTOGETHER.DATA.Repositories;
using BCrypt.Net;
using WORKTOGETHER.DATA.Entities;

namespace WORKTOGETHER.WPF
{
    public partial class LoginWindow : Window
    {
        private UserRepository _userRepo = new UserRepository();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Password;

            // Validation basique
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                AfficherErreur("Veuillez remplir tous les champs !");
                return;
            }

            // Vérifie si l'email contient un @
            var user = _userRepo.FindByEmail(email);
            if (!email.Contains("@"))
            {
                AfficherErreur("Veuillez entrer un email valide !");
                return;
            }
            // Vérifie si l'utilisateur existe
            if (user == null)
            {
                AfficherErreur("Une erreur se produit");
                return;
            }
            // Vérifie le mot de passe bcrypt
            bool passwordOk = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!passwordOk)
            {
                AfficherErreur("Email ou mot de passe incorrect !");
                return;
            }

            
            // Connexion réussie → ouvre MainWindow
            var mainWindow = new MainWindow(user);
            mainWindow.Show();
            this.Close();
        }

        private void AfficherErreur(string message)
        {
            txtErreur.Text = message;
            txtErreur.Visibility = Visibility.Visible;
        }
    }
}