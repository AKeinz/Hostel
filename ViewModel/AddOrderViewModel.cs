using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class AddOrderViewModel : BaseViewModel
    {
        AddOrderLogic addOrderLogic = new AddOrderLogic();
        UsersLogic usersLogic = new UsersLogic();
        RoomsLogic roomsLogic = new RoomsLogic();

        private Order order = new Order();
        public Order Order
        {
            get { return order; }
            set { order = value; OnPropertyChanged(nameof(Order)); }
        }

        public static List<OrderStatuses> Statuses { get; } = new List<OrderStatuses> {
            OrderStatuses.Completed,
            OrderStatuses.InProgress,
            OrderStatuses.Reservation};

        private List<int> clients = new List<int>();
        public List<int> Clients
        {
            get { return clients; }
            set { clients = value; OnPropertyChanged(nameof(Clients)); }
        }

        private List<int> rooms = new List<int>();
        public List<int> Rooms
        {
            get { return rooms; }
            set { rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }

        public ICommand AddCommand { get; set; }

        public AddOrderViewModel()
        {
            AddCommand = new RelayCommand<object>(add);
            addOrderLogic.NotifyError += ShowMessage;
            Clients = usersLogic.GetClietnIds();
            Rooms = roomsLogic.GetRoomIds();
        }

        private void add(object param)
        {
            try
            {
                addOrderLogic.AddOrder(order);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (HostelException ex)
            {
                NotifyError?.Invoke(ex.Message);
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke("Ошибка добавления");
            }
        }

        public event ErrorsHandler? NotifyError;
        public event DisplayWindowHandler? NotifyDisplay;

        private void ShowMessage(string message) => NotifyError?.Invoke(message);
    }
}
