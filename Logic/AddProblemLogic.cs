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
    public class AddProblemLogic : INotifyPropertyChanged
    {
        public readonly IRepository<Problem> problemsRepository;
        public readonly IOrdersRepository ordersRepository;

        private int client_id;
        private string description;


        public int Client_id
        {
            get { return client_id; }
            set { client_id = value; OnPropertyChanged("User_id"); }
        }
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }
        public ICommand AddProblemCommand { get; set; }

        public AddProblemLogic(IRepository<Problem> problemsRepository, IOrdersRepository ordersRepository)
        {
            this.problemsRepository = problemsRepository;
            this.ordersRepository = ordersRepository;
            AddProblemCommand = new RelayCommand(addProblem);
        }

        private int getCurrentRoom()
        {
            return ordersRepository.GetCurrentRoom(client_id);
        }

        private void addProblem(object param)
        {
            int room = getCurrentRoom();
            if (room != -1)
            {
                problemsRepository.Add(new Problem()
                {
                    Room = room,
                    Description = Description,
                    User = client_id,
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
