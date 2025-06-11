using DatabaseLayer;
using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Logic
{
    public class OrdersLogic : BaseLogic, INotifyPropertyChanged
    {
        public readonly IOrdersRepository ordersRepository;

        private List<Order> orders = new List<Order>();
        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; OnPropertyChanged(nameof(Orders)); }
        }

        public OrdersLogic(IOrdersRepository rep)
        {
            this.ordersRepository = rep;
            Orders = GetOrders();
        }

        public OrdersLogic()
        {
            this.ordersRepository = new OrdersRepository();
            Orders = GetOrders();
        }

        public List<Order> GetOrders()
        {
            Orders = ordersRepository.GetAll().ToList();
            return Orders;
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
