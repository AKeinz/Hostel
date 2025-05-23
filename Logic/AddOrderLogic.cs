using DatabaseLayer;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic
{
    public class AddOrderLogic : INotifyPropertyChanged
    {
        public readonly IOrdersRepository ordersRepository;
        public readonly IRepository<Room> roomsRepository;

        private List<int> room_numbers = new List<int>();
        private int room_number;
        private int client_id;
        private DateTime in_day;
        private DateTime out_day;
        private int number_of_people;
        private int total_cost;

        public List<int> Room_numbers
        {
            get { return room_numbers; }
            set { room_numbers = value; OnPropertyChanged("Room_numbers"); }
        }
        public int Room_number
        {
            get { return room_number; }
            set { room_number = value; OnPropertyChanged("Room_number"); }
        }
        public int Client_id
        {
            get { return client_id; }
            set { client_id = value; OnPropertyChanged("Client_id"); }
        }
        public DateTime In_day
        {
            get { return in_day; }
            set { in_day = value; OnPropertyChanged("In_day"); }
        }
        public DateTime Out_day
        {
            get { return out_day; }
            set { out_day = value; OnPropertyChanged("Out_day"); }
        }
        public int Number_of_people
        {
            get { return number_of_people; }
            set { number_of_people = value; OnPropertyChanged("Number_of_people"); }
        }
        public int Total_cost
        {
            get { return total_cost; }
            set { total_cost = value; OnPropertyChanged("Total_cost"); }
        }
        public ICommand AddOrderCommand { get; set; }
        public AddOrderLogic(IOrdersRepository rep, IRepository<Room> roomsRepository)
        {
            ordersRepository = rep;
            this.roomsRepository = roomsRepository;
            getRoomNumbers();
            AddOrderCommand = new RelayCommand(addOrder);
            this.roomsRepository = roomsRepository;
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

        private void addOrder(object param)
        {
            List<Order> withNoAvailableRooms = isRoomAvailable(Room_number, In_day, Out_day);
            List<Order> withNoAvailableClient = isClientAvailable(Client_id, In_day, Out_day);
            if (withNoAvailableRooms is not null) 
            { throw new HostelException($"Комната занята с {withNoAvailableRooms[0].In_day} по {withNoAvailableRooms[0].Out_day}" ); }  //добавить даты когда занята
            else if (withNoAvailableClient.Count > 0) 
            { throw new HostelException($"Клиет уже заселен в комнату {withNoAvailableClient[0].Room} с {withNoAvailableClient[0].In_day} по {withNoAvailableClient[0].Out_day}"); }  //добавить даты когда занята и комнату
            ordersRepository.Add(new Order()
            {
                Room = Room_number,
                Client = Client_id,
                In_day = In_day,
                Out_day = Out_day,
                Number_of_days = (Out_day - In_day).Days,
                Number_of_people = Number_of_people,

            });
        }

        public List<Order> isRoomAvailable(int room, DateTime in_day, DateTime out_day)
        {
            List<Order> order = ordersRepository.GetByDateAndRoom(room, in_day, out_day);
            return order;
        }

        public List<Order> isClientAvailable(int client, DateTime in_day, DateTime out_day)
        {
            List<Order> order = ordersRepository.GetByDateAndClient(client, in_day, out_day);
            return order;
        }

        private void CloseAddOrder()
        {
            NotifyCloseOrder?.Invoke();
        }


        public delegate void Handler();
        public event Handler? NotifyCloseOrder;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
