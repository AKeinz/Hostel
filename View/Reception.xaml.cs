using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Логика взаимодействия для Reception.xaml
    /// </summary>
    public partial class Reception : Window
    {
        ReceptionViewModel receptionViewModel;
        public Reception(int id)
        {
            InitializeComponent();
            receptionViewModel = new ReceptionViewModel() { CurrentUser = CurrentUser.GetUser(id) };
            DataContext = receptionViewModel;

            receptionViewModel.NotifyDisplayAddProblem += DisplayAddProblem;
            receptionViewModel.NotifyDisplaySelectProblem += DisplaySelectedProblem;

            receptionViewModel.NotifyDisplayAddOrder += DisplayAddOrder;
            receptionViewModel.NotifyDisplaySelectOrder += DisplaySelectedOrder;

            receptionViewModel.NotifyDisplayAddClient += DisplayAddClient;
            receptionViewModel.NotifyDisplaySelectClient += DisplaySelectedClient;

            receptionViewModel.NotifyDisplaySelectRoom += DisplaySelectedRoom;

            receptionViewModel.NotifyError += ShowMessage;

        }

        private void DisplayAddProblem()
        {
            AddProblem addProblem = new AddProblem();
            addProblem.ShowDialog();
            receptionViewModel.Update();
        }

        private void DisplaySelectedProblem(object problem)
        {
            SelectedProblem selectedProblem = new SelectedProblem(problem);
            selectedProblem.ShowDialog();
            receptionViewModel.Update();
        }

        private void DisplayAddOrder()
        {
            AddOrder addOrder = new AddOrder();
            addOrder.ShowDialog();
            receptionViewModel.Update();
        }

        private void DisplaySelectedOrder(object order)
        {
            SelectedOrder selectedOrder = new SelectedOrder(order);
            selectedOrder.ShowDialog();
            receptionViewModel.Update();
        }
        private void DisplayAddClient()
        {
            AddClient addClient = new AddClient();
            addClient.ShowDialog();
            receptionViewModel.Update();
        }

        private void DisplaySelectedClient(object user)
        {
            SelectedClient selectedClient = new SelectedClient(user);
            selectedClient.Show();
        }

        private void DisplaySelectedRoom(object user)
        {
            SelectedRoom selectedRoom = new SelectedRoom(user);
            selectedRoom.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
