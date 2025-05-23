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
    public class OrdersLogic : INotifyPropertyChanged
    {
        public readonly IOrdersRepository ordersRepository;

        private List<Order> orders = new List<Order>();
        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; OnPropertyChanged(nameof(Orders)); }
        }
        public ICommand AddOrderCommand { get; set; }
        public ICommand ChangeOrderCommand { get; set; }

        public OrdersLogic(IOrdersRepository rep)
        {
            this.ordersRepository = rep;
            Orders = getOrders();
            AddOrderCommand = new RelayCommand(displayAddOrder);
            ChangeOrderCommand = new RelayCommand(displayChangeOrder);
        }
        private List<Order> getOrders()
        {
            return ordersRepository.GetAll().ToList();
        }
        private void displayAddOrder(object param)
        {

        }
        private void displayChangeOrder(object param)
        {

        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
