using Model;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для SelectedClient.xaml
    /// </summary>
    public partial class SelectedClient : Window
    {
        public SelectedClient(object user)
        {
            SelectedUserViewModel selectedUserViewModel = new SelectedUserViewModel((User)user);

            DataContext = selectedUserViewModel;
            InitializeComponent();
        }
    }
}
