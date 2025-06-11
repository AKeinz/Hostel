using DatabaseLayer;
using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private RoomsLogic roomsLogic = new RoomsLogic();
        private UsersLogic usersLogic = new UsersLogic();
        private OrdersLogic ordersLogic = new OrdersLogic();
        private ProblemsLogic problemsLogic = new ProblemsLogic();
        public AddProblemLogic addProblemLogic = new AddProblemLogic();

        private List<User> users = new List<User>() { new User() { Firstname = "hfhfh" } };
        public List<User> Users
        {
            get { return users; }
            set { users = value; OnPropertyChanged(nameof(Users)); }
        }
        private List<Room> rooms;
        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }
        private List<Order> orders = new List<Order>();
        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; OnPropertyChanged(nameof(Orders)); }
        }
        private List<Problem> problems;
        public List<Problem> Problems
        {
            get { return problems; }
            set { problems = value; OnPropertyChanged(nameof(Problems)); }
        }
        public User CurrentUser { get; set; }
        public ICommand DisplayAddProblemCommand { get; set; }
        public ICommand DisplayChangeProblemCommand { get; set; }
        public ICommand DisplayAddOrderCommand { get; set; }
        public ICommand DisplayChangeOrderCommand { get; set; }
        public ICommand DisplayAddUserCommand { get; set; }
        public ICommand DisplayChangeUserCommand { get; set; }
        public ICommand DisplayAddRoomCommand { get; set; }
        public ICommand DisplayChangeRoomCommand { get; set; }
        public AdminViewModel()
        {
            StatusService.UpdateOrdersStatus();
            users = usersLogic.GetUsers();
            Rooms = roomsLogic.GetRooms();
            Orders = ordersLogic.GetOrders();
            Problems = problemsLogic.GetProblems();
            DisplayAddProblemCommand = new RelayCommand<object>(displayAddProblem);
            DisplayChangeProblemCommand = new RelayCommand<Problem>(displaySelectedProblem);
            DisplayAddOrderCommand = new RelayCommand<object>(displayAddOrder);
            DisplayChangeOrderCommand = new RelayCommand<Order>(displaySelectedOrder);
            DisplayAddUserCommand = new RelayCommand<object>(displayAddUser);
            DisplayChangeUserCommand = new RelayCommand<User>(displaySelectedUser);
            DisplayAddRoomCommand = new RelayCommand<object>(displayAddRoom);
            DisplayChangeRoomCommand = new RelayCommand<Room>(displaySelectedRoom);
        }

        public void Update()
        {
            StatusService.UpdateOrdersStatus();
            Users = usersLogic.GetUsers();
            Rooms = roomsLogic.GetRooms();
            Problems = problemsLogic.GetProblems();
            Orders = ordersLogic.GetOrders();
        }

        private void displayAddProblem(object param)
        {
            NotifyDisplayAddProblem?.Invoke();
        }

        private void displaySelectedProblem(Problem param)
        {
            NotifyDisplaySelectProblem?.Invoke(param);
        }
        private void displayAddOrder(object param)
        {
            NotifyDisplayAddOrder?.Invoke();
        }

        private void displaySelectedOrder(Order param)
        {
            NotifyDisplaySelectOrder?.Invoke(param);
        }

        private void displayAddUser(object param)
        {
            NotifyDisplayAddUser?.Invoke();
        }

        private void displaySelectedUser(User param)
        {
            NotifyDisplaySelectUser?.Invoke(param);
        }

        private void displayAddRoom(object param)
        {
            NotifyDisplayAddRoom?.Invoke();
        }

        private void displaySelectedRoom(Room param)
        {
            NotifyDisplaySelectRoom?.Invoke(param);
        }

        public event ErrorsHandler? NotifyError;

        private void ShowMessage(string message) => NotifyError?.Invoke(message);
        public event DisplayWindowHandler? NotifyDisplayAddProblem;
        public event DisplayWindowHandler? NotifyDisplayAddOrder;
        public event DisplayWindowHandler? NotifyDisplayAddUser;
        public event DisplayWindowHandler? NotifyDisplayAddRoom;

        public delegate void DisplayWithParamHandler<T>(T param);
        public event DisplayWithParamHandler<Problem>? NotifyDisplaySelectProblem;
        public event DisplayWithParamHandler<Order>? NotifyDisplaySelectOrder;
        public event DisplayWithParamHandler<User>? NotifyDisplaySelectUser;
        public event DisplayWithParamHandler<Room>? NotifyDisplaySelectRoom;
    }
}
