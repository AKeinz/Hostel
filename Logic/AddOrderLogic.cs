using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class AddOrderLogic : BaseLogic, INotifyPropertyChanged
    {
        public readonly IOrdersRepository ordersRepository;
        public readonly IRepository<Room> roomsRepository;

        private List<int> room_numbers = new List<int>();
        private Order order;
        public Order Order
        {
            get { return order; }
            set { order = value; OnPropertyChanged("Order"); }
        }
        public List<int> Room_numbers
        {
            get { return room_numbers; }
            set { room_numbers = value; OnPropertyChanged("Room_numbers"); }
        }


        public AddOrderLogic(IOrdersRepository rep, IRepository<Room> roomsRepository)
        {
            ordersRepository = rep;
            this.roomsRepository = roomsRepository;
            getRoomNumbers();
        }

        public AddOrderLogic()
        {
            ordersRepository = new OrdersRepository();
            this.roomsRepository = new RoomsRepository();
            getRoomNumbers();
        }
        public List<int> getRoomNumbers()
        {
            Room_numbers.Clear();
            List<Room> rooms = roomsRepository.GetAll().ToList();
            foreach (Room room in rooms)
            {
                Room_numbers.Add(room.Room_number);
            }
            return Room_numbers;
        }

        public void AddOrder(object param)
        {
            Order = (Order)param;
            if (Order.Room == -1 || Order.Number_of_people <= 0 || Order.Client == -1)
            {
                throw new HostelException("данные не корректны");
            }
            if (Order.In_day >= Order.Out_day) { throw new  HostelException("Дата заселения должна быть меньше даты выселения");  }
            Order.Number_of_days = (Order.Out_day - Order.In_day).Days;
            if (Order.Number_of_days < 0) { Order.Number_of_days = 0; }

            List<Order> withNoAvailableRooms = isRoomAvailable(Order.Room, Order.In_day, Order.Out_day);
            List<Order> withNoAvailableClient = isClientAvailable(Order.Client, Order.In_day, Order.Out_day);
            if (withNoAvailableRooms is not null && withNoAvailableRooms.Count > 0)
            { throw new HostelException($"Комната занята с {withNoAvailableRooms[0].In_day} по {withNoAvailableRooms[0].Out_day}"); }  
            else if (withNoAvailableClient is not null && withNoAvailableClient.Count > 0)
            { throw new HostelException($"Клиент уже заселен в комнату {withNoAvailableClient[0].Room} с {withNoAvailableClient[0].In_day} по {withNoAvailableClient[0].Out_day}");}
            double cost_per_day = roomsRepository.GetById(Order.Room).Cost_per_day;
            Order.Total_cost = (double)(Order.Number_of_days * cost_per_day);
            ordersRepository.Add(Order);
        }

        public List<Order> isRoomAvailable(int room, DateTime in_day, DateTime out_day)
        {
            var status = roomsRepository.GetById(room).Status;
            if (status.Equals(RoomStatuses.Broken))
            {
                throw new HostelException("Комната не доступна");
            }
            List<Order> order = ordersRepository.GetByDateAndRoom(room, in_day, out_day);
            return order;
        }

        public List<Order> isClientAvailable(int client, DateTime in_day, DateTime out_day)
        {
            List<Order> order = ordersRepository.GetByDateAndClient(client, in_day, out_day);
            return order;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public event BaseLogic.ErrorsHandler? NotifyError;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
