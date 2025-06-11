using Logic;
using Model;
using System.Windows.Input;

namespace ViewModel
{
    public class SelectedOrderViewModel : BaseViewModel
    {
        ChangeOrderLogic changeOrderLogic = new ChangeOrderLogic();
        UsersLogic usersLogic = new UsersLogic();
        RoomsLogic roomsLogic = new RoomsLogic();


        private Order order = new Order();
        public Order Order
        {
            get { return order; }
            set { order = value; OnPropertyChanged(nameof(Order)); }
        }


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

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }


        public SelectedOrderViewModel(Order order)
        {
            Order = order;
            Clients = usersLogic.GetClietnIds();
            Rooms = roomsLogic.GetRoomIds();
            UpdateCommand = new RelayCommand<Order>(update);
            DeleteCommand = new RelayCommand<Order>(delete);
        }

        private void update(object param)
        {
            try
            {
                changeOrderLogic.changeOrder(Order);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (HostelException ex)
            {
                NotifyError?.Invoke(ex.Message);
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke("Возникла ошибка в процессе обновления данных");
            }
        }

        private void delete(object param)
        {
            try
            {
                changeOrderLogic.deleteOrder(Order);
                NotifyError?.Invoke("Успешно!");
                NotifyDisplay?.Invoke();
            }
            catch (Exception ex)
            {
                NotifyError?.Invoke("Возникла ошибка в процессе удаления");
            }
        }

        public event ErrorsHandler? NotifyError;
        public event DisplayWindowHandler? NotifyDisplay;

        private void ShowMessage(string message) => NotifyError?.Invoke(message);


    }
}
