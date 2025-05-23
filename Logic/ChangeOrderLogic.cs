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
    public class ChangeOrderLogic : INotifyPropertyChanged
    {
        public readonly IOrdersRepository ordersRepository;

        private Order order;
        public Order Order
        {
            get { return order; }
            set { order = value; OnPropertyChanged(nameof(Order)); }
        }
        public ICommand UpdateOrderCommand { get; set; }
        public ICommand DeleteOrderCommand { get; set; }
        public ChangeOrderLogic(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
            UpdateOrderCommand = new RelayCommand(changeOrder);
            DeleteOrderCommand = new RelayCommand(deleteOrder);
        }

        private void changeOrder(object param)
        {
            ordersRepository.Update(Order);
        }

        private void deleteOrder(object param)
        {
            ordersRepository.Delete(Order);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
