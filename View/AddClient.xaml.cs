using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public AddClient()
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
