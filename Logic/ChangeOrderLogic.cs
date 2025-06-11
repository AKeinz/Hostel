using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class ChangeOrderLogic : INotifyPropertyChanged
    {
        public readonly IOrdersRepository ordersRepository;
        public readonly IRoomsRepository roomsRepository;

        private Order order;
        public Order Order
        {
            get { return order; }
            set { order = value; OnPropertyChanged(nameof(Order)); }
        }

        public ChangeOrderLogic(IOrdersRepository ordersRepository, IRoomsRepository roomsRepository)
        {
            this.ordersRepository = ordersRepository;
            this.roomsRepository = roomsRepository;
        }

        public ChangeOrderLogic()
        {
            this.ordersRepository = new OrdersRepository();
            roomsRepository = new RoomsRepository();
        }


        public void changeOrder(Order param)
        {
            Order = param;
            if (Order.In_day >= Order.Out_day) { throw new HostelException("Дата заселения должна быть меньше даты выселения");}
            Order.Number_of_days = (Order.Out_day - Order.In_day).Days;

            List<Order> withNoAvailableRooms = isRoomAvailable(Order.Id, Order.Room, Order.In_day, Order.Out_day);
            List<Order> withNoAvailableClient = isClientAvailable(Order.Id, Order.Client, Order.In_day, Order.Out_day);
            if (withNoAvailableRooms is not null && withNoAvailableRooms.Count > 0)
            { throw new HostelException($"Комната занята с {withNoAvailableRooms[0].In_day} по {withNoAvailableRooms[0].Out_day}"); } 
            else if (withNoAvailableClient is not null && withNoAvailableClient.Count > 0)
            { throw new HostelException($"Клиет уже заселен в комнату {withNoAvailableClient[0].Room} с {withNoAvailableClient[0].In_day} по {withNoAvailableClient[0].Out_day}"); }
            double cost_per_day = roomsRepository.GetById(Order.Room).Cost_per_day;
            Order.Total_cost = (double)(Order.Number_of_days * cost_per_day);
            ordersRepository.Update(Order);
        }

        public List<Order> isRoomAvailable(int orderid, int room, DateTime in_day, DateTime out_day)
        {
            List<Order> order = ordersRepository.GetByDateAndRoom(orderid,room, in_day, out_day);
            return order;
        }

        public List<Order> isClientAvailable(int orderid, int client, DateTime in_day, DateTime out_day)
        {
            List<Order> order = ordersRepository.GetByDateAndClient(orderid,client, in_day, out_day);
            return order;
        }

        public void deleteOrder(Order param)
        {
            ordersRepository.Delete(param);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
