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
    /// Logique d'interaction pour InterventionsWindow.xaml
    /// </summary>
    public partial class InterventionsWindow : Window
    {
        private InterventionRepository _repository = new InterventionRepository();

        public InterventionsWindow()
        {
            InitializeComponent();
            ChargerIntervetnion();
        }

        private void ChargerIntervetnion()
        {
            DgInterventions.ItemsSource = _repository.FindAllWithDetails();
        }
    }
}
