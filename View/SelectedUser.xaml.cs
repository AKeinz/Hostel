using Model;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для SelectedUser.xaml
    /// </summary>
    public partial class SelectedUser : Window
    {
        public SelectedUser(object user)
        {
            InitializeComponent();
            SelectedUserViewModel selectedUserViewModel = new SelectedUserViewModel((User)user);
            selectedUserViewModel.NotifyDisplay += Close;
            DataContext = selectedUserViewModel;

            selectedUserViewModel.NotifyError += ShowMessage;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
