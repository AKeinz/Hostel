using DatabaseLayer;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Logic
{
    public class RoomsLogic : INotifyPropertyChanged
    {
        public readonly IRepository<Room> roomsRepository;

        private List<Room> rooms;
        public List<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; OnPropertyChanged(nameof(Rooms)); }
        }
        public ICommand AddRoomCommand { get; set; }
        public ICommand SelectRoomCommand { get; set; }

        public RoomsLogic(IRepository<Room> rep)
        {
            roomsRepository = rep;
            Rooms = getRooms();
            AddRoomCommand = new RelayCommand(displayAddRoom);
            SelectRoomCommand = new RelayCommand(displaySelectedRoom);
        }

        private List<Room> getRooms()
        {
            return roomsRepository.GetAll().ToList();
        }
        private void displayAddRoom(object param)
        {
            Notify?.Invoke();
        }
        private void displaySelectedRoom(object param)
        {

        }


        public delegate void RoomsHandler();
        public event RoomsHandler? Notify;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
