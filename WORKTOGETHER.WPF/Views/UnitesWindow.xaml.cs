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
using System.Windows.Shapes;
using WORKTOGETHER.DATA.Repositories;

namespace WORKTOGETHER.WPF.Views
{
    /// <summary>
    /// Logique d'interaction pour UnitesWindow.xaml
    /// </summary>
    public partial class UnitesWindow : Window
    {
        /// <summary>
        /// P
        /// </summary>
        private UniteRepository _repository = new UniteRepository();

        public UnitesWindow()
        {
            InitializeComponent();
            ChargerUnites();
        }

        private void ChargerUnites()
        {
           DgUnites.ItemsSource = _repository.FindAll();
        }
    }
}