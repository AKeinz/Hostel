using DatabaseLayer;
using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class ReceptionViewModel : BaseViewModel
    {
        StatusService orderStatusService = new StatusService();
        private AddProblemLogic addProblemLogic = new AddProblemLogic();
        private RoomsLogic roomsLogic = new RoomsLogic();
        private UsersLogic usersLogic = new UsersLogic();
        ProblemsLogic problemsLogic = new ProblemsLogic();
        OrdersLogic ordersLogic = new OrdersLogic();
        ChangeProblemLogic changeProblemLogic = new ChangeProblemLogic();

        public User CurrentUser { get; set; }

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        }

        private List<User> clients = new List<User>() { new User() { Firstname = "hfhfh" } };
        public List<User> Clients
        {
            get { return clients; }
            set { clients = value; OnPropertyChanged(nameof(Clients)); }
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
        private List<int> room_numbers = new List<int>();
        public List<int> Room_numbers
        {
            get { return room_numbers; }
            set { room_numbers = value; OnPropertyChanged("Room_numbers"); }
        }

        private int room_number;
        public int Room_number
        {
            get { return room_number; }
            set { room_number = value; OnPropertyChanged("Room_number"); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        private bool isEnabledStatusesCombobox = false;
        public bool IsEnabledStatusesCombobox
        {
            get { return isEnabledStatusesCombobox; }
            set { isEnabledStatusesCombobox = value; OnPropertyChanged(nameof(IsEnabledStatusesCombobox)); }
        }

        private string changeButtonContent = "Изменить";
        public string ChangeButtonContent
        {
            get { return changeButtonContent; }
            set { changeButtonContent = value; OnPropertyChanged("ChangeButtonContent"); }
        }

        public static List<ProblemStatuses> Statuses { get; } = new List<ProblemStatuses> { ProblemStatuses.Done, ProblemStatuses.OnQuery, ProblemStatuses.Active };



        public ICommand AddProblemCommand { get; set; }
        public ICommand DisplayAddProblemCommand { get; set; }
        public ICommand ChangeStatusCommand { get; set; }
        public ICommand DisplayAddOrderCommand { get; set; }
        public ICommand DisplayChangeOrderCommand { get; set; }
        public ICommand DisplayAddUserCommand { get; set; }
        public ICommand DisplaySelectUserCommand { get; set; }
        public ICommand DisplaySelectRoomCommand { get; set; }

        public ReceptionViewModel()
        {
            StatusService.UpdateOrdersStatus();
            AddProblemCommand = new RelayCommand<object>(addProblem);
            Clients = usersLogic.GetClients();
            Rooms = roomsLogic.GetRooms();
            Problems = problemsLogic.GetProblems();
            Orders = ordersLogic.GetOrders();
            Room_numbers = problemsLogic.GetRoomNumbers();
            addProblemLogic.NotifyError += ShowMessage;
            ChangeStatusCommand = new RelayCommand<object>(changeStatus);
            DisplayAddOrderCommand = new RelayCommand<object>(displayAddOrder);
            DisplayChangeOrderCommand = new RelayCommand<Order>(displaySelectedOrder);
            DisplayAddUserCommand = new RelayCommand<object>(displayAddUser);
            DisplaySelectUserCommand = new RelayCommand<User>(displaySelectedUser);
            DisplayAddProblemCommand = new RelayCommand<object>(displayAddProblem);
            DisplaySelectRoomCommand = new RelayCommand<Room>(displaySelectedRoom);
        }

        private void addProblem(object param)
        {
            addProblemLogic.Client_id = CurrentUser.Id;
            try
            {
                addProblemLogic.AddProblem(CurrentUser.Id, Description, Room_number);
            }
            catch (Exception ex)
            {
                ShowMessage("Ошибка добавления неисправности. Не оставляйте описание пустым");
            }
            Update();
        }
        private void displayAddProblem(object param)
        {
            NotifyDisplayAddProblem?.Invoke();
        }

        private void changeStatus(object param)
        {
            if (IsEnabledStatusesCombobox)
            {
                int id = (int)param;
                Problem problem = Problems.Where(p => p.Id == id).First();
                changeProblemLogic.changeProblem(problem);
            }
            changeIsEnabled();
            Update();
        }

        private void changeIsEnabled()
        {
            IsEnabledStatusesCombobox = !IsEnabledStatusesCombobox;
            if (IsEnabledStatusesCombobox)
            {
                ChangeButtonContent = "Сохранить изменения";
            }
            else { ChangeButtonContent = "Изменить"; }
        }
        public void Update()
        {
            StatusService.UpdateOrdersStatus();
            Clients = usersLogic.GetClients();
            Rooms = roomsLogic.GetRooms();
            Problems = problemsLogic.GetProblems();
            Orders = ordersLogic.GetOrders();
            Room_numbers = problemsLogic.GetRoomNumbers();
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
            NotifyDisplayAddClient?.Invoke();
        }

        private void displaySelectedUser(User param)
        {
            NotifyDisplaySelectClient?.Invoke(param);
        }

        private void displaySelectedRoom(Room param)
        {
            NotifyDisplaySelectRoom?.Invoke(param);
        }


        public event ErrorsHandler? NotifyError;

        public event DisplayWindowHandler? NotifyDisplayAddProblem;
        public event DisplayWindowHandler? NotifyDisplayAddOrder;
        public event DisplayWindowHandler? NotifyDisplayAddClient;

        public delegate void DisplayWithParamHandler<T>(T param);
        public event DisplayWithParamHandler<Problem>? NotifyDisplaySelectProblem;
        public event DisplayWithParamHandler<Order>? NotifyDisplaySelectOrder;
        public event DisplayWithParamHandler<User>? NotifyDisplaySelectClient;
        public event DisplayWithParamHandler<Room>? NotifyDisplaySelectRoom;

        private void ShowMessage(string message) => NotifyError?.Invoke(message);
    }
}
