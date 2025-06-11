using Model;
using System.Windows;
using ViewModel;
namespace View
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        AdminViewModel adminViewModel;
        public Administrator(int id)
        {
            InitializeComponent();
            adminViewModel = new AdminViewModel() { CurrentUser = CurrentUser.GetUser(id) };

            adminViewModel.NotifyDisplayAddProblem += DisplayAddProblem;
            adminViewModel.NotifyDisplaySelectProblem += DisplaySelectedProblem;

            adminViewModel.NotifyDisplayAddOrder += DisplayAddOrder;
            adminViewModel.NotifyDisplaySelectOrder += DisplaySelectedOrder;

            adminViewModel.NotifyDisplayAddUser += DisplayAddUser;
            adminViewModel.NotifyDisplaySelectUser += DisplaySelectedUser;

            adminViewModel.NotifyDisplayAddRoom += DisplayAddRoom;
            adminViewModel.NotifyDisplaySelectRoom += DisplaySelectedRoom;

            DataContext = adminViewModel;
        }


        private void DisplayAddProblem()
        {
            AddProblem addProblem = new AddProblem();
            addProblem.ShowDialog();
            adminViewModel.Update();
        }

        private void DisplaySelectedProblem(object problem)
        {
            SelectedProblem selectedProblem = new SelectedProblem(problem);
            selectedProblem.ShowDialog();
            adminViewModel.Update();
        }

        private void DisplayAddOrder()
        {
            AddOrder addOrder = new AddOrder();
            addOrder.ShowDialog();
            adminViewModel.Update();
        }

        private void DisplaySelectedOrder(object order)
        {
            SelectedOrder selectedOrder = new SelectedOrder(order);
            selectedOrder.ShowDialog();
            adminViewModel.Update();
        }
        private void DisplayAddUser()
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();
            adminViewModel.Update();
        }

        private void DisplaySelectedUser(object user)
        {
            SelectedUser selectedUser = new SelectedUser(user);
            selectedUser.ShowDialog();
            adminViewModel.Update();
        }

        private void DisplayAddRoom()
        {
            AddRoom addRoom = new AddRoom();
            addRoom.ShowDialog();
            adminViewModel.Update();
        }

        private void DisplaySelectedRoom(object room)
        {
            ChangeRoom selectedRoom = new ChangeRoom((Room)room);
            selectedRoom.ShowDialog();
            adminViewModel.Update();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
