
using System.Windows;

using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для Service.xaml
    /// </summary>
    public partial class Service : Window
    {
        public Service(int id)
        {
            InitializeComponent();
            ServiceViewModel serviceViewModel = new ServiceViewModel() { CurrentUser = CurrentUser.GetUser(id) };
            DataContext = serviceViewModel;
            serviceViewModel.NotifyError += ShowMessage;
            serviceViewModel.NotifyDisplaySelectRoom += DisplaySelectedRoom;
            DataContext = serviceViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
            ServiceViewModel serviceViewModel = (ServiceViewModel)problemsGrid.DataContext;
            serviceViewModel.Update();
        }

        private void DisplaySelectedRoom(object user)
        {
            SelectedRoom selectedRoom = new SelectedRoom(user);
            selectedRoom.Show();
        }


    }
}
