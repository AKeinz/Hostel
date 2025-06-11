using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для ClientMain.xaml
    /// </summary>
    public partial class ClientMain : Window
    {
        public ClientMain(int id)
        {
            InitializeComponent();
            ClientViewModel clientViewModel = new ClientViewModel() { CurrentUser = CurrentUser.GetUser(id) };
            clientViewModel.NotifyError += ShowMessage;
            clientViewModel.NotifyDisplaySelectRoom += DisplaySelectedRoom;
            DataContext = clientViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void DisplaySelectedRoom(object user)
        {
            SelectedRoom selectedRoom = new SelectedRoom(user);
            selectedRoom.Show();
        }

    }
}
