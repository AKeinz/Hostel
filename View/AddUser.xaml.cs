using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            AddUserViewModel addUserViewModel = new AddUserViewModel();
            addUserViewModel.NotifyDisplay += Close;
            DataContext = addUserViewModel;

            addUserViewModel.NotifyError += ShowMessage;
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
